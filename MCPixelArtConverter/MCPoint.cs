using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class MCPoint
    {
        public float X;
        public float Y;
        public float Z;

        public MCPoint(JArray json)
        {
            X = float.Parse(json[0].ToString());
            Y = float.Parse(json[1].ToString());
            Z = float.Parse(json[2].ToString());
        }

        public MCPoint(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }

        /*
        Down, //Negative y <0, -1, 0>
        Up, //Positive y <0, 1, 0>
        North, //Negative z <0, 0, -1>
        South, //Positive z <0, 0, 1>
        West, //Negative x <-1, 0, 0>
        East //Positive x <1, 0, 0>
        */
        public static MCPoint FromSide(Sides side)
        {
            MCPoint point = new MCPoint(0,0,0);
            
            switch (side)
            {
                case Sides.Down:
                    point.Y = -1;
                    break;
                case Sides.Up:
                    point.Y = 1;
                    break;
                case Sides.North:
                    point.Z = -1;
                    break;
                case Sides.South:
                    point.Z = 1;
                    break;
                case Sides.West:
                    point.X = -1;
                    break;
                case Sides.East:
                    point.X = 1;
                    break;
            }

            return point;
        }

        private static bool isZero(float X)
        {
            return Math.Abs(X) < 0.0001;
        }
        
        public Sides ToSide()
        {
            //only 1 coordinate can have a value
            if ( (!isZero(X) && (!isZero(Y) ||!isZero(Z))) || (!isZero(Y) && !isZero(Z)))
                throw new ArgumentException(this + " is not along a cardinal axis.");
            if (isZero(X) && isZero(Y) && isZero(Z))
                throw new ArgumentException(this + " is not along a cardinal axis.");

            if (X > 0)
                return Sides.East;
            else if (X < 0)
                return Sides.West;
            else if (Y > 0)
                return Sides.Up;
            else if (Y < 0)
                return Sides.Down;
            else if (Z > 0)
                return Sides.South;
            else if (Z < 0)
                return Sides.North;

            throw new ApplicationException("This should never happen " + this);
        }

        public void Rotate(RotationMatrix R)
        {
            float xN = (float) (R.V[0, 0] * X + R.V[0, 1] * Y + R.V[0, 2] * Z);
            float yN = (float) (R.V[1, 0] * X + R.V[1, 1] * Y + R.V[1, 2] * Z);
            float zN = (float) (R.V[2, 0] * X + R.V[2, 1] * Y + R.V[2, 2] * Z);
            X = xN;
            Y = yN;
            Z = zN;
        }
        
        public override String ToString()
        {
            return "Point < " + X + "," + Y + "," + Z + " >";
        }
    }
}
