using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class ImageDithererErrorDiffuser : ImageDitherer
    {
        double[,] diffusionMatrix;
        int diffusionHeight;
        int diffusionWidth;

        ColorDouble[,] colorOffsets;
        Size size;

        public string Name { get; }

        //ONLY USE RISING HEIGTH
        public ImageDithererErrorDiffuser(string name, double[,] d)
        {
            diffusionMatrix = d;
            diffusionHeight = diffusionMatrix.GetLength(0);
            diffusionWidth = diffusionMatrix.GetLength(1);
            Name = name;
            //TODO: P2 handle exceptions function calls were Reset(s) has not been called yet
        }

        public void Reset(Size s)
        {
            size = s;
            colorOffsets = new ColorDouble[diffusionHeight, size.Width];
            for (int i = 0; i < diffusionHeight; i++)
            {
                for (int j = 0; j < size.Width; j++)
                {
                    colorOffsets[i, j] = new ColorDouble(0, 0, 0, 0);
                }
            }
        }

        int GetOffsetHeight(int h)
        {
            return h % diffusionHeight;
        }

        public void ApplyError(int w, int h, ColorDouble errorColor)
        {
            int offsetheight = GetOffsetHeight(h);
            if (w == 0)
            {
                //This is the first pixel for this row, clear previous offsets
                for (int i = 0; i < size.Width; i++)
                {
                    colorOffsets[offsetheight, i] = new ColorDouble(0, 0, 0, 0);
                }
            }

            for (int i = 0; i < diffusionHeight; i++)
            {
                for (int j = 0; j < diffusionWidth; j++)
                {
                    if (w - 2 + j >= 0 && w - 2 + j < size.Width && offsetheight + i < size.Height)
                    {
                        colorOffsets[GetOffsetHeight(offsetheight + i), w - 2 + j] += errorColor * diffusionMatrix[i, j];
                    }
                }
            }
        }

        public ColorDouble GetColorOffset(int w, int h)
        {
            int oh = GetOffsetHeight(h);
            return colorOffsets[oh, w];
        }

        public int CompareTo(ImageDitherer other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
