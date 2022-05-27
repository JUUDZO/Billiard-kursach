using System;

namespace Billiards
{
    public class Vector
    {
        public readonly static Vector zero = new Vector(0, 0, 0);
        private double x, y, z;

        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public Vector()
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
        }

        public Vector(double x1, double y1, double z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }

        public double Length()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public Vector Unit()
        {
            double length = Math.Sqrt(x * x + y * y + z * z);
            return new Vector(x / length, y / length, z / length);
        }


        public static Vector operator *(Vector v1, double k)
        {
            return new Vector(k * v1.x, k * v1.y, k * v1.z);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
    }
}
