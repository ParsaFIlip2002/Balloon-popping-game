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
            

         

        }

        private void PopBalloons(object sender, MouseButtonEventArgs e)
        {

     
        }
        private void StartGame ()
        {

        }
        private void RestartGame()
        {

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                itemRemover.Add(x);
            }
            foreach ( Rectangle y in itemRemover)
            {
                MyCanvas.Children.Remove(y);            }
        }

    }
}
