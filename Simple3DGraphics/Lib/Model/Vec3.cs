using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Simple3DGraphics.Lib.Model
{
    public struct Vec3
    {
        public float X;
        public float Y;
        public float Z;

        public float Norm() => (float)Math.Sqrt(X * X + Y * Y + Z * Z);

        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3 Normalize()
        {
            float norm = Norm();
            if(norm == 0)
            {
                norm = 1;
            }
            Vec3 normalized = new Vec3
            {
                X = X / norm,
                Y = Y / norm,
                Z = Z / norm
            };

            return normalized;
        }

        public Vec3 ScaleToScene(float width, float height)
        {
            return new Vec3()
            {
                X = (X + 1f) * width,
                Y = (Y + 1f) * height,
                Z = Z
            };
        }

        public Vec3 ScaleRaw(float scale)
        {
            return new Vec3()
            {
                X = (X) * scale,
                Y = (Y) * scale,
                Z = (Z) * scale
            };
        }

        public PointF GetXY()
        {
            return new PointF(X, Y);
        }

        public Vec3 Add(Vec3 vec)
        {
            return Subtract(vec, true);
        }

        public Vec3 Subtract(Vec3 vec, bool add = false)
        {
            Vec3 res = vec;
            if (!add)
            {
                res = res.ScaleRaw(-1);
            }
            return new Vec3()
            {
                X = X + res.X,
                Y = Y + res.Y,
                Z = Z + res.Z
            };
        }
    }
}
