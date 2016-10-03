using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategs
{
    class Path
    {
        public List<PointF> points;
        public PointF start, end;
        public Path()
        {
            points = new List<PointF>();
        }
        public Path(List<PointF> ps)
        {
            points = ps;
        }

        public Path(PointF start, PointF end)
        {
            points = createPath(start, end);
            this.start = start;
            this.end = end;
        }

        private List<PointF> createPath(PointF start, PointF end)
        {
            this.start = start;
            this.end = end;
            return new List<PointF>() { start, end };
        }
    }
}
