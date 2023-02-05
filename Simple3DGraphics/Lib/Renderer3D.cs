using Simple3DGraphics.Lib.Model;
using Simple3DGraphics.Lib.Shape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Simple3DGraphics.Lib.Mathematics;
using System.Threading.Tasks;

namespace Simple3DGraphics.Lib
{
    public class Renderer3D
    {
        public Scene3D Scene { get; set; } = Scene3D.DefaultScene();

        private Mat4x4 projMat;
        
        float viewDistanceNear = 0.1f; 
        float viewDistanceFar = 1000f;
        float fovDeg = 120;
        public Renderer3D()
        {
        }

        private void FillTriangle(Graphics g, Triangle triangle)
        {
            using (Brush brush = new SolidBrush(triangle.color))
            //using (Pen brush = new Pen(triangle.color))
            {
                g.FillPath(brush, triangle.ToXY());
            }
        }

        float fTheta = 0;
        Mat4x4 matRotZ = new Mat4x4(), matRotX = new Mat4x4();
        private void UpdateRotation(float deltaTimeSecs)
        {

            fTheta += 1.0f * deltaTimeSecs;

            // Rotation Z
            matRotZ.m[0][0] = (float)Math.Cos(fTheta);
            matRotZ.m[0][1] = (float)Math.Sin(fTheta);
            matRotZ.m[1][0] = (float)-Math.Sin(fTheta);
            matRotZ.m[1][1] = (float)Math.Cos(fTheta);
            matRotZ.m[2][2] = 1;
            matRotZ.m[3][3] = 1;

            // Rotation X
            matRotX.m[0][0] = 1;
            matRotX.m[1][1] = (float)Math.Cos(fTheta * 0.5f);
            matRotX.m[1][2] = (float)Math.Sin(fTheta * 0.5f);
            matRotX.m[2][1] = (float)-Math.Sin(fTheta * 0.5f);
            matRotX.m[2][2] = (float)Math.Cos(fTheta * 0.5f);
            matRotX.m[3][3] = 1;
        }

        public void DrawScene(Graphics g, Size screenSize, float deltaTimeSecs)
        {
            if (screenSize.Height < 3 || screenSize.Width < 3)
            {
                return;
            }

            float width = screenSize.Width;
            float height = screenSize.Height;
            float aspectRatio = height / width;
            List<Triangle> projectedTriangles = new List<Triangle>();

            Scene.Camera.UpdateWorldMat();

            projMat = Math3D.GetProjectionMatrix(viewDistanceNear, viewDistanceFar, fovDeg, aspectRatio);
            UpdateRotation(deltaTimeSecs);

            Parallel.ForEach(Scene.Shapes, (shape) =>
            {
                foreach (Triangle tri in shape.GetTriangles())
                {
                    // Rotate in Z-Axis
                    Triangle target = tri.ProjectTo(Scene.Camera.WorldMatrix);

                    // Rotate in X-Axis
                    target = target.ProjectTo(matRotX);

                    // Translate triangle
                    target = target.Add(shape.GetPosition());

                    // Filter out unvisible sides.
                    if (!Scene.Camera.IsVisible(target))
                    {
                        continue;
                    }

                    float illumination = Math3D.Dot(target.GetNormal(), Scene.LightDirection.Normalize());
                    target.color = Utils.DeriveColor(target.color, illumination < 0.1 ? 0.1f : illumination);

                    // World2View
                    target = target.ProjectTo(Scene.Camera.ViewMatrix);

                    // 3D ---> 2D
                    target = target.ProjectTo(projMat);
                    target = target.ScaleToScene(width * 0.5f, height * 0.5f);
                    lock (projectedTriangles)
                    {
                        projectedTriangles.Add(target);
                    }
                }
            });

            projectedTriangles.Sort((a, b) => 
            {
                if(a == null || b == null)
                {
                    return -1;
                }
                return a.AvgOfZ() > b.AvgOfZ() ? -1 : 1;
            });

            //g.Clear(Color.White);
            foreach (Triangle shape in projectedTriangles)
            {
                FillTriangle(g, shape);
            }
        }
    }
}
