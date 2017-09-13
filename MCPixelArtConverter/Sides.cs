using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    enum Sides
    {
        Down, //Negative y <0, -1, 0>
        Up, //Positive y <0, 1, 0>
        North, //Negative z <0, 0, -1>
        South, //Positive z <0, 0, 1>
        West, //Negative x <-1, 0, 0>
        East //Positive x <1, 0, 0>
    }
}
