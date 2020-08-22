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
        Snake _snake;
        Fruit _fruit;
        DispatcherTimer _time;
        int _score = 0;
        public MainWindow()
        {
            InitializeComponent();
            _fruit = new Fruit();
            _snake = new Snake();
            _time = new DispatcherTimer();
            _time.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _time.Tick += Time_Tick;
        }
        private void Time_Tick(object sender, EventArgs e)
        {
            _snake.Move();
            SetSnakeInCanvas();
            Debug.WriteLine($"Snake : {_snake.Tail[0].X} {_snake.Tail[0].Y}");
            Debug.WriteLine($"Fruit : {_fruit.FruitCoordinate.X} {_fruit.FruitCoordinate.Y}");
            Debug.WriteLine($"Tail : {_snake.Tail.Count}");

            if (_snake.Tail[0].X == _fruit.FruitCoordinate.X && _snake.Tail[0].Y == _fruit.FruitCoordinate.Y)
            {
                _snake.Tail.Add(new Coordinate(_snake.Tail[0].X, _snake.Tail[0].Y));
                myCanvas.Children.RemoveAt(0);
                _fruit = new Fruit();
                SetFruitInCanvas();
            }

            var lenghtToRemove = 0;
            for (int i = 0; i < myCanvas.Children.Count; i++)
            {
                if (myCanvas.Children[i] is Rectangle)
                    lenghtToRemove++;
            }
            lenghtToRemove -= _snake.Tail.Count;
            myCanvas.Children.RemoveRange(1, lenghtToRemove);

            lenghtToRemove = 0;
        }
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetSnakeInCanvas();
            SetFruitInCanvas();
            _time.Start();
        }
        private void SetFruitInCanvas()
        {
            myCanvas.Children.Insert(0, _fruit.Circle);
        }
        private void SetSnakeInCanvas()
        {
            _snake.DrawSnakeHead();
            myCanvas.Children.Add(_snake.Rec);
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
    }
}