using Simple3DGraphics.Lib.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Simple3DGraphics.Lib.Shape
{
    public interface BaseShape
    {
        Vec3 GetPosition();
        IEnumerable<Mesh> GetMeshes();
    }
}
