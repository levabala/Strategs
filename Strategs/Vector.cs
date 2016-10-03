using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategs
{
    class Vector
    {
        public float length, alpha, dx, dy;
        public PointF start, end;        
        public Vector(PointF s, PointF e)
        {
            start = s;
            end = e;
            length = getLength();
            alpha = getAlpha();
        }
        public Vector(float len, float al, PointF s)
        {
            length = len;
            alpha = al;
            start = s;
            end = getEndPoint();
        }

        public void NextPoint()
        {
            start = end;
            end = new PointF(end.x + dx, end.y + dy);
        }

        public void rotateTo(float al)
        {
            alpha = al;
            end = getEndPoint();
        }

        public void changeLength(float len)
        {
            length = len;            
            end = getEndPoint();
            dx = end.x - start.x;
            dy = end.y - start.y;
        }
        public PointF getEndPoint()
        {
            PointF pf = new PointF((float)(this.length * Math.Cos(alpha) + start.x), (float)(this.length * Math.Sin(alpha) + start.y));            
            return pf;
        }

        public float getLength()
        {
            float lengthX = Math.Abs(end.x - start.x);
            float lengthY = Math.Abs(end.y - start.y);
            dx = lengthX;
            dy = lengthY;
            return (float)Math.Sqrt(lengthX * lengthX + lengthY * lengthY);
        }

        public float getAlpha()
        {
            float ddx = end.x - start.x;
            float ddy = end.y - start.y;
            if (ddx == 0f) return 0f;
            float angle = (float)Math.Atan(ddy / ddx);
            if ((ddx < 0 && ddy < 0) || (ddx < 0 && ddy >= 0)) angle = (float)(angle - Math.PI);
            //if (angle == null) return 0f;
            return angle;
        }
    }
}
