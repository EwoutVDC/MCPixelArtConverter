using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class ColorDouble
    {
        public double A { get; private set; }
        public double R { get; private set; }
        public double G { get; private set; }
        public double B { get; private set; }



        public ColorDouble(double a, double r, double g, double b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public ColorDouble(ColorDouble color)
        {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public ColorDouble(System.Drawing.Color color)
        {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public static ColorDouble operator +(ColorDouble c1, ColorDouble c2)
        {
            return new ColorDouble(c1.A + c2.A, c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);
        }

        public static ColorDouble operator *(ColorDouble c, double d)
        {
            return new ColorDouble(c.A * d, c.R * d, c.G * d, c.B * d);
        }

        public static ColorDouble operator -(ColorDouble c1, ColorDouble c2) => c1 + (c2 * -1);

        public static ColorDouble operator /(ColorDouble c, double d) => c * (1 / d);

        public double Difference(ColorDouble targetColor)
        {
            return Math.Pow(targetColor.A - A, 2) +
                   Math.Pow(targetColor.R - R, 2) +
                   Math.Pow(targetColor.G - G, 2) +
                   Math.Pow(targetColor.B - B, 2);
        }

        public override string ToString()
        {
            return "(" + A + "," + R + "," + G + "," + B + ")";

        }
    }
}
