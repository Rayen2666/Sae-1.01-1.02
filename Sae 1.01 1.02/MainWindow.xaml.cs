using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Sae_1._01_1._02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // booléens pour monter et descendre
        bool goUp, goDown = false;
        int vitesseJoueur = 10;
        int vitesseEnnemi = 10;

         
        Random random = new Random();
        int speed1;
        int speed2;
        int speed3;
        int speed4;
        


        private ImageBrush playerSkin = new ImageBrush();

        ImageBrush enemie1Sprite = new ImageBrush();
        ImageBrush enemie2Sprite = new ImageBrush();
        ImageBrush enemie3Sprite = new ImageBrush();
        ImageBrush enemie4Sprite = new ImageBrush();
        ImageBrush ArrierePlanSprite = new ImageBrush();
        DispatcherTimer jeuTimer = new DispatcherTimer();
     
       
        

        public MainWindow()
        {
            InitializeComponent();
            // chargement de l’image du joueur 
            playerSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "formule1.png"));
            // assignement de skin du joueur au rectangle associé
            joueur.Fill = playerSkin;
            Menu fenetreDebut = new Menu();
            fenetreDebut.ShowDialog();

            ArrierePlanSprite.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/Fond.png"));
            ArrierePlan.Fill = ArrierePlanSprite;
            ArrierePlan2.Fill = ArrierePlanSprite;

            jeuTimer.Interval = TimeSpan.FromMilliseconds(17);
            jeuTimer.Tick += Jeu; 
            DebutJeu();
 
        }



        private void DebutJeu()
        {
            jeuTimer.Start();
            goUp = false;
            goDown = false;
            enemie1Sprite.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "enemie1.png"));
            enemie1.Fill = enemie1Sprite;
            enemie2Sprite.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "enemie2.png"));
            enemie2.Fill = enemie2Sprite;
            enemie3Sprite.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "enemie3.png"));
            enemie3.Fill = enemie3Sprite;
            enemie4Sprite.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "enemie4.png"));
            enemie4.Fill = enemie4Sprite;
        }

        

        private void Jeu(object sender, EventArgs e)
        {
            // création d'un rectangle joueur pour la détection de collision
            /*Rect joueur = new Rect(Canvas.GetLeft(joueur1), Canvas.GetTop(joueur1), joueur1.Width, joueur1.Height);*/


            // déplacez l'arrière-plan de 10 pixels vers la gauche à chaque tick (1 tick = 17ms)
            Canvas.SetLeft(ArrierePlan, Canvas.GetLeft(ArrierePlan) - 10);
            Canvas.SetLeft(ArrierePlan2, Canvas.GetLeft(ArrierePlan2) - 10);

           
            speed1 = random.Next(15,25);
            speed2 = random.Next(20,25);
            speed3 = random.Next(27,32);
            speed4 = random.Next(25,35);


            Canvas.SetLeft(enemie1, Canvas.GetLeft(enemie1) - speed1);
            Canvas.SetLeft(enemie2, Canvas.GetLeft(enemie2) - speed2);
            Canvas.SetLeft(enemie3, Canvas.GetLeft(enemie3) - speed3);
            Canvas.SetLeft(enemie4, Canvas.GetLeft(enemie4) - speed4);


            Bouger_Joueur();
        }

        private void Bouger_Joueur()
        {
            if (goUp && Canvas.GetTop(joueur) > 0)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - vitesseJoueur);
            }
            if (goDown && Canvas.GetTop(joueur) > 0)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + vitesseJoueur);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                goUp = false;
            }
            if (e.Key == Key.Down)
            {
                goDown = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                goUp = true;
            }
            if (e.Key == Key.Down)
            {
                goDown = true;
            }
        }
    }




}
   



