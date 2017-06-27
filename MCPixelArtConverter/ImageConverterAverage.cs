using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
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

        public ImageConverterAverage(MCResourcePack rp)
        {
            resourcePack = rp;
        }

        public MCBlockState[,] Convert(Bitmap image, Size size)
        {
            Bitmap scaledImage = new Bitmap(image, size);
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
            Int32 minDiffScore = Int32.MaxValue;
            MCBlockState bestBlock = null;
            foreach (string blockName in validBlocks)
            {

            }

            return bestBlock;
        }
    }
}
