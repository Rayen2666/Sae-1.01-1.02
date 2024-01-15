using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
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
using static System.Formats.Asn1.AsnWriter;

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

        Rect joueurHitBox;
        Rect bordure1HitBox;
        Rect bordure2HitBox;
        Rect enemie1HitBox;
        Rect enemie2HitBox;
        Rect enemie3HitBox;
        Rect enemie4HitBox;

        bool gameover = false;

        private ImageBrush playerSkin = new ImageBrush();

        ImageBrush enemie1Skin = new ImageBrush();
        ImageBrush enemie2Skin = new ImageBrush();
        ImageBrush enemie3Skin = new ImageBrush();
        ImageBrush enemie4Skin = new ImageBrush();
        ImageBrush ArrierePlanSprite = new ImageBrush();
        DispatcherTimer jeuTimer = new DispatcherTimer();




        Random vitesse = new Random();

        public int vitesse1 = 0;
        public int vitesse2 = 0;
        public int vitesse3 = 0;
        public int vitesse4 = 0;

        Random rand = new Random();

        int[] enemie1Position = { 115, 120, 125 };

        int totalEnemie = 0;

        private List<Rectangle> itemsToRemove = new List<Rectangle>();

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





            // chargement de l’image du joueur 
            playerSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "formule1.png"));
            // assignement de skin du joueur au rectangle associé
            joueur.Fill = playerSkin;
            ArrierePlanSprite.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/Fond.png"));
            ArrierePlan.Fill = ArrierePlanSprite;
            ArrierePlan2.Fill = ArrierePlanSprite;

            jeuTimer.Interval = TimeSpan.FromMilliseconds(17);
            jeuTimer.Tick += Jeu;
                  

            vitesse1 = vitesse.Next(10, 30);
            vitesse2 = vitesse.Next(10, 30);
            vitesse3 = vitesse.Next(10, 30);
            vitesse4 = vitesse.Next(10, 30);

            jeuTimer.Start();
            DebutJeu();
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
            gameover = false;
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
            /*MakeEnemies(10);*/

            /*voiturePasse.Content = "Voiture passee : " + totalEnemie;*/

            // si l'obstacle dépasse -50 emplacement
            if (Canvas.GetLeft(enemie1) < -50)
            {
                /*itemsToRemove.Add(enemie1);*/
                // régler la position gauche de l'obstacle à 950 pixels
                Canvas.SetLeft(enemie1, 1000);
                // définit aléatoirement la position supérieure de l'obstacle à partir du tableau que nous avons créé précédemment
                // cela choisira aléatoirement une position dans le tableau afin qu'elle ne soit pas la même à chaque fois qu'elle apparaîtra sur l'écran
                Canvas.SetTop(enemie1, enemie1Position[rand.Next(0, enemie1Position.Length)]);
                // ajouter 1 au score
                /*score += 1;*/
            }

                joueurHitBox = new  Rect (Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);
                enemie1HitBox = new Rect(Canvas.GetLeft(enemie1), Canvas.GetTop(enemie1), enemie1.Width, enemie1.Height);
                enemie2HitBox = new Rect(Canvas.GetLeft(enemie2), Canvas.GetTop(enemie2), enemie2.Width, enemie2.Height);
                enemie3HitBox = new Rect(Canvas.GetLeft(enemie3), Canvas.GetTop(enemie3), enemie3.Width, enemie3.Height);
                enemie4HitBox = new Rect(Canvas.GetLeft(enemie4), Canvas.GetTop(enemie4), enemie4.Width, enemie4.Height);

        


            if (joueurHitBox.IntersectsWith(enemie1HitBox) || joueurHitBox.IntersectsWith(enemie2HitBox) ||
                joueurHitBox.IntersectsWith(enemie3HitBox) || joueurHitBox.IntersectsWith(enemie4HitBox))
                {
                    gameover = true;
                    jeuTimer.Stop();
                }
        
             

            if (gameover == true)
            {

                enemie1.Stroke = Brushes.Black;
                enemie1.StrokeThickness = 1;
                enemie2.Stroke = Brushes.Black;
                enemie2.StrokeThickness = 1;
                enemie3.Stroke = Brushes.Black;
                enemie3.StrokeThickness = 1;
                enemie4.Stroke = Brushes.Black;
                enemie4.StrokeThickness = 1;

                joueur.Stroke = Brushes.Red;
                joueur.StrokeThickness = 1;
              
            }
            else
            {
    
                joueur.StrokeThickness = 0;
                enemie1.StrokeThickness = 0;
                enemie2.StrokeThickness = 0;
                enemie3.StrokeThickness = 0;
                enemie4.StrokeThickness = 0;
            }





            int dkdk = rand.Next(120, 320);

        }
            
             










            /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            /*---------------------------------------------------------------------↓↓↓↓↓↓-METHODES-↓↓↓↓↓↓--------------------------------------------------------------------*/

            /*private void MakeEnemies(int limit)
            {
                int left = 0;
                // on conserve le max d’ennemis
                totalEnemie = limit;
                for (int i = 0; i < limit; i++)
                {
                    ImageBrush enemieSkin = new ImageBrush();
                    Rectangle newEnemie = new Rectangle
                    {
                        Tag = "enemy",
                        Height = 45,
                        Width = 45,
                        Fill = enemieSkin,
                    };
                    Canvas.SetTop(newEnemie, 30);
                    Canvas.SetLeft(newEnemie, left);
                    myCanvas.Children.Add(newEnemie);
                    left -= 60;

                    *//*// incrémente les images des ennemis (max 8)
                    enemyImages++;
                    if (enemyImages > 8)
                        enemyImages = 1;
                    enemySkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/invader" + enemie1Skin + ".png"));*//*
                }
            }*/

            /*private void NouveauEnemie(int limit)
            {

            }
            */
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
            }

            /*---------------------------------------------------------------------↑↑↑↑↑↑-METHODES-↑↑↑↑↑↑--------------------------------------------------------------------*/
            /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        }




    } 
   



