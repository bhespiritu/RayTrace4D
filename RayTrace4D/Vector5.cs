using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace4D
{
    public struct Vector5
    {
        public static Vector5 zero = new Vector5(0, 0, 0, 0, 0);

        public double x, y, z, w, v;

        public Vector5 toDir()
        {
            return new Vector5(x, y, z, w, 0);
        }

        public Vector5(double x, double y, double z, double w, double v)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
            this.v = v;
        }


        public Vector5 copy()
        {
            return new Vector5(x, y, z, w,v);
        }

        public Vector5 normalize() => this / magnitude();

        public double sqrMagnitude() => x * x + y * y + z * z + w * w + v*v;

        public double magnitude() => Math.Sqrt(sqrMagnitude());

        //dot product
        public static double operator *(Vector5 a, Vector5 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w + a.v * b.v;
        }

        //elementwise multiplication
        public static Vector5 operator ^(Vector5 a, Vector5 b)
        {
            return new Vector5(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w, a.v*b.v);
        }

        public static Vector5 operator *(Vector5 a, double b)
        {
            return new Vector5(a.x * b, a.y * b, a.z * b, a.w * b, a.v * b);
        }

        public static Vector5 operator *(double s, Vector5 v) => v * s;

        public static Vector5 operator /(Vector5 a, double b)
        {
            return new Vector5(a.x / b, a.y / b, a.z / b, a.w / b, a.v / b);
        }

        public static Vector5 operator /(Vector5 a, Vector5 b)
        {
            return new Vector5(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w, a.v / b.v);
        }


        public static Vector5 operator +(Vector5 a, Vector5 b)
        {
            return new Vector5(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w, a.v + b.v);
        }

        public static Vector5 operator -(Vector5 a, Vector5 b)
        {
            return new Vector5(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w, a.v - b.v);
        }


        public static Vector5 operator -(Vector5 a) => -1 * a;


        public override string ToString()
        {
            return "{" + x + "," + y + "," + z + "," + w + +',' + v + '}';
        }

    }
}
