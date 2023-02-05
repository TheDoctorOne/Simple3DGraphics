using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simple3DGraphics.Lib
{
    public partial class View3DControl : UserControl
    {
        public Renderer3D renderer3D = new Renderer3D();
        public View3DControl()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        private void View3DControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            renderer3D.DrawScene(g, Size);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
