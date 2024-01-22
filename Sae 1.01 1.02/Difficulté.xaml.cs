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
using static System.Net.Mime.MediaTypeNames;

namespace Sae_1._01_1._02
{
    /// <summary>
    /// Logique d'interaction pour Difficulté.xaml
    /// </summary>
    public partial class Difficulte : Window
    {
        public int vitesseMin = 0;
        public int vitesseMax = 0;
        public int tempsMin = 0;
        public int tempsMax = 0;

        ImageBrush fond_difficulte = new ImageBrush();
        ImageBrush fleche = new ImageBrush();

        public Difficulte()
        {
            InitializeComponent();

            fond_difficulte.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/ImageMenu.jpg"));
            fond_Difficulte.Fill = fond_difficulte;

            fleche.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image/fleche.png"));
            retourimage.Background = fleche;
        }

        private void Selection(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /*private void choix_difficulte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }*/

        private void butFacile_Click(object sender, RoutedEventArgs e)
        {
            tempsMin = 5;
            tempsMax = 10;
            vitesseMin = 10;
            vitesseMax = 20;
            this.DialogResult = true;
        }

        private void butMoyen_Click(object sender, RoutedEventArgs e)
        {
            tempsMin = 3;
            tempsMax = 8;
            vitesseMin = 20;
            vitesseMax = 30;
            this.DialogResult = true;
        }

        private void butDifficile_Click(object sender, RoutedEventArgs e)
        {
            tempsMin = 1;
            tempsMax = 5;
            vitesseMin = 30;
            vitesseMax = 40;
            this.DialogResult = true;
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
