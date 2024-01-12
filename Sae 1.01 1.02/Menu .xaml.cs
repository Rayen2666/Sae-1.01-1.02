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
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {

        ImageBrush image = new ImageBrush();


        public Menu()
        {
            InitializeComponent();
            image.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image2.png"));
            image2.Fill = image;


        }

        private void Jouer_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }


            private void Parametre_Button_Click(object sender, RoutedEventArgs e)
        {
            Parametres fenetreParametres = new Parametres();
            fenetreParametres.ShowDialog();
        }

      

        private void Annuler_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
           
        }
    }
}
