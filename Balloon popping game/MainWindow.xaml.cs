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

        DispatcherTimer gameTimer = new DispatcherTimer(); //timer

        int speed = 3; //hastigheten på ballonger uppåt
        int intervals = 90; // hur ofta en balloong skapas
        Random rand = new Random(); //Slumptal

        List<Rectangle> itemRemover = new List<Rectangle>(); //Ballonger som åkt förbi toppen kommer att lagras i itemRemover

        ImageBrush backgroundImage = new ImageBrush(); //Importerar backgrund bild

        int balloonSkins; // deklarerar
        int i; // deklarerar
        int missedBalloons;// deklarerar

        bool gameIsActive;// deklarerar

        int score;// deklarerar


        MediaPlayer player = new MediaPlayer();


        /// <summary>
        /// GameEnigne körs varje gång intervalet har uppnåts på gameTimer, intervalet är 20
        /// 
        /// importerar en bild om används som backgrundsbild
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += GameEnigne; // körs varje gång intervalet har uppnåts på gameTimer
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); //intervalet är 20

            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/files/background-Image.jpg")); //navigerar vart bilden är 
            MyCanvas.Background = backgroundImage; // använder den som backgrundsbild

            RestartGame();
        }
        /// <summary>
        /// Metoden GameEnigne skapar en rectangle och då skickas bilden på balloonsen till rectangeln och med hjälp av if satser så bestämms det hur ofta dom ska produceras. 
        /// Metoden bestämmer också vart balloonsen ska produceras 
        /// Metoden håller också koll på hur många man missat och om man missar 10 st så förlorar man 
        /// Speed ökar om man får mer än tre Score
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameEnigne(object sender, EventArgs e)
        {

          
            scoreText.Content = "Score" + score; // skapar en text som det står Score på och en variable som heter score

            intervals -= 10; 

            if (intervals < 1) // körs när intervals är högre än 1 
            {
                ImageBrush balloonImage = new ImageBrush(); // ny Imagebrush skapas

                balloonSkins += 1; // lägger till 1 på inten

                if (balloonSkins > 5) // om ballonSkins blir mer än 5 så körs inte if satsen
                {
                    balloonSkins = 1;
                }
                switch (balloonSkins) // swithc case för att välja färg på ballongen 
                {
                    case 1:
                        balloonImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/files/balloon1.png")); //importerar bilden
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
                Rectangle newBalloon = new Rectangle // en rectangle som en bild kan läggas in
                {
                    Tag = "balloon",
                    Height = 50,
                    Width = 50,
                    Fill = balloonImage, // fyller Rectangle med en bild som valts i switch casen
                };
                Canvas.SetLeft(newBalloon, rand.Next(50, 400)); // newBalloon kommer att sättas på ett random nummer mellan 50-400
                Canvas.SetTop(newBalloon, 600); // //max värdet på NewBalloon är 600 (Lodrätt)

                MyCanvas.Children.Add(newBalloon); // Lägger till objektet
                 
                intervals = rand.Next(90, 150); //Resetar intervalen 
            }

            foreach (var x in MyCanvas.Children.OfType<Rectangle>()) //kollar om  Rectangle finns i MyCanvas

            {
                if ((string)x.Tag == "balloon") //"balloon" är en tag, letar efter Rectangle som har tagen balloon
                {
                   
                    i = rand.Next(-5, 5); // generera ett nummer mellan talen
                    Canvas.SetTop(x, Canvas.GetTop(x) - speed); // positionering på ballongerna 
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - (i * -1)); //positionering på ballongerna

                }

                if (Canvas.GetTop(x) < 20) // om GetTop mlir mer än 20 så körs if satsen
                {
                    itemRemover.Add(x); // lägger till Canvas i itemRemover
                    missedBalloons += 1; // plussar 1 i missedBalloons
                }
            }
            foreach (Rectangle y in itemRemover) // kollar om
            {
                MyCanvas.Children.Remove(y); // tar bort Rectanglen som finns i itemRemover
            }

            if (missedBalloons > 10) // om man missar 10 balloons så körs denna if satsen, en text skickas ut att du förlora och restar sedan spelet
            {
                gameIsActive = false; 

                gameTimer.Stop(); // stoppar timern
                MessageBox.Show("Game over! You missed 10 balloons" + Environment.NewLine + "Click to play again");

                RestartGame();
            }

            if (score > 3) // om score blir mer än 3 så ökas speed 
            {
                speed = 7;
            }

        }
        /// <summary>
        ///  Spelar upp ett mp3 ljud när man klickar på en ballong och adderar ett poäng i score när det sker. 
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopBalloons(object sender, MouseButtonEventArgs e)
        {
            if (gameIsActive) // när speleet spelas så körs denna 
            {

                if (e.OriginalSource is Rectangle) // körs när man klickar på en Rectangle(Balloon)
                {
                    Rectangle activeRec = (Rectangle)e.OriginalSource; // Lokal Rectangle

                    player.Open(new Uri("../../files/pop_sound.mp3", UriKind.RelativeOrAbsolute)); //importerar mp3 
                    player.Play();

                    MyCanvas.Children.Remove(activeRec); // tar bort den lokala Rectanglen 

                    score += 1; // adderar med 1 i score
                }
            }

        }

        /// <summary>
        /// Startar spelet och resetar alla värden
        /// </summary>
        private void StartGame()
        {
            gameTimer.Start(); // startar timer

            missedBalloons = 0;
            score = 0;
            intervals = 90; 
            gameIsActive = true;
            speed = 3;

        }

        /// <summary>
        /// Tar bort alla REctanglar och kör sedan metoden StartGame
        /// </summary>
        private void RestartGame()
        {

            foreach (var x in MyCanvas.Children.OfType<Rectangle>()) // kollar om  Rectangle finns i MyCanvas 
            {
                itemRemover.Add(x); // om  Rectangle finns i MyCanvas så läggs den till i itemRemover
            }
            foreach (Rectangle y in itemRemover) // kollar om  Rectangle finns i itemRemover 
            {
                MyCanvas.Children.Remove(y);  // Tar bort om Rectangle finns i itemRemover 
            }
            itemRemover.Clear(); //Resetar 

            StartGame(); // kör metoden StartGame

        }
    }
}
