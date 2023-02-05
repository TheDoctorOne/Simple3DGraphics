using Simple3DGraphics.Lib.Model;
using Simple3DGraphics.Lib.Shape;
using System;
using System.Collections.Generic;

namespace Simple3DGraphics.Lib
{
    public class Scene3D
    {
        public List<BaseShape> Shapes { get; } = new List<BaseShape>();
        public Camera3D Camera { get; } = new Camera3D();
        public Vec3 LightDirection { get; } = new Vec3(0, 0, -1);

        public static Scene3D DefaultScene()
        {
            Scene3D scene3D = new Scene3D();

            scene3D.Shapes.Add(new Rect3D(new Vec3(0.5f, 0.5f, 6), new Vec3(1, 1, 1)));
            scene3D.Shapes.Add(new Rect3D(new Vec3(0, 0, 5), new Vec3(1, 1, 1)));

            return scene3D;
        }
    }
}