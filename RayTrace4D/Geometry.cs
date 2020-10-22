using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace4D
{
    public interface IRaytraceObject
    {
        double intersect(Ray r);

        Vector4 getColor(Vector5 pos);
        Vector5 getNormal(Vector5 pos);
    }

    struct Sphere : IRaytraceObject
    {
        public Vector5 center;
        public Vector4 color;
        public double radius;
        public Vector4 getColor(Vector5 pos)
        {
            return color;
        }

        public Vector5 getNormal(Vector5 pos)
        {
            return (pos - center) / radius;
        }

        public double intersect(Ray r)
        {
            Vector5 diff = center - r.origin;
            double r2 = radius * radius;
            double rMag = r.direction.sqrMagnitude();
            bool isInside = diff.sqrMagnitude() < r2;
            double tc = (diff * r.direction) / (rMag);
            if (!isInside && tc < 0) return -1;
            double d2 = ((r.origin + (tc * r.direction)) - center).sqrMagnitude();

            if (!isInside && d2 > r2) return -1;

            double tOff = Math.Sqrt(r2 - d2) / Math.Sqrt(rMag);

            return tc + tOff * (isInside ? 1 : -1);
        }
    }
}
