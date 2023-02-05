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
                g.FillPath(brush, mesh.ToXY(false));
            }
        }

        public void DrawScene(Graphics g, Size screenSize)
        {
            if(screenSize.Height < 3 || screenSize.Width < 3)
            {
                return;
            }

            float width = screenSize.Width;
            float height = screenSize.Height;
            float aspectRatio = height / width;
            projMat = Math3D.GetProjectionMatrix(viewDistanceNear, viewDistanceFar, fovDeg, aspectRatio);
            List<Mesh> projectedMeshes = new List<Mesh>();

            Parallel.ForEach(Scene.Shapes, (shape) =>
            {
                foreach (Mesh mesh in shape.GetMeshes())
                {
                    Mesh target;

                    target = mesh.ProjectTo(projMat, false);
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
