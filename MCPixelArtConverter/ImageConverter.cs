using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    interface ImageConverter
    {
        MCBlockState[,] Convert(Bitmap image, Size size);
    }
}
