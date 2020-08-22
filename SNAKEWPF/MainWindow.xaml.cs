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
            myCanvas.Children.RemoveAt(1);

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
            Rectangle newRectangle = new Rectangle
            {
                Height = 10,
                Width = 10,
                Fill = Brushes.Red
            };

            Canvas.SetLeft(newRectangle, _snake.Tail[0].X);
            Canvas.SetTop(newRectangle, _snake.Tail[0].Y);
            myCanvas.Children.Add(newRectangle);
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