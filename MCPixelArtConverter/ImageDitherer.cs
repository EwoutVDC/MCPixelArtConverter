using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    interface ImageDitherer : IComparable<ImageDitherer>
    {
        void ApplyError(int w, int h, ColorDouble colorError);
        ColorDouble GetColorOffset(int w, int h);
        string Name { get; }
        void Reset(Size s);
    }
}
