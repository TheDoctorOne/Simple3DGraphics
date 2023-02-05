using Simple3DGraphics.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple3DGraphics.Lib.Mathematics
{
    public class Math3D
    {
        public static Mat4x4 GetProjectionMatrix(float viewDistanceNear, float viewDistanceFar, float fov, float aspectRatio)
        {
            Mat4x4 mat = new Mat4x4();
            float fovRad = (float)(1 / Math.Tan(fov * 0.5 / 180f * Math.PI));
            
            mat.m[0][0] = aspectRatio * fovRad;
            mat.m[1][1] = fovRad;
            mat.m[2][2] = viewDistanceFar / (viewDistanceFar-viewDistanceNear);
            mat.m[3][2] = (-viewDistanceFar * viewDistanceNear) / (viewDistanceFar - viewDistanceNear);
            mat.m[2][3] = 1f;
            mat.m[3][3] = 0f;

            return mat;
        }

        public static Vec3 Cross(Vec3 line1, Vec3 line2)
        {
            Vec3 vec = new Vec3();

            vec.X = line1.Y * line2.Z - line1.Z * line2.Y;
            vec.Y = line1.Z * line2.X - line1.X * line2.Z;
            vec.Z = line1.X * line2.Y - line1.Y * line2.X;

            return vec;
        }

        public static Vec3 MultiplyMatrixVector(Vec3 vec, Mat4x4 mat)
        {
            Vec3 res = new Vec3();

            res.X = vec.X * mat.m[0][0] + vec.Y * mat.m[1][0] + vec.Z * mat.m[2][0] + mat.m[3][0];
            res.Y = vec.X * mat.m[0][1] + vec.Y * mat.m[1][1] + vec.Z * mat.m[2][1] + mat.m[3][1];
            res.Z = vec.X * mat.m[0][2] + vec.Y * mat.m[1][2] + vec.Z * mat.m[2][2] + mat.m[3][2];
            float w = vec.X * mat.m[0][3] + vec.Y * mat.m[1][3] + vec.Z * mat.m[2][3] + mat.m[3][3];

            if(w != 0)
            {
                res.X /= w;
                res.Y /= w;
                res.Z /= w;
            }

            return res;
        }

        public static float Dot(Vec3 v1, Vec3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
    }
}
