using Simple3DGraphics.Lib.Mathematics;
using Simple3DGraphics.Lib.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Simple3DGraphics.Lib.Shape
{
    public class Mesh
    {
        public Vec3[] pos;
        public Color color = Color.Black;

        public Mesh()
        {
            pos = new Vec3[3];
            pos[0] = new Vec3();
            pos[1] = new Vec3();
            pos[2] = new Vec3();
        }

        public Mesh(Vec3[] pos)
        {
            this.pos = new Vec3[3];
            this.pos[0] = pos[0];
            this.pos[1] = pos[1];
            this.pos[2] = pos[2];
        }

        public Mesh(float[] pos)
        {
            this.pos = new Vec3[3];
            this.pos[0] = new Vec3(pos[0], pos[1], pos[2]);
            this.pos[1] = new Vec3(pos[3], pos[4], pos[5]);
            this.pos[2] = new Vec3(pos[6], pos[7], pos[8]);
        }

        public Mesh SetColor(Color color)
        {
            this.color = color;
            return this;
        }

        public Mesh ScaleToScene(float width, float height)
        {
            Mesh mesh = new Mesh();

            mesh.pos[0] = pos[0].ScaleToScene(width, height);
            mesh.pos[1] = pos[1].ScaleToScene(width, height);
            mesh.pos[2] = pos[2].ScaleToScene(width, height);
            mesh.color = color;

            return mesh;
        }

        public Mesh ProjectTo(Mat4x4 projectionMatrix, bool normalize)
        {
            Mesh target = new Mesh();

            target.pos[0] = Math3D.MultiplyMatrixVector(normalize ? pos[0].Normalize() : pos[0], projectionMatrix);
            target.pos[1] = Math3D.MultiplyMatrixVector(normalize ? pos[1].Normalize() : pos[1], projectionMatrix);
            target.pos[2] = Math3D.MultiplyMatrixVector(normalize ? pos[2].Normalize() : pos[2], projectionMatrix);
            target.color = color;

            return target;
        }

        public Vec3 GetNormal()
        {
            return Math3D.Cross(
                pos[1].Subtract(pos[0])
                , 
                pos[2].Subtract(pos[0])
                );
        }

        public GraphicsPath ToXY(bool normalized)
        {
            if(pos.Length == 0)
            {
                return new GraphicsPath();
            }
            GraphicsPath res = new GraphicsPath();
            PointF first = new PointF();

            for (int i = 1; i<pos.Length; i++) 
            {
                if (normalized)
                {
                    res.AddLine(pos[i - 1].Normalize().GetXY(), pos[i].Normalize().GetXY());
                } 
                else
                {
                    res.AddLine(pos[i - 1].GetXY(), pos[i].GetXY());
                }

                if(i == 1)
                {
                    first = normalized ? pos[i - 1].Normalize().GetXY() : pos[i - 1].GetXY();
                }
            }

            res.AddLine(res.GetLastPoint(), first);

            return res;
        }
    }
}
