using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple3DGraphics.Lib.Model
{
    public class Mat4x4
    {
        public float[][] m;
        public Mat4x4()
        {
            m = new float[4][];
            for(int i=0; i<4 ;i++)
            {
                m[i] = new float[4];
            }
        }
    }
}
