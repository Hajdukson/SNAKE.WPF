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
        Snake _snake = new Snake();
        Fruit _fruit = new Fruit();
        DispatcherTimer _time = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            snakeImage.Source = RandomImageSource();
            _time.Tick += Time_Tick;
        }
        private void Time_Tick(object sender, EventArgs e)
        {
            SetSnakeInCanvas();
            _snake.Move();

            txtScore.Text = (_snake.Lenght - 1).ToString();

            _snake.TailLogic();

            if (_snake.Position.X == _fruit.FruitCoordinate.X && _snake.Position.Y == _fruit.FruitCoordinate.Y)
            {
                _snake.Lenght++;
                myCanvas.Children.RemoveAt(0);
                _fruit = new Fruit();
                SetFruitInCanvas();
                snakeImage.Source = RandomImageSource();
            }

            var lengthToRemove = 0;
            for (int i = 0; i < myCanvas.Children.Count; i++)
            {
                if (myCanvas.Children[i] is Rectangle)
                    lengthToRemove++;
            }
            lengthToRemove -= _snake.Tail.Count;
            myCanvas.Children.RemoveRange(1, lengthToRemove);

            if(GameOver())
            {
                _snake.Lenght = 1;
                _snake.Tail.Clear();
                _snake.Position.X = _snake.Position.Y = 0;
                _snake.Tail.Add(_snake.Position);
                _snake.Direction = Direction.Right;
            }
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
            _time.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _time.Start();
            SetSnakeInCanvas();
            SetFruitInCanvas();
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
        private bool GameOver()
        {

            if (_snake.Position.X > 380 || _snake.Position.Y > 350 || _snake.Position.X < 0 || _snake.Position.Y < 0)
                return true;

            return _snake.Tail.Where(c => c.X == _snake.Position.X && c.Y == _snake.Position.Y).ToList().Count > 1;
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