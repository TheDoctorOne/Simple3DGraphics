using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Simple3DGraphics.Lib
{
    public class Utils
    {
        public static Color DeriveColor(Color src, float derivePercent)
        {
            if(derivePercent < 0)
            {
                derivePercent *= -1;
            }
            return Color.FromArgb(src.A, (int)(src.R * derivePercent), (int)(src.G * derivePercent), (int)(src.B * derivePercent));
        }
    }
}
