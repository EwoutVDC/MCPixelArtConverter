using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    //TODO: add converter that matches each pixel of the block texture to the original image
    //Average uses the average color of each block texture as one pixel
    class ImageConverterAverage : ImageConverter
    {
        Dictionary<MCBlockVariant, Color> averageColors = new Dictionary<MCBlockVariant, Color>();

        public ImageConverterAverage(Dictionary<MCBlockVariant, Bitmap> palette)
        {
            //TODO: do this in parallel? it breaks the loading somehow
            foreach (KeyValuePair<MCBlockVariant, Bitmap> kv in palette)
                //Parallel.ForEach(palette, kv =>
            {
                Int64 r = 0, g = 0, b = 0, a = 0;
                Bitmap bm = kv.Value;
                if (bm == null)
                    continue;

                for (int w = 0; w < bm.Width; w++)
                {
                    for (int h = 0; h < bm.Height; h++)
                    {
                        a += bm.GetPixel(w, h).A;
                        r += bm.GetPixel(w, h).R;
                        g += bm.GetPixel(w, h).G;
                        b += bm.GetPixel(w, h).B;
                    }
                }
                a /= (bm.Width * bm.Height);
                r /= (bm.Width * bm.Height);
                g /= (bm.Width * bm.Height);
                b /= (bm.Width * bm.Height);


                averageColors.Add(kv.Key, Color.FromArgb((int)a, (int)r, (int)g, (int)b));
            }
            //);
        }

        public MCBlockVariant[,] Convert(Bitmap image, Size size)
        {            
            Bitmap scaledImage = new Bitmap(image, size);

            MCBlockVariant[,] blocks = new MCBlockVariant[size.Width, size.Height];

            object lockobj = new object();

            //TODO: look into progress bar?

            Parallel.For(0, size.Width, w =>
            {
                Bitmap scaledImageClone;
                lock (lockobj)
                {
                    scaledImageClone = (Bitmap)scaledImage.Clone(new Rectangle(w, 0, 1, size.Height), scaledImage.PixelFormat);
                }
                for (int h = 0; h < size.Height; h++)
                {
                    blocks[w, h] = GetBestVariant(scaledImageClone.GetPixel(0, h));
                }
            }
            );

            //TODO: apply dithering? => allow to set imageDitherer and use that for different dithering algorithms
            /*
             * Interface for dithering: imageDitherer.diffuseError(scaledImage, blocks[w,h], w, h) ?
             * Probably better to calculate error to diffuse in the converter?
             * How can that work with a converter that selects a block based on its entire pixel instead of only the average
             * color?
             * -> pass original image and chosen block? seems to tightly coupled
             * -> different ditherers with same algorithm for different converters? i would like to prevent that
             * 
             * Dithering is applied on a block level since that is the smallest unit we can change individually
             * Even if the error calculation is done on pixel level!
             * 
             * steps for dithering:
             * - calculate best block for target pixel/image area (ImageConverter)
             * - calculate block color error (in ImageConverter since this is different for each converter: ie avg colors vs individual pixels)
             * - diffuse error to next target pixels/image area
             *      => ImageDitherer.GetColorOffsetForBlock(image, w,h, out a, out r, out g, out b) ??
             *         ImageConverter.ApplyColorOffsetForBlock(w,h,a,r,g,b);
             * - 
             * 
             * 
             */

            return blocks;
        }


        //TODO: evaluate this multithreading vs singlethreading vs pixel level multithreading
        //This might still be usefull for dithering ?
        MCBlockVariant GetBestVariantParallel(Color pixel)
        {
            Double minDiffScore = Double.MaxValue;
            MCBlockVariant bestVariant = null;
            Parallel.ForEach(averageColors, variantColor =>
            {
                Color c = variantColor.Value;
                Double diff = Math.Pow(pixel.A - c.A, 2) +
                              Math.Pow(pixel.R - c.R, 2) +
                              Math.Pow(pixel.G - c.G, 2) +
                              Math.Pow(pixel.B - c.B, 2);
                if (diff < minDiffScore)
                {
                    bestVariant = variantColor.Key;
                    minDiffScore = diff;
                }
            });

            return bestVariant;
        }

        MCBlockVariant GetBestVariant(Color pixel)
        {
            Double minDiffScore = Double.MaxValue;
            MCBlockVariant bestVariant = null;
            foreach (KeyValuePair<MCBlockVariant, Color> variantColor in averageColors)
            {
                if (!variantColor.Key.Selected)
                    continue;

                //TODO: verify color matching. not very convincing on picture
                Color c = variantColor.Value;
                Double diff = Math.Pow(pixel.A - c.A, 2) + 
                              Math.Pow(pixel.R - c.R, 2) +
                              Math.Pow(pixel.G - c.G, 2) +
                              Math.Pow(pixel.B - c.B, 2);
                if (diff < minDiffScore)
                {
                    bestVariant = variantColor.Key;
                    minDiffScore = diff;
                }
            }

            return bestVariant;
        }
    }
}
