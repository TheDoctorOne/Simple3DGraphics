using Simple3DGraphics.Lib.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Simple3DGraphics.Lib.Shape
{
    public class Rect3D : BaseShape
    {
        private Vec3 loc;
        private Triangle[] triangles;

        public static readonly Color[] DEFAULT_SIDE_COLORS = new Color[] { 
            Color.Blue,     // South
            Color.Red,      // East
            Color.Violet,   // North
            Color.Yellow,   // West
            Color.Magenta,  // Top
            Color.Aqua,     // Bottom 
        };

        private void SetTriangles(Vec3 pos, Vec3 size, Color[] sideColors = null)
        {
            if (sideColors == null)
            {
                sideColors = DEFAULT_SIDE_COLORS;
            }
            float posXs = pos.X + size.X;
            float posYs = pos.Y + size.Y;
            float posZs = pos.Z + size.Z;
            triangles = new Triangle[]
                { 
                    // South
                    new Triangle(new float[]{ pos.X, pos.Y, pos.Z /**/ , pos.X, posYs, pos.Z /**/, posXs, posYs, pos.Z }).SetColor(sideColors[0]),
                    new Triangle(new float[]{ pos.X, pos.Y, pos.Z /**/ , posXs, posYs, pos.Z /**/, posXs, pos.Y, pos.Z }).SetColor(sideColors[0]),
                    
                    // East
                    new Triangle(new float[]{ posXs, pos.Y, pos.Z /**/ , posXs, posYs, pos.Z /**/, posXs, posYs, posZs }).SetColor(sideColors[1]),
                    new Triangle(new float[]{ posXs, pos.Y, pos.Z /**/ , posXs, posYs, posZs /**/, posXs, pos.Y, posZs }).SetColor(sideColors[1]),
                    
                    // North
                    new Triangle(new float[]{ posXs, pos.Y, posZs /**/ , posXs, posYs, posZs /**/, pos.X, posYs, posZs }).SetColor(sideColors[2]),
                    new Triangle(new float[]{ posXs, pos.Y, posZs /**/ , pos.X, posYs, posZs /**/, pos.X, pos.Y, posZs }).SetColor(sideColors[2]),
                    
                    // West
                    new Triangle(new float[]{ pos.X, pos.Y, posZs /**/ , pos.X, posYs, posZs /**/, pos.X, posYs, pos.Z }).SetColor(sideColors[3]),
                    new Triangle(new float[]{ pos.X, pos.Y, posZs /**/ , pos.X, posYs, pos.Z /**/, pos.X, pos.Y, pos.Z }).SetColor(sideColors[3]),
                    
                    // Top
                    new Triangle(new float[]{ pos.X, posYs, pos.Z /**/ , pos.X, posYs, posZs /**/, posXs, posYs, posZs }).SetColor(sideColors[4]),
                    new Triangle(new float[]{ pos.X, posYs, pos.Z /**/ , posXs, posYs, posZs /**/, posXs, posYs, pos.Z }).SetColor(sideColors[4]),
                    
                    // Bottom
                    new Triangle(new float[]{ posXs, pos.Y, posZs /**/ , pos.X, pos.Y, posZs /**/, pos.X, pos.Y, pos.Z }).SetColor(sideColors[5]),
                    new Triangle(new float[]{ posXs, pos.Y, posZs /**/ , pos.X, pos.Y, pos.Z /**/, posXs, pos.Y, pos.Z }).SetColor(sideColors[5]),
                };
        }

        public Rect3D(Vec3 loc, Vec3 size, Color[] sideColors = null)
        {
            this.loc = loc;
            SetTriangles(new Vec3(), size, sideColors);
        }

        public IEnumerable<Triangle> GetTriangles()
        {
            return triangles;
        }

        public Vec3 GetPosition()
        {
            return loc;
        }
    }
}
