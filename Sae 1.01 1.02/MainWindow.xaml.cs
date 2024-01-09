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

        private ImageBrush playerSkin = new ImageBrush();

        public MainWindow()
        {
            InitializeComponent();
            // chargement de l’image du joueur 
            playerSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "formule1.png"));
            // assignement de skin du joueur au rectangle associé
            player1.Fill = playerSkin;
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
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

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
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

        private void GameEngine(object sender, EventArgs e)
        {
            /*// création d'un rectangle joueur pour la détection de collision
            Rect joueur = new Rect(Canvas.GetLeft(joueur1), Canvas.GetTop(joueur1), joueur1.Width, joueur1.Height);*/

            Bouger_Joueur();
        }

        private void Bouger_Joueur()
        {
            if (goUp && Canvas.GetTop(player1) > 0)
            {
                Canvas.SetTop(player1, Canvas.GetTop(player1) - vitesseJoueur);
            }
            if (goDown && Canvas.GetTop(player1) > 0)
            {
                Canvas.SetTop(player1, Canvas.GetTop(player1) + vitesseJoueur);
            }
        }
    }
}
