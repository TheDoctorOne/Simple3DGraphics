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

        private void FillMesh(Graphics g, Mesh mesh)
        {
            using (Brush brush = new SolidBrush(mesh.color))
            //using (Pen brush = new Pen(mesh.color))
            {
                g.FillPath(brush, mesh.ToXY());
            }
        }

        float fTheta = 0;
        Mat4x4 matRotZ = new Mat4x4(), matRotX = new Mat4x4();
        private void UpdateRotation(float elapsedTime)
        {

            fTheta += 0.2f;

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

        public void DrawScene(Graphics g, Size screenSize, float elapsedTime)
        {
            if(screenSize.Height < 3 || screenSize.Width < 3)
            {
                return;
            }

            float width = screenSize.Width;
            float height = screenSize.Height;
            float aspectRatio = height / width;
            List<Mesh> projectedMeshes = new List<Mesh>();

            projMat = Math3D.GetProjectionMatrix(viewDistanceNear, viewDistanceFar, fovDeg, aspectRatio);
            UpdateRotation(elapsedTime);

            Parallel.ForEach(Scene.Shapes, (shape) =>
            {
                foreach (Mesh mesh in shape.GetMeshes())
                {
                    // Rotate in Z-Axis
                    Mesh target = mesh.ProjectTo(matRotZ);

                    // Rotate in X-Axis
                    target = target.ProjectTo(matRotX);

                    // Translate Mesh
                    target = target.Add(shape.GetPosition());

                    Vec3 normal = target.GetNormal().Normalize();
                    if(normal.Z > 0)
                    {
                        continue;
                    }

                    // 3D ---> 2D
                    target = target.ProjectTo(projMat);
                    target = target.ScaleToScene(width * 0.5f, height * 0.5f);
                    lock (projectedMeshes)
                    {
                        projectedMeshes.Add(target);
                    }
                }
            });


            //g.Clear(Color.White);
            foreach (Mesh shape in projectedMeshes)
            {
                FillMesh(g, shape);
            }
        }
    }
}
