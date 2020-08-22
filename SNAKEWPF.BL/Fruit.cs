using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace SNAKEWPF.BL
{
    public class Fruit
    {
        public Coordinate FruitCoordinate { get; private set; }
        public Ellipse Circle { get; set; } = new Ellipse();
        public Fruit()
        {
            var rd = new Random();
            var x = (rd.Next(0, 37) * 10) + 1;
            var y = (rd.Next(0, 35) * 10) + 1;

            Circle.Width = Circle.Height = 10;
            Circle.Fill = Brushes.Blue;

            Canvas.SetLeft(Circle, x);
            Canvas.SetTop(Circle, y);

            FruitCoordinate = new Coordinate(x, y);

        }
    }
}
