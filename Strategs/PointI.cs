using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Strategs
{
    struct PointI
    {
        public int x, y;

        public PointI(int x, int y)
        {
            this.x = x;
            this.y = y;
        }      

        public override string ToString()
        {
            return "x:" + x + " y:" + y;
        }
    }
}
