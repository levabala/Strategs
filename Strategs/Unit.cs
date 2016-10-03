using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Strategs
{
    class Unit
    {
        public PointF position;        
        public Vector moving;
        public Path path = new Path();
        public int pathI = 0;
        public int speed = 1;
        public float dx, dy;
        public Ellipse ellipse = new Ellipse();
        public int id;
        public PointI sortedPos;
        public Unit(PointF pos)
        {
            position = pos;
            moving = new Vector(pos, pos);

            ellipse.Fill = Brushes.Red;
            ellipse.Width = ellipse.Height = 4;
            ellipse.Margin = new Thickness(pos.x, pos.y, 0, 0);
            ellipse.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;                     
        }                

        public void MoveTo(PointF p)
        {
            moving.changeLength(0);
            path = new Path(position, p);
            VectorToPoint(p);            
        }
        public void MoveByPath()
        {
            if (path.points.Count == 0) return;
            if (Math.Abs(position.x - path.end.x) + Math.Abs(position.y - path.end.y) < 2)
            {
                path = new Path();
                moving.changeLength(0);
                return;
            }
            moving.NextPoint();
            dx = moving.start.x - position.x;
            dy = moving.start.y - position.y;
            position = moving.start;                        
            /*if (path.points.Count == 0) return;
            if (pathI > path.points.Count - 1)
            {
                path = new Path();
                pathI = 0;
                return;
            }

            pathI++;
            BigPosition = new Point((int)position.X, (int)position.Y);
            if (BigPosition != )*/
        }

        public void VectorToPoint(PointF p)
        {
            moving = new Vector(position, p);
            moving.changeLength(speed);
        }
        public void DeSelect()
        {
            ellipse.Fill = Brushes.Red;
        }

        public event EventHandler Selected;        
        protected virtual void OnSelected(EventArgs e)
        {
            if (Selected != null)
                Selected(this, e);
            else return;

            ellipse.Fill = Brushes.GreenYellow;
        }

        public void Select()
        {
            OnSelected(new EventArgs());
        }

        private void Ellipse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnSelected(new EventArgs());
        }        
    }
}
