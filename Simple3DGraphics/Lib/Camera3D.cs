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
        public Vec3 Orientation { get; set; } = new Vec3();
        public Vec3 LookDirection { get; private set; } = new Vec3();

        public Mat4x4 WorldMatrix { get; private set; } = new Mat4x4();
        public Mat4x4 ViewMatrix { get; private set; } = new Mat4x4();


        public void UpdateWorldMat()
        {
            Mat4x4 matRotZ = Math3D.MatRotationZ(Orientation.Z);
            Mat4x4 matRotX = Math3D.MatRotationX(Orientation.X);

            Mat4x4 matTrans;
            matTrans = Math3D.MatTranslation(Position);

            WorldMatrix = Math3D.MatIdentity();   // Form World Matrix
            WorldMatrix = matRotZ.Multiply(matRotX); // Transform by rotation
            WorldMatrix = WorldMatrix.Multiply(matTrans); // Transform by translation

            // Create "Point At" Matrix for camera
            Vec3 vUp = new Vec3(0, 1, 0);
            Vec3 vTarget = new Vec3(0, 0, 1);
            Mat4x4 matCameraRot = Math3D.MatRotationY(Orientation.Y); // Yaw
            LookDirection = matCameraRot.Multiply(vTarget);
            vTarget = Position.Add(LookDirection);
            Mat4x4 matCamera = Math3D.PointAt(Position, vTarget, vUp);

            // Make view matrix from camera
            ViewMatrix = Math3D.MatQuickInverse(matCamera);
        }

        public bool IsVisible(Triangle target)
        {
            Vec3 normal = target.GetNormal().Normalize();

            return Math3D.Dot(normal, target.Subtract(Position).pos[0]) < 0;
        }
    }
}
