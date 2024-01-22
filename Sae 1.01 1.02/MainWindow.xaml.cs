using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
        private bool goUp, goDown, goLeft, goRight = false;

        private int vitesseJoueur = 7;

        private Rect joueurHitBox;
        private Rect ennemie1HitBox;
        private Rect ennemie2HitBox;
        private Rect ennemie3HitBox;
        private Rect ennemie4HitBox;

        private bool gameover = false;

        private ImageBrush joueurSkin = new ImageBrush();
        private ImageBrush imageButQuitterPause = new ImageBrush();

        private ImageBrush ennemie1Skin = new ImageBrush();
        private ImageBrush ennemie2Skin = new ImageBrush();
        private ImageBrush ennemie3Skin = new ImageBrush();
        private ImageBrush ennemie4Skin = new ImageBrush();
        private ImageBrush ArrierePlanSprite = new ImageBrush();
        private DispatcherTimer jeuTimer = new DispatcherTimer();

        private int tempsVoiture = 0;

        private int compteurtemps=0;

        public Key toucheHaut, toucheBas, toucheGauche, toucheDroite;
        

        private int vitesse1 = 0;
        private int vitesse2 = 0;
        private int vitesse3 = 0;
        private int vitesse4 = 0;

        private Random aleatoire = new Random();

        private int[] ennemie1Position = { 115, 120, 125 };
        private int[] ennemie2Position = { 185, 190, 195 };
        private int[] ennemie3Position = { 255, 260, 265 };
        private int[] ennemie4Position = { 325, 330, 335 };

        private SoundPlayer Klaxon = new SoundPlayer(soundLocation: "Son/KlaxonVoiture.wav");

        private int numeroVoitureEnnemie1;
        private int numeroVoitureEnnemie2;
        private int numeroVoitureEnnemie3;
        private int numeroVoitureEnnemie4;

        private readonly int RESET = 0;

        

        private readonly int DELAI_ENNEMIE = 150;

        //Arriere plan
        private readonly int VITESSE_ARRIERE_PLAN = 10;
        private readonly int ARRIERE_PLAN_REAPPARITION = -3370;

        //Disparition et réapparition 
        private readonly int ENNEMIE_DISPARITION = -50;
        private readonly int ENNEMIE_REAPPARITION = 3000;
        
        //Temps
        private readonly int TEMPS_SECONDE = 30;

        //Choix du skin enemie
        private readonly int SKIN_MIN = 1;
        private readonly int SKIN_MAX = 15;

        //Remise des enemies à leur place initiale
        private readonly int RESET_POSITION_GAUCHE_ENNEMIE1 = 2254;
        private readonly int RESET_POSITION_GAUCHE_ENNEMIE2 = 2707;
        private readonly int RESET_POSITION_HAUT_ENNEMIE1 = 120;
        private readonly int RESET_POSITION_HAUT_ENNEMIE2 = 191;
        private readonly int RESET_POSITION_HAUT_ENNEMIE3 = 260;
        private readonly int RESET_POSITION_HAUT_ENNEMIE4 = 330;
        private readonly int RESET_POSITION_GAUCHE_JOUEUR = 200;
        private readonly int RESET_POSITION_HAUT_JOUEUR = 220;

        //Collision joueur avec le bord
        private readonly int COLLISION_BORDURE_HAUT = 93;
        private readonly int COLLISION_BORDURE_BAS = 357;
        private readonly int COLLISION_BORDURE_GAUCHE = 20;
        private readonly int COLLISION_BORDURE_DROITE = 1200;
        private readonly int COLLISION_JOUEUR = 7;

        //Difficulté ennemies
        int tempsMin;
        int tempsMax;
        int vitesseMin;
        int vitesseMax;


        private Difficulte difficulte = new Difficulte();

        public MainWindow()
        {
            InitializeComponent();

            

            Menu fenetreDebut = new Menu();
            Difficulte difficulte = new Difficulte();
            fenetreDebut.ShowDialog();
            if (fenetreDebut.DialogResult == false)
                Application.Current.Shutdown();
            else if (fenetreDebut.DialogResult == true)
            {
                difficulte.ShowDialog();

            }

            toucheHaut = fenetreDebut.toucheHaut;
            toucheBas = fenetreDebut.toucheBas;
            toucheDroite = fenetreDebut.toucheDroite;
            toucheGauche = fenetreDebut.toucheGauche;



            tempsMin = difficulte.tempsMin;
            tempsMax = difficulte.tempsMax;
            vitesseMin = difficulte.vitesseMin;
            vitesseMax = difficulte.vitesseMax;


            imageButQuitterPause.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/croix.png"));
            butQuitterPause.Background = imageButQuitterPause;
            // chargement de l’image du joueur 
            joueurSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/formule1.png"));
            // assignement de skin du joueur au rectangle associé
            joueur.Fill = joueurSkin;
            ArrierePlanSprite.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/Fond.png"));
            ArrierePlan.Fill = ArrierePlanSprite;
            ArrierePlan2.Fill = ArrierePlanSprite;

            jeuTimer.Interval = TimeSpan.FromMilliseconds(17);
            jeuTimer.Tick += Jeu;

            jeuTimer.Start();
            DebutJeu();
        }


        private void DebutJeu()
        {
            jeuTimer.Start();
            goUp = false;
            goDown = false;

            ennemie1Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/enemie1.png"));
            ennemie1.Fill = ennemie1Skin;
            ennemie2Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/enemie2.png"));
            ennemie2.Fill = ennemie2Skin;
            ennemie3Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/enemie3.png"));
            ennemie3.Fill = ennemie3Skin;
            ennemie4Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/enemie4.png"));
            ennemie4.Fill = ennemie4Skin;

            gameover = false;
        }


