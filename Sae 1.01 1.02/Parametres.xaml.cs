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
using System.Windows.Shapes;

namespace Sae_1._01_1._02
{
    /// <summary>
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : Window
    {
        public Key toucheHaut, toucheBas, toucheGauche, toucheDroite;
        public bool changementTouche = false;
        public Key toucheHautDefault = Key.Up;
        public Key toucheBasDefault = Key.Down;
        public Key toucheGaucheDefault = Key.Left;
        public Key toucheDroiteDefault = Key.Right;


        ImageBrush fleche = new ImageBrush();
        ImageBrush image = new ImageBrush();
        ImageBrush direction1 = new ImageBrush();
        ImageBrush direction2 = new ImageBrush();
        ImageBrush direction3 = new ImageBrush();
        ImageBrush direction4 = new ImageBrush();

        public Parametres()
        {
            InitializeComponent();
            fleche.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/fleche.png"));
            retourimage.Background = fleche;
            image.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/ImageMenu.jpg"));
            image2.Fill = image;
            direction1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/flecheHaut.png"));
            flecheHaut.Fill = direction1;
            direction2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/flecheBas2.png"));
            flecheBas.Fill = direction2;
            direction3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/flecheDroite.png"));
            flecheDroite.Fill = direction3;
            direction4.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/flecheGauche.png"));
            flecheGauche.Fill = direction4;

        }

      


        private void Bas_Click(object sender, RoutedEventArgs e)
        {
            changementTouche = true;
            Bas.Content = ".....";
        }

        private void Bas_KeyDown(object sender, KeyEventArgs e)
        {
            if (changementTouche)




            {
                if (e.Key == Key.Escape)
                {
                    Bas.Content = toucheBasDefault;



                }
                else
                {
                    Bas.Content = e.Key.ToString();
                    toucheBas = e.Key;
                }
            }
        }

        private void Gauche_Click(object sender, RoutedEventArgs e)
        {
            changementTouche = true;
            Gauche.Content = ".....";
        }

        private void Gauche_KeyDown(object sender, KeyEventArgs e)
        {
            if (changementTouche)




            {
                if (e.Key == Key.Escape)
                {
                    Gauche.Content = toucheGaucheDefault;



                }
                else
                {
                    Gauche.Content = e.Key.ToString();
                    toucheGauche = e.Key;
                }
            }
        }

        private void Droite_Click(object sender, RoutedEventArgs e)
        {
            changementTouche = true;
            Droite.Content = ".....";

        }

        private void Droite_KeyDown(object sender, KeyEventArgs e)
        {
            if (changementTouche)
            {
                if (e.Key == Key.Escape)
                {



                    Droite.Content = toucheDroiteDefault;
                }

                else
                {
                    Droite.Content = e.Key.ToString();
                    toucheDroite = e.Key;

                }
            }

        }

        private void ButtonHaut_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


        private void Haut_KeyDown(object sender, KeyEventArgs e)
        {
            if (changementTouche)
            {
                if (e.Key == Key.Escape)
                {



                    Haut.Content = toucheHautDefault;
                }

                else
                {
                    Haut.Content = e.Key.ToString();
                    toucheHaut = e.Key;

                }
            }
        }
        private void Haut_Click(object sender, RoutedEventArgs e)
        {
            changementTouche = true;
            Haut.Content = ".....";

        }
    }
}
