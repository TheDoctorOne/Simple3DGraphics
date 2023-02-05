using Simple3DGraphics.Lib.Mathematics;
using Simple3DGraphics.Lib.Model;
using Simple3DGraphics.Lib.Shape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple3DGraphics.Lib
{
    public class Camera3D
    {
        public Vec3 Position { get; set; } = new Vec3();

        public bool IsVisible(Mesh target)
        {
            Vec3 normal = target.GetNormal().Normalize();

            return Math3D.Dot(normal, target.Subtract(Position).pos[0]) < 0;
        }
    }
}
