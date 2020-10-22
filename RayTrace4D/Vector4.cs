using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTrace4D
{
    public struct Vector4
    {
        public static Vector4 zero = new Vector4(0, 0, 0, 0);

        public double x, y, z, w;
        public double r => x;
        public double g => y;
        public double b => z;
        public double a => w;

        public Vector4 toDir()
        {
            return new Vector4(x, y, z, 0);
        }

        public Vector4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4(string hex)
        {
            if (hex.StartsWith("#")) hex = hex.Substring(1);

            x = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255d;
            y = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255d;
            z = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255d;

            if (hex.Length > 6)
            {
                w = int.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) / 255d;
            }
            else
            {
                w = 1;
            }

        }

        public Vector4 copy()
        {
            return new Vector4(x, y, z, w);
        }

        public Vector4 normalize() => this / magnitude();

        public double sqrMagnitude() => x * x + y * y + z * z + w * w;

        public double magnitude() => Math.Sqrt(sqrMagnitude());

        //dot product
        public static double operator *(Vector4 a, Vector4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        //elementwise multiplication
        public static Vector4 operator ^(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        public static Vector4 operator *(Vector4 a, double b)
        {
            return new Vector4(a.x * b, a.y * b, a.z * b, a.w * b);
        }

        public static Vector4 operator *(double s, Vector4 v) => v * s;

        public static Vector4 operator /(Vector4 a, double b)
        {
            return new Vector4(a.x / b, a.y / b, a.z / b, a.w / b);
        }

        public static Vector4 operator /(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
        }


        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Vector4 cross(Vector4 a, Vector4 b)
        {
            return new Vector4(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x, 0);
        }

        public static Vector4 operator -(Vector4 a) => -1 * a;
        public Color toColor() => Color.FromArgb((int)(w * 255), (int)(x * 255), (int)(y * 255), (int)(z * 255));
        public static implicit operator Vector4(Color c)
        {
            Vector4 Col = new Vector4();
            Col.x = c.R / 255d;
            Col.y = c.G / 255d;
            Col.z = c.B / 255d;
            Col.w = c.A / 255d;
            return Col;
        }


        public override string ToString()
        {
            return "{" + x + "," + y + "," + z + "," + w + '}';
        }

    }
}
