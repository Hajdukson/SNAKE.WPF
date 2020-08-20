using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SNAKEWPF.BL
{
    public class Snake
    {
        public int Length { get; private set; } = 5;
        public Direction Direction { get; set; } = Direction.Right;
        public Coordinate HeadPosition { get; private set; } = new Coordinate(1, 1);
        public List<Coordinate> Tail { get; private set; } = new List<Coordinate>();
        public List<Rectangle> Waz { get; set; } = new List<Rectangle>();
        public Snake()
        {

        }

        public void EatFruit()
        {   
            Length += 1;
            Rectangle newRectangle = new Rectangle
            {
                Height = 10,
                Width = 10,
                Fill = Brushes.Red
            };
            Waz.Add(newRectangle);
        }
        public void Move()
        {
            switch (Direction)
            {
                case Direction.Left:
                    HeadPosition.X--;
                    break;
                case Direction.Right:
                    HeadPosition.X++;
                    break;
                case Direction.Up:
                    HeadPosition.Y--;
                    break;
                case Direction.Down:
                    HeadPosition.Y++;
                    break;
            }
        }
        public void TailLogic()
        {
            Tail.Add(new Coordinate(HeadPosition.X, HeadPosition.Y));
            if (Tail.Count > Length)
            {
                var endTail = Tail.First();
                Tail.Remove(endTail);
            }
        }
    }
    public enum Direction { Left, Right, Up, Down }
}
