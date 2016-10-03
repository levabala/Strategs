using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Strategs
{
    struct PointF
    {
        public float x, y;

        public PointF(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public PointF(Point p)
        {
            x = (float)p.X;
            y = (float)p.Y;
        }

        public override string ToString()
        {
            return "x:" + x + " y:" + y;
        }
    }
}