/*----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
/*------------------------------------------------------------------↓↓↓↓↓↓-TOUCHE HAUT BAS-↓↓↓↓↓↓-----------------------------------------------------------------------------------*/

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == toucheHaut)
            {
                goUp = false;
            }
            if (e.Key == Key.Down || e.Key == toucheBas)
            {
                goDown = false;
            }
            if (e.Key == Key.Left || e.Key == toucheGauche)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right || e.Key == toucheDroite)
            {
                goRight = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == toucheHaut)
            {
                goUp = true;
            }
            if (e.Key == Key.Down || e.Key == toucheBas)
            {
                goDown = true;
            }
            if (e.Key == Key.Left || e.Key == toucheGauche)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right || e.Key == toucheDroite)
            {
                goRight = true;
            }
            if (e.Key == Key.Space)
            {
                Klaxon.Play();
            }


            if (e.Key == Key.P)
            {
                fenetrePause.Visibility = Visibility.Visible;
                butQuitterPause.Visibility = Visibility.Visible;
                jeuTimer.Stop();
            }
        }
/*-----------------------------------------------------------------↑↑↑↑↑↑--TOUCHE HAUT BAS-↑↑↑↑↑↑-----------------------------------------------------------------------------------*/
/*----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


/*----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
/*-----------------------------------------------------------------------↓↓↓↓↓↓-JEUX-↓↓↓↓↓↓-----------------------------------------------------------------------------------------*/


        private void Jeu(object sender, EventArgs e)
        {
            
            Canvas.SetLeft(ennemie1, Canvas.GetLeft(ennemie1) - vitesse1);
            Canvas.SetLeft(ennemie2, Canvas.GetLeft(ennemie2) - vitesse2);
            Canvas.SetLeft(ennemie3, Canvas.GetLeft(ennemie3) - vitesse3);
            Canvas.SetLeft(ennemie4, Canvas.GetLeft(ennemie4) - vitesse4);

            Bouger_Joueur();
            ArrierePlanEnMouvement();
            Collision();


            compteurtemps++;
            if (compteurtemps % 30 == 0)
            {
                tempsVoiture++;
                tempsPasse.Content = "Temps passe : " + tempsVoiture + " secondes";
            }

            VoituresEnnemies();

        }


