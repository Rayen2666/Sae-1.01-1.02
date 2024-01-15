using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using static System.Formats.Asn1.AsnWriter;

namespace Sae_1._01_1._02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // booléens pour monter et descendre
        bool goUp, goDown, goLeft, goRight = false;

        int vitesseJoueur = 5;

        Rect joueurHitBox;
        Rect bordure1HitBox;
        Rect bordure2HitBox;

        private ImageBrush playerSkin = new ImageBrush();

        ImageBrush enemie1Skin = new ImageBrush();
        ImageBrush enemie2Skin = new ImageBrush();
        ImageBrush enemie3Skin = new ImageBrush();
        ImageBrush enemie4Skin = new ImageBrush();
        ImageBrush ArrierePlanSprite = new ImageBrush();
        DispatcherTimer jeuTimer = new DispatcherTimer();

        private int tempsVoiture = 0;

        int compteurtemps;
        Random vitesse = new Random();

        public int vitesse1 = 0;
        public int vitesse2 = 0;
        public int vitesse3 = 0;
        public int vitesse4 = 0;

        Random rand = new Random();

        int[] enemie1Position = { 115, 120, 125 };
        int[] enemie2Position = { 185, 190, 195 };
        int[] enemie3Position = { 255, 260, 265 };
        int[] enemie4Position = { 325, 330, 335 };
        int[] tempsEntreEnemie = {60, 120, 180, 240, 320};

        int totalEnemie = 0;

        List<Rectangle> itemRemover = new List<Rectangle>();
        int numeroVoiture;


        public MainWindow()
        {
            InitializeComponent();

            Menu fenetreDebut = new Menu();
            Difficulté difficulte = new Difficulté();
            fenetreDebut.ShowDialog();
            if (fenetreDebut.DialogResult == false)
                Application.Current.Shutdown();
            else if (fenetreDebut.DialogResult == true)
            {
                difficulte.ShowDialog();
                
            }

            
          
            
            
            DebutJeu();
            // chargement de l’image du joueur 
            playerSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "formule1.png"));
            // assignement de skin du joueur au rectangle associé
            joueur.Fill = playerSkin;
            ArrierePlanSprite.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/Fond.png"));
            ArrierePlan.Fill = ArrierePlanSprite;
            ArrierePlan2.Fill = ArrierePlanSprite;

                jeuTimer.Interval = TimeSpan.FromMilliseconds(17);
                jeuTimer.Tick += Jeu;
            
            jeuTimer.Interval = TimeSpan.FromMilliseconds(17);
            jeuTimer.Tick += Jeu; 
            DebutJeu();

            /*vitesse1 = vitesse.Next(10, 30);
            vitesse2 = vitesse.Next(10, 30);
            vitesse3 = vitesse.Next(10, 30);
            vitesse4 = vitesse.Next(10, 30);*/

            vitesse1 = 10;
            vitesse2 = 10;
            vitesse3 = 10;
            vitesse4 = 10;
        }




        private void DebutJeu()
        {
            jeuTimer.Start();
            goUp = false;
            goDown = false;
            enemie1Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "enemie1.png"));
            enemie1.Fill = enemie1Skin;
            enemie2Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "enemie2.png"));
            enemie2.Fill = enemie2Skin;
            enemie3Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "enemie3.png"));
            enemie3.Fill = enemie3Skin;
            enemie4Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "enemie4.png"));
            enemie4.Fill = enemie4Skin;
        }


        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------↓↓↓↓↓↓-TOUCHE HAUT BAS-↓↓↓↓↓↓----------------------------------------------------------------*/

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
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
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
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
        }
        /*-----------------------------------------------------------------↑↑↑↑↑↑--TOUCHE HAUT BAS-↑↑↑↑↑↑----------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/




        private void Jeu(object sender, EventArgs e)
        {

            // déplacez l'arrière-plan de 10 pixels vers la gauche à chaque tick (1 tick = 17ms)
            Canvas.SetLeft(ArrierePlan, Canvas.GetLeft(ArrierePlan) - 10);
            Canvas.SetLeft(ArrierePlan2, Canvas.GetLeft(ArrierePlan2) - 10);


            Canvas.SetLeft(enemie1, Canvas.GetLeft(enemie1) - vitesse1);
            Canvas.SetLeft(enemie2, Canvas.GetLeft(enemie2) - vitesse2);
            Canvas.SetLeft(enemie3, Canvas.GetLeft(enemie3) - vitesse3);
            Canvas.SetLeft(enemie4, Canvas.GetLeft(enemie4) - vitesse4);

            Bouger_Joueur();
            ArrierePlanEnMouvement();
            Collision();


            compteurtemps++;
            if (compteurtemps % 60 == 0)
            {
                tempsVoiture++;
                tempsPasse.Content = "Temps passe : " + tempsVoiture + " secondes";
            }

            VoituresEnemies();

        }



        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------------------------------------↓↓↓↓↓↓-METHODES-↓↓↓↓↓↓--------------------------------------------------------------------*/
        

        private void VoituresEnemies()
        {
            if (Canvas.GetLeft(enemie1) < -50)
            {
                if (compteurtemps % (60 * rand.Next(1, 6)) == 0)
                {
                    /*itemsToRemove.Add(enemie1); (rand.NextDouble()*5)*/
                    // régler la position gauche de l'obstacle à 950 pixels
                    Canvas.SetLeft(enemie1, 1000);
                    // définit aléatoirement la position supérieure de l'obstacle à partir du tableau que nous avons créé précédemment
                    // cela choisira aléatoirement une position dans le tableau afin qu'elle ne soit pas la même à chaque fois qu'elle apparaîtra sur l'écran
                    Canvas.SetTop(enemie1, enemie1Position[rand.Next(0, enemie1Position.Length)]);
                    // ajouter 1 au score
                    /*score += 1;*/
                    compteurtemps = 0;
                }
            }

            if (Canvas.GetLeft(enemie2) < -50)
            {
                if (compteurtemps % (60 * rand.Next(1, 6)) == 0)
                {
                    /*itemsToRemove.Add(enemie1);*/
                    // régler la position gauche de l'obstacle à 950 pixels
                    Canvas.SetLeft(enemie2, 1000);
                    // définit aléatoirement la position supérieure de l'obstacle à partir du tableau que nous avons créé précédemment
                    // cela choisira aléatoirement une position dans le tableau afin qu'elle ne soit pas la même à chaque fois qu'elle apparaîtra sur l'écran
                    Canvas.SetTop(enemie2, enemie2Position[rand.Next(0, enemie2Position.Length)]);
                    // ajouter 1 au score
                    /*score += 1;*/
                    compteurtemps = 0;
                }
            }

            if (Canvas.GetLeft(enemie3) < -50)
            {
                if (compteurtemps % (60 * rand.Next(1, 6)) == 0)
                {
                    /*itemsToRemove.Add(enemie1);*/
                    // régler la position gauche de l'obstacle à 950 pixels
                    Canvas.SetLeft(enemie3, 1000);
                    // définit aléatoirement la position supérieure de l'obstacle à partir du tableau que nous avons créé précédemment
                    // cela choisira aléatoirement une position dans le tableau afin qu'elle ne soit pas la même à chaque fois qu'elle apparaîtra sur l'écran
                    Canvas.SetTop(enemie3, enemie3Position[rand.Next(0, enemie3Position.Length)]);
                    // ajouter 1 au score
                    /*score += 1;*/
                    compteurtemps = 0;
                }
            }

            if (Canvas.GetLeft(enemie4) < -50)
            {
                if (compteurtemps % (60 * rand.Next(1, 6)) == 0)
                {
                    /*itemsToRemove.Add(enemie1);*/
                    // régler la position gauche de l'obstacle à 950 pixels
                    Canvas.SetLeft(enemie4, 1000);
                    // définit aléatoirement la position supérieure de l'obstacle à partir du tableau que nous avons créé précédemment
                    // cela choisira aléatoirement une position dans le tableau afin qu'elle ne soit pas la même à chaque fois qu'elle apparaîtra sur l'écran
                    Canvas.SetTop(enemie4, enemie4Position[rand.Next(0, enemie4Position.Length)]);
                    // ajouter 1 au score
                    /*score += 1;*/
                    compteurtemps = 0;
                }
            }
        }


        private void Collision()
        {
            /*joueurHitBox = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);*/
             
            if (Canvas.GetTop(joueur) < 93)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + 10);
            }
            if (Canvas.GetTop(joueur) > 357)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - 10);
            }

        }
        


        private void ArrierePlanEnMouvement()
        {
            // déplacez l'arrière-plan de 10 pixels vers la gauche à chaque tick (1 tick = 17ms)
            Canvas.SetLeft(ArrierePlan, Canvas.GetLeft(ArrierePlan) - 5);
            Canvas.SetLeft(ArrierePlan2, Canvas.GetLeft(ArrierePlan2) - 5);

            // code de défilement de parallaxe pour c#
            // le code ci-dessous fera défiler l'arrière-plan simultanément et le fera paraître sans fin
            // vérifie le premier arrière-plan
            // si la première position X de l'arrière-plan descend en dessous de -1435 pixels
            if (Canvas.GetLeft(ArrierePlan) < -1435)
            {
                // positionne le premier arrière-plan derrière le deuxième arrière-plan
                // ci-dessous, nous définissons les arrière-plans à gauche, à la position de largeur background2
                Canvas.SetLeft(ArrierePlan, Canvas.GetLeft(ArrierePlan2) + ArrierePlan2.Width);
            }
            // on fait pareil pour le fond 2
            // si la position X de l'arrière-plan 2 descend en dessous de -1435
            if (Canvas.GetLeft(ArrierePlan2) < -1435)
            {
                // positionne le deuxième arrière-plan derrière le premier arrière-plan
                // ci-dessous, nous définissons la position gauche de l'arrière-plan 2 ou la position X sur la position de la largeur de l'arrière-plan
                Canvas.SetLeft(ArrierePlan2, Canvas.GetLeft(ArrierePlan) + ArrierePlan.Width);
            }
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
            if (goLeft && Canvas.GetLeft(joueur) > 0)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - vitesseJoueur);
            }
            if (goRight && Canvas.GetRight(joueur) > 0)
            {
                Canvas.SetRight(joueur, Canvas.GetRight(joueur) + vitesseJoueur);
            }
        }

        /*---------------------------------------------------------------------↑↑↑↑↑↑-METHODES-↑↑↑↑↑↑--------------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    }




}
   



