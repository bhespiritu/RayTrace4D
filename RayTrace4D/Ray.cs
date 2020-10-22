using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace4D
{
    public struct Ray
    {
        public Vector5 origin;
        public Vector5 direction;
    }

    public struct RayResult
    {
        public Vector4 color;
        public bool didHit;
    }
}
