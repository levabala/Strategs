using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Strategs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    
    public partial class MainWindow : Window
    {
        Window myW;
        Controller controller;
        Team RED;
        Canvas field;
        Rectangle selectRect;

        public MainWindow()
        {
            InitializeComponent();
            myW = (Window)FindName("Window");
            basePreparations();            
        }

        private void basePreparations()
        {
            controller = new Controller((int)myW.Width);
            RED = controller.CreateTeam(Color.FromRgb(255,0,0));
            for (int x = 0; x < 200; x += 7)
                for (int y = 0; y < 300; y += 7)
                    controller.CreateUnit(RED, new Unit(new PointF(x, y)));
            //RED.EverybodyMoveTo(new PointF(100f, 100f));

            int timeInterval = 10;
            var t = new System.Windows.Threading.DispatcherTimer();
            t.Tick += T_Tick;
            t.Interval = new TimeSpan(0,0,0,0,timeInterval);
            t.Start();

            initCanvas();

            field.PreviewMouseRightButtonUp += Field_MouseRightButtonUp;
            field.MouseDown += Field_MouseDown;
            field.MouseUp += Field_MouseUp;

            myW.KeyDown += MyW_KeyDown;
        }

        int ci = 0;
        private void MyW_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.S:
                    controller.SelectEverybody();
                    break;
                case Key.Q:
                    controller.SelectColumn(ci);                    
                    ci++;
                    break;
                case Key.R:
                    controller.SelectRect(new PointF(0f, 0f), new PointF(100f, 50f));
                    break;
            }
        }

        Point startRectPoint, endRectPoint;
        bool rectStarted = false;
        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
                controller.ClearSelectedList();
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                startRectPoint = e.GetPosition(field);
                rectStarted = true;
            }                     
        }

        private void Field_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released && rectStarted)
            {
                endRectPoint = e.GetPosition(field);
                int diff = PointsDiff(startRectPoint, endRectPoint);
                if (diff > 5)
                {
                    controller.SelectRect(new PointF(startRectPoint), new PointF(endRectPoint));
                }
                rectStarted = false;
            }
        }

        private void Field_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(field);
            controller.SelectedMoveTo(new PointF((float)pos.X, (float)pos.Y));
        }

        private int PointsDiff(Point p1, Point p2)
        {
            return (int)(Math.Abs(p2.X - p1.X) + Math.Abs(p2.Y - p1.Y));
        }

        private void initCanvas()
        {
            field =  (Canvas)FindName("Field");
            foreach (Team t in controller.teams)
                foreach (int index in t.units)
                {                    
                    field.Children.Add(controller.units[index].ellipse);
                }                    
        }

        private void T_Tick(object sender, EventArgs e)
        {
            //field.Children.Clear();
            foreach (Team t in controller.teams)
            {
                controller.EverybodyFromMove(t);
                foreach (int index in t.units)
                {
                    Unit u = controller.units[index];
                    u.ellipse.Margin = new Thickness(u.position.x, u.position.y, 0, 0);                    
                }
            }               
        }                
    }
}
