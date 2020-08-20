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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.ComponentModel;
using SNAKEWPF.BL;
using System.IO;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows.Threading;
using System.Threading;

namespace SNAKEWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _time;
        Fruit _fruit;
        Snake _snake;
        //bool Exit = false;
        //private int points = 0;
        private bool outOfRange = false;
        private bool GameOver
        {
            get
            {
                return (_snake.Tail.Where(c => c.X == _snake.HeadPosition.X
                && c.Y == _snake.HeadPosition.Y).ToList().Count > 1) || outOfRange;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            _time = new DispatcherTimer();

            _snake = new Snake();
            _fruit = new Fruit();
            
            mycanvas.Children.Insert(0, _fruit.Circle);
            SetSnakeInCanvas();
            
            myImage.Source = RandomImageSource();
            _time.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _time.Tick += Time_Tick;
            
        }
        private void SetSnakeInCanvas()
        {
            Rectangle newRectangle = new Rectangle
            {
                Height = 10,
                Width = 10,
                Fill = Brushes.Red
            };

            _snake.Waz.Add(newRectangle);

            Canvas.SetLeft(_snake.Waz.ElementAt(0), _snake.HeadPosition.X);
            Canvas.SetTop(_snake.Waz.ElementAt(0), _snake.HeadPosition.Y);

            mycanvas.Children.Insert(0, _snake.Waz.ElementAt(0));
        }
        private ImageSource RandomImageSource()
        {
            var urlList = new List<Uri>
            {
                new Uri("https://cdn.pixabay.com/photo/2015/02/28/15/25/snake-653639_1280.jpg"),
                new Uri("https://cdn.pixabay.com/photo/2014/11/23/21/22/green-tree-python-543243_1280.jpg"),
                new Uri("https://cdn.pixabay.com/photo/2014/10/25/07/52/king-snake-502263_1280.jpg"),
                new Uri("https://cdn.pixabay.com/photo/2015/02/28/15/25/rattlesnake-653642_1280.jpg"),
                new Uri("https://cdn.pixabay.com/photo/2015/02/28/15/25/rattlesnake-653646_1280.jpg")
            };

            var random = new Random();
            var drawnImg = random.Next(0, urlList.Count - 1);
            var bitmap = new BitmapImage(urlList.ElementAt(drawnImg));

            return bitmap;
        }
        private void Time_Tick(object sender, EventArgs e)
        {
            _snake.Move();
        }

        //private bool FruitRespOnSnakeTail(Fruit fruit)
        //{
        //    foreach (Coordinate tailElement in _snake.Tail)
        //    {
        //        if (tailElement.X == fruit.FruitCoordinate.X && tailElement.Y == fruit.FruitCoordinate.Y)
        //            return true;
        //    }
        //    return false;
        //}
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    _snake.Direction = Direction.Up;
                    break;
                case Key.Left:
                    _snake.Direction = Direction.Left;
                    break;
                case Key.Down:
                    _snake.Direction = Direction.Down;
                    break;
                case Key.Right:
                    _snake.Direction = Direction.Right;
                    break;
            }
        }
    }
}