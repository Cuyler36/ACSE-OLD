using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Animal_Crossing_GCN_Save_Editor
{
    class DrawGrid
    {
        public void MakeGrid(int numXCells, int numYCells, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int rectSize = 8;
            //Rectangle r = new Rectangle(0, 0, 80 * 8, 92 * 8);
            //Size s = new Size(8, 8);
            //ControlPaint.DrawGrid(g, r, s, Color.White);
            Pen p = new Pen(Color.Black);
            for (int y = 0; y <= numYCells; y++)
                g.DrawLine(p, 0, y * rectSize, numXCells * rectSize, y * rectSize);
            for (int x = 0; x <= numXCells; x++)
                g.DrawLine(p, x * rectSize, 0, x * rectSize, numYCells * rectSize);
        }
    }
}
