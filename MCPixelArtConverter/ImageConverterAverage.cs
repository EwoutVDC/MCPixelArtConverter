using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    //Average uses the average color of each block texture as one pixel
    //TODO: add converter that matches each pixel of the block texture to the original image
    class ImageConverterAverage : ImageConverter
    {
        static string[] validBlocks = {"black_wool",
                                       "blue_wool",
                                       "brown_wool",
                                       "cyan_wool",
                                       "gray_wool",
                                       "green_wool",
                                       "light_blue_wool",
                                       "lime_wool",
                                       "magenta_wool",
                                       "orange_wool",
                                       "pink_wool",
                                       "purple_wool",
                                       "red_wool",
                                       "silver_wool",
                                       "white_wool",
                                       "yellow_wool" };

        MCResourcePack resourcePack;

        Dictionary<MCBlockVariant, Color> averageColors = new Dictionary<MCBlockVariant, Color>();

        public ImageConverterAverage(MCResourcePack rp, Sides facing)
        {
            resourcePack = rp;

            foreach(KeyValuePair<MCBlockVariant, Bitmap> kv in rp.GetPalette(facing))
            {
                Int64 r = 0, g = 0, b = 0;
                Bitmap bm = kv.Value;
                if (bm == null)
                    continue; //Could not get a good texture for this variant from the facing side

                for (int w = 0; w < bm.Width; w++)
                {
                    for (int h = 0; h < bm.Height; h++)
                    {
                        r += bm.GetPixel(w, h).R;
                        g += bm.GetPixel(w, h).G;
                        b += bm.GetPixel(w, h).B;
                    }
                }
                r /= (bm.Width * bm.Height);
                g /= (bm.Width * bm.Height);
                b /= (bm.Width * bm.Height);

                averageColors.Add(kv.Key, Color.FromArgb((int)r, (int)g, (int)b));
            }
        }

        public MCBlockVariant[,] Convert(Bitmap image, Size size)
        {            
            Bitmap scaledImage = new Bitmap(image, size);
            //TODO: better way to convert image: Create palette with blocks, use GDI+ Bitmap.ConvertFormat (C++)??

            MCBlockVariant[,] blocks = new MCBlockVariant[size.Width, size.Height];

            for (int w = 0; w < size.Width; w++)
            {
                for (int h = 0; h < size.Height; h++)
                {
                    blocks[w,h] = GetBestVariant(scaledImage.GetPixel(w, h));
                }
            }

            //TODO: apply dithering? => allow to set imageDitherer and use that for different dithering algorithms

            return blocks;
        }

        MCBlockVariant GetBestVariant(Color pixel)
        {
            Double minDiffScore = Double.MaxValue;
            MCBlockVariant bestVariant = null;
            foreach (KeyValuePair<MCBlockVariant, Color> variantColor in averageColors)
            {
                Color c = variantColor.Value;
                Double diff = Math.Pow(pixel.R - c.R, 2) + Math.Pow(pixel.G - c.G, 2) + Math.Pow(pixel.B - c.B, 2);
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
