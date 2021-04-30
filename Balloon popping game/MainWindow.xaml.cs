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
using System.Windows.Threading;

namespace Balloon_popping_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();

        int speed = 3;
        int intervals = 90;
        Random rand = new Random();

        List<Rectangle> itemRemover = new List<Rectangle>();

        ImageBrush backgroundImage = new ImageBrush();

        int balloonSkins;
        int missedBalloons;

        bool gameIsActive;

        int score;


        MediaPlayer player = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += GameEnigne;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/files/background.image.jpg"));
            MyCanvas.Background = backgroundImage;

            RestartGame();
        }

        private void GameEnigne(object sender, EventArgs e)
        {

            scoreText.Content = "Score" + score;

            intervals -= 10;

            if (intervals < 1)
            {
                ImageBrush balloonImage = new ImageBrush();

                balloonSkins += 1;
                if (balloonSkins > 5)
                {
                    balloonSkins = 1;
                }
                switch (balloonSkins)
                {
                    case 1:
                        balloonImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/files/balloon1.png"));
                        break;
                    case 2:
                        balloonImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/files/balloon2.png"));
                        break;
                    case 3:
                        balloonImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/files/balloon3.png"));
                        break;
                    case 4:
                        balloonImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/files/balloon4.png"));
                        break;
                    case 5:
                        balloonImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/files/balloon5.png"));
                        break;
                }
                Rectangle newBalloon = new Rectangle
                {
                    Tag = "ballooon",
                    Height = 50,
                    Width = 50,
                    Fill = balloonImage,
                };
                Canvas.SetLeft(newBalloon, rand.Next(50, 400));
                Canvas.SetTop(newBalloon, 600);

                MyCanvas.Children.Add(newBalloon);
                intervals = rand.Next(90, 150);
            }

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())

            {
                if ((string)x.Tag == "balloon")
                {
                    i = rand.Next(-5, 5);
                    Canvas.SetTop(x, Canvas.GetTop(x) - speed);
                    Canvas.SetLeft(x, Canvas.GetTop(x) - (i * speed));

                }
            }
        }

        private void PopBalloons(object sender, MouseButtonEventArgs e)
        {


        }
        private void StartGame()
        {
            gameTimer.Start();
            missedBalloons = 0;
            score = 0;
            intervals = 90; ;
            gameIsActive = true;
            speed = 3;

        }
        private void RestartGame()
        {

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                itemRemover.Add(x);
            }
            foreach (Rectangle y in itemRemover)
            {
                MyCanvas.Children.Remove(y); 
        }
            itemRemover.Clear();

            StartGame();

        }
    }
}
