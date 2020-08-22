using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

namespace SNAKEWPF.BL
{
    public class Snake
    {
        public List<Coordinate> Tail { get; set; } = new List<Coordinate>
        {
            new Coordinate(1,1)
        };
        public Direction Direction { get; set; } = Direction.Right;
        public Rectangle Rec{ get; private set;}
        public void DrawSnakeHead()
        {
            Rec = new Rectangle
            {
                Height = 10,
                Width = 10,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(Rec, Tail[0].X);
            Canvas.SetTop(Rec, Tail[0].Y);
        }
        public void Move()
        {
            switch (Direction)
            {
                case Direction.Left:
                    Tail[0].X -= 10;
                    break;
                case Direction.Right:
                    Tail[0].X += 10;
                    break;
                case Direction.Up:
                    Tail[0].Y -= 10;
                    break;
                case Direction.Down:
                    Tail[0].Y += 10;
                    break;
            }
        }
    }
    public enum Direction { Left, Right, Up, Down }

}
