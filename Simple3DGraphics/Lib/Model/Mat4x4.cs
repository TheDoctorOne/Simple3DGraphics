using System;

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


        public Mat4x4 Multiply(Mat4x4 m2)
        {
            Mat4x4 matrix = new Mat4x4();
            for (int c = 0; c < 4; c++)
                for (int r = 0; r < 4; r++)
                    matrix.m[r][c] = m[r][0] * m2.m[0][c] + m[r][1] * m2.m[1][c] + m[r][2] * m2.m[2][c] + m[r][3] * m2.m[3][c];
            return matrix;
        }


        public Vec3 Multiply(Vec3 i)
        {
            Vec3 v = new Vec3();
            v.X = i.X * m[0][0] + i.Y * m[1][0] + i.Z * m[2][0] + v.W * m[3][0];
            v.Y = i.X * m[0][1] + i.Y * m[1][1] + i.Z * m[2][1] + v.W * m[3][1];
            v.Z = i.X * m[0][2] + i.Y * m[1][2] + i.Z * m[2][2] + v.W * m[3][2];
            v.W = i.X * m[0][3] + i.Y * m[1][3] + i.Z * m[2][3] + v.W * m[3][3];
            return v;
        }
    }
}
