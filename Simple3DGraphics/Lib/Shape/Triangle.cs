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
    public class Triangle
    {
        public Vec3[] pos;
        public Color color = Color.Black;

        public Triangle()
        {
            pos = new Vec3[3];
            pos[0] = new Vec3();
            pos[1] = new Vec3();
            pos[2] = new Vec3();
        }

        public Triangle(Vec3[] pos)
        {
            this.pos = new Vec3[3];
            this.pos[0] = pos[0];
            this.pos[1] = pos[1];
            this.pos[2] = pos[2];
        }

        public Triangle(float[] pos)
        {
            this.pos = new Vec3[3];
            this.pos[0] = new Vec3(pos[0], pos[1], pos[2]);
            this.pos[1] = new Vec3(pos[3], pos[4], pos[5]);
            this.pos[2] = new Vec3(pos[6], pos[7], pos[8]);
        }

        public Triangle SetColor(Color color)
        {
            this.color = color;
            return this;
        }

        public Triangle ScaleToScene(float width, float height)
        {
            Triangle tri = new Triangle();

            tri.pos[0] = pos[0].ScaleToScene(width, height);
            tri.pos[1] = pos[1].ScaleToScene(width, height);
            tri.pos[2] = pos[2].ScaleToScene(width, height);
            tri.color = color;

            return tri;
        }

        public Triangle ProjectTo(Mat4x4 projectionMatrix)
        {
            Triangle target = new Triangle();

            target.pos[0] = Math3D.MultiplyMatrixVector(pos[0], projectionMatrix);
            target.pos[1] = Math3D.MultiplyMatrixVector(pos[1], projectionMatrix);
            target.pos[2] = Math3D.MultiplyMatrixVector(pos[2], projectionMatrix);
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

        public Triangle Add(Vec3 add)
        {
            return Subtract(add, true);
        }

        public Triangle Subtract(Vec3 sub, bool add = false)
        {
            Triangle tri = new Triangle();

            tri.pos[0] = pos[0].Subtract(sub, add);
            tri.pos[1] = pos[1].Subtract(sub, add);
            tri.pos[2] = pos[2].Subtract(sub, add);
            tri.color = color;

            return tri;
        }

        public Triangle Subtract(Triangle sub, bool add = false)
        {
            Triangle tri = new Triangle();

            tri.pos[0] = pos[0].Subtract(sub.pos[0]);
            tri.pos[1] = pos[1].Subtract(sub.pos[1]);
            tri.pos[2] = pos[2].Subtract(sub.pos[2]);

            return tri;
        }

        public float AvgOfZ()
        {
            return (pos[0].Z + pos[1].Z + pos[2].Z) / 3f; 
        }

        public GraphicsPath ToXY()
        {
            if(pos.Length == 0)
            {
                return new GraphicsPath();
            }
            GraphicsPath res = new GraphicsPath();
            PointF first = new PointF();

            for (int i = 1; i<pos.Length; i++)
            {
                res.AddLine(pos[i - 1].GetXY(), pos[i].GetXY());

                if(i == 1)
                {
                    first = pos[i - 1].GetXY();
                }
            }

            res.AddLine(res.GetLastPoint(), first);

            return res;
        }
    }
}
