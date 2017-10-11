using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class RotationMatrix
    {
        //3x3 matrix to rotate 3D points. Since we always use 90/180/270 degree rotation, we can use int
        public int[,] V { get; } = new int[3, 3];

        private static double RadianFromDegree(int deg)
        {
            return deg * Math.PI / 180;
        }

        private RotationMatrix()
        { }

        private RotationMatrix(int[, ] v)
        {
            V = v;
        }

        private static RotationMatrix RotateX(int x)
        {
            double xRad = RadianFromDegree(x);
            RotationMatrix Rx = new RotationMatrix(new int[3,3]
                                                   { { 1, 0, 0 },
                                                     { 0, (int)Math.Cos(xRad), (int)(-1 * Math.Sin(xRad)) },
                                                     { 0, (int)Math.Sin(xRad), (int)Math.Cos(xRad) } });
            return Rx;
        }

        private static RotationMatrix RotateY(int y)
        {
            double yRad = RadianFromDegree(y);
            RotationMatrix Ry = new RotationMatrix(new int[3, 3]
                                                   { { (int)Math.Cos(yRad), 0, (int)Math.Sin(yRad) },
                                                     { 0, 1, 0 },
                                                     { (int)(-1 * Math.Sin(yRad)), 0, (int)Math.Cos(yRad) }});
            return Ry;
        }

        private static RotationMatrix RotateZ(int z)
        {
            double zRad = RadianFromDegree(z);
            RotationMatrix Rz = new RotationMatrix(new int[3, 3]
                                                   { { (int)Math.Cos(zRad), (int)(-1 * Math.Sin(zRad)), 0},
                                                     { (int)Math.Sin(zRad), (int)Math.Cos(zRad), 0 },
                                                     { 0, 0, 1}});
            return Rz;
        }

        public RotationMatrix (int x, int y, int z)
        {
            RotationMatrix Rx, Ry, Rz;
            Rx = RotateX(x);
            Ry = RotateY(y);
            Rz = RotateZ(z);
            V = Rx.Multiply(Ry).Multiply(Rz).V;
        }

        public RotationMatrix Multiply(RotationMatrix other)
        {
            RotationMatrix result = new RotationMatrix();
            for (int i=0; i < 3; i++)
            {
                for (int j=0; j < 3; j++)
                {
                    result.V[i, j] = 0;
                    for (int k=0; k < 3; k++)
                    {
                        result.V[i, j] += this.V[i, k] * other.V[k, j];
                    }
                }
            }
            return result;
        }

        //TODO: P4 better place to put this common code? create separate class for this?
        public static RotateFlipType RotateFlipTypeFromDegrees(int rotate)
        {
            RotateFlipType rotation;
            switch (rotate)
            {
                case 90:
                    rotation = RotateFlipType.Rotate90FlipNone;
                    break;
                case 180:
                    rotation = RotateFlipType.Rotate180FlipNone;
                    break;
                case 270:
                    rotation = RotateFlipType.Rotate270FlipNone;
                    break;
                case 0:
                default:
                    rotation = RotateFlipType.RotateNoneFlipNone;
                    break;
            }

            return rotation;
        }
    }
}
