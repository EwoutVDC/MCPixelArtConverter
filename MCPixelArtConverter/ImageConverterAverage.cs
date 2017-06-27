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
        static String[] validBlocks = {"black_wool",
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
        Dictionary<String, Color> averageColors = new Dictionary<String, Color>();

        public ImageConverterAverage(MCResourcePack rp)
        {
            resourcePack = rp;

            foreach (String blockName in validBlocks)
            {
                MCBlockState blockState = resourcePack.getBlockState(blockName);
                Bitmap topView = blockState.GetTopView();
                //TODO: this average calculation is unsafe
                Int64 r = 0, g = 0, b = 0;

                for (int w = 0; w < topView.Width; w++)
                {
                    for (int h = 0; h < topView.Height; h++)
                    {
                        r += topView.GetPixel(w, h).R;
                        g += topView.GetPixel(w, h).G;
                        b += topView.GetPixel(w, h).B;
                    }
                }
                r /= (topView.Width * topView.Height);
                g /= (topView.Width * topView.Height);
                b /= (topView.Width * topView.Height);

                averageColors.Add(blockName, Color.FromArgb((int)r, (int)g, (int)b));
            }
        }

        public MCBlockState[,] Convert(Bitmap image, Size size)
        {
            //TODO: Does this uses interpolation/dithering???? better scaling algorithm using Graphics
            Graphics g = Graphics.FromImage(image);
            Bitmap scaledImage = new Bitmap(image, size);
            //scaledImage.
            MCBlockState[,] blocks = new MCBlockState[size.Width, size.Height];

            for (int w = 0; w < size.Width; w++)
            {
                for (int h = 0; h < size.Height; h++)
                {
                    blocks[w,h] = GetBestBlock(scaledImage.GetPixel(w, h));
                }
            }

            return blocks;
        }

        MCBlockState GetBestBlock(Color pixel)
        {
            Double minDiffScore = Double.MaxValue;
            MCBlockState bestBlock = null;
            foreach (KeyValuePair<String, Color> nameColor in averageColors)
            {
                Color c = nameColor.Value;
                Double diff = Math.Pow(pixel.R - c.R, 2) + Math.Pow(pixel.G - c.G, 2) + Math.Pow(pixel.B - c.B, 2);
                if (diff < minDiffScore)
                {
                    bestBlock = resourcePack.getBlockState(nameColor.Key);
                    minDiffScore = diff;
                }
            }

            return bestBlock;
        }
    }
}