/*----------------------------------------------------------------------↑↑↑↑↑↑--JEUX-↑↑↑↑↑↑-----------------------------------------------------------------------------------------*/
/*----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


/*----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
/*---------------------------------------------------------------------↓↓↓↓↓↓-METHODES-↓↓↓↓↓↓---------------------------------------------------------------------------------------*/


        private void VoituresEnnemies()
        {
            if (compteurtemps % DELAI_ENNEMIE == 0)
            {
                vitesse1 = aleatoire.Next(vitesseMin, vitesseMax);
                vitesse2 = aleatoire.Next(vitesseMin, vitesseMax);
                vitesse3 = aleatoire.Next(vitesseMin, vitesseMax);
                vitesse4 = aleatoire.Next(vitesseMin, vitesseMax);
            }

            joueurHitBox = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);
            ennemie1HitBox = new Rect(Canvas.GetLeft(ennemie1), Canvas.GetTop(ennemie1), ennemie1.Width, ennemie1.Height);
            ennemie2HitBox = new Rect(Canvas.GetLeft(ennemie2), Canvas.GetTop(ennemie2), ennemie2.Width, ennemie2.Height);
            ennemie3HitBox = new Rect(Canvas.GetLeft(ennemie3), Canvas.GetTop(ennemie3), ennemie3.Width, ennemie3.Height);
            ennemie4HitBox = new Rect(Canvas.GetLeft(ennemie4), Canvas.GetTop(ennemie4), ennemie4.Width, ennemie4.Height);


            if (joueurHitBox.IntersectsWith(ennemie1HitBox) || joueurHitBox.IntersectsWith(ennemie2HitBox) ||
                joueurHitBox.IntersectsWith(ennemie3HitBox) || joueurHitBox.IntersectsWith(ennemie4HitBox))
            {
                Console.WriteLine("Collision");
                gameover = true;
                jeuTimer.Stop();
                fenetrePerdu.Visibility = Visibility.Visible;
            }


            if (gameover == true)
            {

                ennemie1.Stroke = Brushes.Black;
                ennemie1.StrokeThickness = 1;
                ennemie2.Stroke = Brushes.Black;
                ennemie2.StrokeThickness = 1;
                ennemie3.Stroke = Brushes.Black;
                ennemie3.StrokeThickness = 1;
                ennemie4.Stroke = Brushes.Black;
                ennemie4.StrokeThickness = 1;

                joueur.Stroke = Brushes.Red;
                joueur.StrokeThickness = 1;

            }
            else
            {

                joueur.StrokeThickness = 0;
                ennemie1.StrokeThickness = 0;
                ennemie2.StrokeThickness = 0;
                ennemie3.StrokeThickness = 0;
                ennemie4.StrokeThickness = 0;
            }

            

            if (Canvas.GetLeft(ennemie1) < ENNEMIE_DISPARITION)
            {
                if (compteurtemps % (TEMPS_SECONDE * aleatoire.Next(tempsMin, tempsMax)) == RESET)
                {
                    Console.WriteLine("Temps à attendre voiture 1: " + aleatoire);
                    numeroVoitureEnnemie1 = aleatoire.Next(SKIN_MIN, SKIN_MAX);
                    Console.WriteLine("N° skin voiture 1: " + numeroVoitureEnnemie1);
                    ennemie1Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/Voitures_Enemies/enemie" + numeroVoitureEnnemie1 + ".png"));
                    ennemie1.Fill = ennemie1Skin;

                    // Faire réapparaitre l'ennemie à 3000 pixels
                    Canvas.SetLeft(ennemie1, ENNEMIE_REAPPARITION);
                    // définit aléatoirement la position supérieure de l'obstacle à partir du tableau que nous avons créé précédemment
                    // cela choisi aléatoirement une position dans le tableau afin qu'elle ne soit pas la même à chaque fois qu'elle apparaîtra sur l'écran
                    Canvas.SetTop(ennemie1, ennemie1Position[aleatoire.Next(0, ennemie1Position.Length)]);
                    compteurtemps = RESET;
                }
                
            }

            if (Canvas.GetLeft(ennemie2) < ENNEMIE_DISPARITION)
            {
                if (compteurtemps % (TEMPS_SECONDE * aleatoire.Next(tempsMin, tempsMax)) == RESET)
                {
                    Console.WriteLine("Temps à attendre voiture 2: " + aleatoire);
                    numeroVoitureEnnemie2 = aleatoire.Next(SKIN_MIN, SKIN_MAX);
                    Console.WriteLine("N° skin voiture 2: " + numeroVoitureEnnemie2);
                    ennemie2Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/Voitures_Enemies/enemie" + numeroVoitureEnnemie2 + ".png"));
                    ennemie2.Fill = ennemie2Skin;

                    // Faire réapparaitre l'énemie à 3000 pixels
                    Canvas.SetLeft(ennemie2, ENNEMIE_REAPPARITION);
                    // cela choisi aléatoirement une position dans le tableau afin qu'elle ne soit pas la même à chaque fois qu'elle apparaîtra sur l'écran
                    Canvas.SetTop(ennemie2, ennemie2Position[aleatoire.Next(0, ennemie2Position.Length)]);
                    compteurtemps = RESET;
                }
            }

            if (Canvas.GetLeft(ennemie3) < ENNEMIE_DISPARITION)
            {
                if (compteurtemps % (TEMPS_SECONDE * aleatoire.Next(tempsMin, tempsMax)) == RESET)
                {
                    Console.WriteLine("Temps à attendre voiture 3: " + aleatoire);
                    numeroVoitureEnnemie3 = aleatoire.Next(SKIN_MIN, SKIN_MAX);
                    Console.WriteLine("N° skin voiture 3: " + numeroVoitureEnnemie3);
                    ennemie3Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/Voitures_Enemies/enemie" + numeroVoitureEnnemie3 + ".png"));
                    ennemie3.Fill = ennemie3Skin;

                    // Faire réapparaitre l'énemie à 3000 pixels
                    Canvas.SetLeft(ennemie3, ENNEMIE_REAPPARITION);
                    // cela choisi aléatoirement une position dans le tableau afin qu'elle ne soit pas la même à chaque fois qu'elle apparaîtra sur l'écran
                    Canvas.SetTop(ennemie3, ennemie3Position[aleatoire.Next(0, ennemie3Position.Length)]);
                    compteurtemps = RESET;
                }
            }

            if (Canvas.GetLeft(ennemie4) < ENNEMIE_DISPARITION)
            {
                if (compteurtemps % (TEMPS_SECONDE * aleatoire.Next(tempsMin, tempsMax)) == RESET)
                {
                    Console.WriteLine("Temps à attendre voiture 4: " + aleatoire);
                    numeroVoitureEnnemie4 = aleatoire.Next(SKIN_MIN, SKIN_MAX);
                    Console.WriteLine("N° skin voiture 4: " + numeroVoitureEnnemie4);
                    ennemie4Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/Voitures_Enemies/enemie" + numeroVoitureEnnemie4 + ".png"));
                    ennemie4.Fill = ennemie4Skin;

                    // Faire réapparaitre l'énemie à 3000 pixels
                    Canvas.SetLeft(ennemie4, ENNEMIE_REAPPARITION);
                    // cela choisi aléatoirement une position dans le tableau afin qu'elle ne soit pas la même à chaque fois qu'elle apparaîtra sur l'écran
                    Canvas.SetTop(ennemie4, ennemie4Position[aleatoire.Next(0, ennemie4Position.Length)]);
                    compteurtemps = RESET;
                }
            }
        }



        private void Collision()
        {
            if (Canvas.GetTop(joueur) < COLLISION_BORDURE_HAUT)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + COLLISION_JOUEUR);
            }
            if (Canvas.GetTop(joueur) > COLLISION_BORDURE_BAS)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - COLLISION_JOUEUR);
            }
            if (Canvas.GetLeft(joueur) > COLLISION_BORDURE_GAUCHE)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - COLLISION_JOUEUR);
            }
            if (Canvas.GetLeft(joueur) < COLLISION_BORDURE_DROITE)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + COLLISION_JOUEUR);
            }
        }


        private void ArrierePlanEnMouvement()
        {
            // déplacez l'arrière-plan de 10 pixels vers la gauche à chaque tick (1 tick = 17ms)
            Canvas.SetLeft(ArrierePlan, Canvas.GetLeft(ArrierePlan) - VITESSE_ARRIERE_PLAN);
            Canvas.SetLeft(ArrierePlan2, Canvas.GetLeft(ArrierePlan2) - VITESSE_ARRIERE_PLAN);

            // code de défilement de parallaxe pour c#
            // le code ci-dessous fera défiler l'arrière-plan simultanément et le fera paraître sans fin
            // vérifie le premier arrière-plan
            // si la première position X de l'arrière-plan descend en dessous de -1435 pixels
            if (Canvas.GetLeft(ArrierePlan) < ARRIERE_PLAN_REAPPARITION)
            {
                // positionne le premier arrière-plan derrière le deuxième arrière-plan
                // ci-dessous, nous définissons les arrière-plans à gauche, à la position de largeur background2
                Canvas.SetLeft(ArrierePlan, Canvas.GetLeft(ArrierePlan2) + ArrierePlan2.Width);
                Console.WriteLine("Arriere plan 1 déplacé en : " + (Canvas.GetLeft(ArrierePlan2) + ArrierePlan2.Width));
            }
            // on fait pareil pour le fond 2
            // si la position X de l'arrière-plan 2 descend en dessous de -1435
            if (Canvas.GetLeft(ArrierePlan2) < ARRIERE_PLAN_REAPPARITION)
            {
                // positionne le deuxième arrière-plan derrière le premier arrière-plan
                // ci-dessous, nous définissons la position gauche de l'arrière-plan 2 ou la position X sur la position de la largeur de l'arrière-plan
                Canvas.SetLeft(ArrierePlan2, Canvas.GetLeft(ArrierePlan) + ArrierePlan.Width);
                Console.WriteLine("Arriere plan 2 déplacé en : " + (Canvas.GetLeft(ArrierePlan) + ArrierePlan.Width));
            }
        }


        private void Bouger_Joueur()
        {
            if (goUp && Canvas.GetTop(joueur) > RESET)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - vitesseJoueur);
                Console.WriteLine("Bouton pour monter pressé on monte le joueur");
            }
            if (goDown && Canvas.GetTop(joueur) > RESET)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + vitesseJoueur);
                Console.WriteLine("Bouton pour descendre pressé on descend le joueur");
            }
            if (goLeft && Canvas.GetLeft(joueur) > RESET)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - vitesseJoueur);
                Console.WriteLine("Bouton pour se décaler à gauche pressé on décale à gauche le joueur");
            }
            if (goRight && Canvas.GetLeft(joueur) > RESET)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + vitesseJoueur);
                Console.WriteLine("Bouton pour se décaler à droite pressé on décale à droite le joueur");
            }
        }


        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------↓↓↓↓↓↓-FENETRE PERDU-↓↓↓↓↓↓------------------------------------------------------------------*/

        private void butRejouerPerdu_Click(object sender, RoutedEventArgs e)
        {
            tempsVoiture = RESET;

            foreach (Rectangle x in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "enemie")
                {
                    Canvas.SetLeft(ennemie1, RESET_POSITION_GAUCHE_ENNEMIE1);
                    Canvas.SetTop(ennemie1, RESET_POSITION_HAUT_ENNEMIE1);

                    Canvas.SetLeft(ennemie2, RESET_POSITION_GAUCHE_ENNEMIE2);
                    Canvas.SetTop(ennemie2, RESET_POSITION_HAUT_ENNEMIE2);

                    Canvas.SetLeft(ennemie3, RESET_POSITION_GAUCHE_ENNEMIE2);
                    Canvas.SetTop(ennemie3, RESET_POSITION_HAUT_ENNEMIE3);

                    Canvas.SetLeft(ennemie4, RESET_POSITION_GAUCHE_ENNEMIE1);
                    Canvas.SetTop(ennemie4, RESET_POSITION_HAUT_ENNEMIE4);
                }

                if ((string)x.Tag == "joueur")
                {
                    Canvas.SetLeft(joueur, RESET_POSITION_GAUCHE_JOUEUR);
                    Canvas.SetTop(joueur, RESET_POSITION_HAUT_JOUEUR);
                }
            }
            jeuTimer.Start();
            fenetrePerdu.Visibility = Visibility.Hidden;
            gameover = false;
        }

        private void butMenuPerdu_Click(object sender, RoutedEventArgs e)
        {
            Menu fenetreDebut = new Menu();
            fenetreDebut.ShowDialog();
        }

        private void butQuitterPerdu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*------------------------------------------------------------------↑↑↑↑↑↑-FENETRE PERDU-↑↑↑↑↑↑------------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------↓↓↓↓↓↓-FENETRE PAUSE-↓↓↓↓↓↓------------------------------------------------------------------*/
        private void butRejouerPause_Click(object sender, RoutedEventArgs e)
        {
            tempsVoiture = RESET;

            foreach (Rectangle x in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "enemie")
                {
                    Canvas.SetLeft(ennemie1, RESET_POSITION_GAUCHE_ENNEMIE1);
                    Canvas.SetTop(ennemie1, RESET_POSITION_HAUT_ENNEMIE1);

                    Canvas.SetLeft(ennemie2, RESET_POSITION_GAUCHE_ENNEMIE2);
                    Canvas.SetTop(ennemie2, RESET_POSITION_HAUT_ENNEMIE2);

                    Canvas.SetLeft(ennemie3, RESET_POSITION_GAUCHE_ENNEMIE2);
                    Canvas.SetTop(ennemie3, RESET_POSITION_HAUT_ENNEMIE3);

                    Canvas.SetLeft(ennemie4, RESET_POSITION_GAUCHE_ENNEMIE1);
                    Canvas.SetTop(ennemie4, RESET_POSITION_HAUT_ENNEMIE4);
                }

                /*else*/
                if ((string)x.Tag == "joueur")
                {
                    Canvas.SetLeft(joueur, RESET_POSITION_GAUCHE_JOUEUR);
                    Canvas.SetTop(joueur, RESET_POSITION_HAUT_JOUEUR);
                }
            }
            jeuTimer.Start();
            fenetrePause.Visibility = Visibility.Hidden;
            butQuitterPause.Visibility = Visibility.Hidden;
        }

        private void butReprendrePause_Click(object sender, RoutedEventArgs e)
        {
            jeuTimer.Start();
            fenetrePause.Visibility = Visibility.Hidden;
            butQuitterPause.Visibility = Visibility.Hidden;
        }

        private void butMenuPause_Click(object sender, RoutedEventArgs e)
        {
            Menu fenetreDebut = new Menu();
            fenetreDebut.ShowDialog();
        }

        private void butQuitterPause_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        /*------------------------------------------------------------------↑↑↑↑↑↑-FENETRE PAUSE-↑↑↑↑↑↑------------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/

/*---------------------------------------------------------------------↑↑↑↑↑↑-METHODES-↑↑↑↑↑↑---------------------------------------------------------------------------------------*/
/*----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

    }
}










