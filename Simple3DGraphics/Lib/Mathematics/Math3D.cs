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

        public static Mat4x4 PointAt(Vec3 pos, Vec3 target, Vec3 up)
        {
            // Calculate new forward direction
            Vec3 newForward = target.Subtract(pos);
            newForward = newForward.Normalize();

            // Calculate new Up direction
            Vec3 a = newForward.Scale(Dot(up, newForward));
            Vec3 newUp = up.Subtract(a).Normalize();

            Vec3 newRight = Cross(newUp, newForward);

            // Construct Dimensioning and Translation Matrix	
            Mat4x4 matrix = new Mat4x4();
            matrix.m[0][0] = newRight.X; matrix.m[0][1] = newRight.Y; matrix.m[0][2] = newRight.Z; matrix.m[0][3] = 0.0f;
            matrix.m[1][0] = newUp.X; matrix.m[1][1] = newUp.Y; matrix.m[1][2] = newUp.Z; matrix.m[1][3] = 0.0f;
            matrix.m[2][0] = newForward.X; matrix.m[2][1] = newForward.Y; matrix.m[2][2] = newForward.Z; matrix.m[2][3] = 0.0f;
            matrix.m[3][0] = pos.X; matrix.m[3][1] = pos.Y; matrix.m[3][2] = pos.Z; matrix.m[3][3] = 1.0f;
            return matrix;

        }

        public static Mat4x4 MatQuickInverse(Mat4x4 m) // Only for Rotation/Translation Matrices
        {
            Mat4x4 matrix = new Mat4x4();
            matrix.m[0][0] = m.m[0][0]; matrix.m[0][1] = m.m[1][0]; matrix.m[0][2] = m.m[2][0]; matrix.m[0][3] = 0.0f;
            matrix.m[1][0] = m.m[0][1]; matrix.m[1][1] = m.m[1][1]; matrix.m[1][2] = m.m[2][1]; matrix.m[1][3] = 0.0f;
            matrix.m[2][0] = m.m[0][2]; matrix.m[2][1] = m.m[1][2]; matrix.m[2][2] = m.m[2][2]; matrix.m[2][3] = 0.0f;
            matrix.m[3][0] = -(m.m[3][0] * matrix.m[0][0] + m.m[3][1] * matrix.m[1][0] + m.m[3][2] * matrix.m[2][0]);
            matrix.m[3][1] = -(m.m[3][0] * matrix.m[0][1] + m.m[3][1] * matrix.m[1][1] + m.m[3][2] * matrix.m[2][1]);
            matrix.m[3][2] = -(m.m[3][0] * matrix.m[0][2] + m.m[3][1] * matrix.m[1][2] + m.m[3][2] * matrix.m[2][2]);
            matrix.m[3][3] = 1.0f;
            return matrix;
        }

        public static Mat4x4 MatRotationX(float fAngleRad)
        {
            Mat4x4 matrix = new Mat4x4();
            matrix.m[0][0] = 1.0f;
            matrix.m[1][1] = (float)Math.Cos(fAngleRad);
            matrix.m[1][2] = (float)Math.Sin(fAngleRad);
            matrix.m[2][1] = -(float)Math.Sin(fAngleRad);
            matrix.m[2][2] = (float)Math.Cos(fAngleRad);
            matrix.m[3][3] = 1.0f;
            return matrix;
        }

        public static Mat4x4 MatRotationY(float fAngleRad)
        {
            Mat4x4 matrix = new Mat4x4();
            matrix.m[0][0] = (float)Math.Cos(fAngleRad);
            matrix.m[0][2] = (float)Math.Sin(fAngleRad);
            matrix.m[2][0] = -(float)Math.Sin(fAngleRad);
            matrix.m[1][1] = 1.0f;
            matrix.m[2][2] = (float)Math.Cos(fAngleRad);
            matrix.m[3][3] = 1.0f;
            return matrix;
        }

        public static Mat4x4 MatRotationZ(float fAngleRad)
        {
            Mat4x4 matrix = new Mat4x4();
            matrix.m[0][0] = (float)Math.Cos(fAngleRad);
            matrix.m[0][1] = (float)Math.Sin(fAngleRad);
            matrix.m[1][0] = -(float)Math.Sin(fAngleRad);
            matrix.m[1][1] = (float)Math.Cos(fAngleRad);
            matrix.m[2][2] = 1.0f;
            matrix.m[3][3] = 1.0f;
            return matrix;
        }

        public static Mat4x4 MatIdentity()
        {
            Mat4x4 matrix = new Mat4x4();
            matrix.m[0][0] = 1.0f;
            matrix.m[1][1] = 1.0f;
            matrix.m[2][2] = 1.0f;
            matrix.m[3][3] = 1.0f;
            return matrix;
        }

        public static Mat4x4 MatTranslation(Vec3 vec)
        {
            Mat4x4 matrix = new Mat4x4();
            matrix.m[0][0] = 1.0f;
            matrix.m[1][1] = 1.0f;
            matrix.m[2][2] = 1.0f;
            matrix.m[3][3] = 1.0f;
            matrix.m[3][0] = vec.X;
            matrix.m[3][1] = vec.Y;
            matrix.m[3][2] = vec.Z;
            return matrix;
        }
    }
}
