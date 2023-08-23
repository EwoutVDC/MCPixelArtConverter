using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    //TODO: P4 add converter that matches each pixel of the block texture to a part of the original image (scaled down 16x less)
    //Average uses the average color of each block texture as one pixel
    class ImageConverterAverage : ImageConverter
    {
        ConcurrentDictionary<MCBlockVariant, ColorDouble> averageColors = new ConcurrentDictionary<MCBlockVariant, ColorDouble>();

        public ImageConverterAverage(Dictionary<MCBlockVariant, Bitmap> palette)
        {
            Parallel.ForEach(palette, kv =>
            {
                if (!kv.Key.Selected)
                    return;

                ColorDouble averageColor = new ColorDouble(0, 0, 0, 0);
                Bitmap bm = kv.Value;
                if (bm == null)
                    return;

                for (int w = 0; w < bm.Width; w++)
                {
                    for (int h = 0; h < bm.Height; h++)
                    {
                        averageColor += new ColorDouble(bm.GetPixel(w, h).A,
                                                        bm.GetPixel(w, h).R,
                                                        bm.GetPixel(w, h).G,
                                                        bm.GetPixel(w, h).B);
                    }
                }

                averageColor /= bm.Width * bm.Height;

                if (!averageColors.TryAdd(kv.Key, averageColor))
                    Console.Error.WriteLine("Could not add average color for" + kv.Key);
            }
            );
        }

        public MCBlockVariant[,] Convert(Bitmap image, Size size, ImageDitherer ditherer)
        {            
            //todo: P4 investigate how this scales up/down
            Bitmap scaledImage = new Bitmap(image, size);

            MCBlockVariant[,] blocks = new MCBlockVariant[size.Width, size.Height];
            
            if (ditherer == null)
            {
                //without dithering, parallel processing is easier since there is no dependency on previously
                //calculated blocks
                object lockobj = new object();

                Parallel.For(0, size.Width, w =>
                {
                    Bitmap scaledImageClone;
                    lock (lockobj)
                    {
                        scaledImageClone = scaledImage.Clone(new Rectangle(w, 0, 1, size.Height), scaledImage.PixelFormat);
                    }
                    for (int h = 0; h < size.Height; h++)
                    {
                        blocks[w, h] = GetBestVariant(scaledImageClone.GetPixel(0, h));
                    }
                }
                );
            }
            else
            {
                //with dithering, parallel processing is harder/impossible because the error needs to be
                //propagated to the next pixels
                for (int h = 0; h < scaledImage.Height; h++)
                {
                    for (int w = 0; w < scaledImage.Width; w++)
                    {
                        ColorDouble errorColor;
                        blocks[w, h] = GetBestVariantWithError(scaledImage.GetPixel(w, h),
                                                               ditherer.GetColorOffset(w,h),
                                                               out errorColor);
                        ditherer.ApplyError(w, h, errorColor);
                    }
                    Console.WriteLine((h + 1) + "/" + scaledImage.Height);
                }
            }

            return blocks;
        }
        
        MCBlockVariant GetBestVariantWithError(Color targetColor,
                                               ColorDouble colorOffset,
                                               out ColorDouble errorColor)
        {
            ColorDouble targetColorDouble = new ColorDouble(targetColor) + colorOffset;
            Double minDiffScore = Double.MaxValue;
            MCBlockVariant bestVariant = null;
            foreach (KeyValuePair<MCBlockVariant, ColorDouble> variantColor in averageColors)
            {
                if (!variantColor.Key.Selected)
                    continue;

                ColorDouble c = variantColor.Value;
                Double diff = c.Difference(targetColorDouble);
                if (diff < minDiffScore)
                {
                    bestVariant = variantColor.Key;
                    minDiffScore = diff;
                }
            }

            errorColor = targetColorDouble - averageColors[bestVariant];

            return bestVariant;
        }
        
        MCBlockVariant GetBestVariant(Color pixel)
        {
            Double minDiffScore = Double.MaxValue;
            MCBlockVariant bestVariant = null;
            foreach (KeyValuePair<MCBlockVariant, ColorDouble> variantColor in averageColors)
            {
                ColorDouble c = variantColor.Value;
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
