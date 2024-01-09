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

namespace Sae_1._01_1._02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // booléens pour monter et descendre
        private bool goUp, goDown = false;



        public MainWindow()
        {
            InitializeComponent();      
            Menu fenetreDebut = new Menu();
            fenetreDebut.ShowDialog();

            
            InitializeComponent();


        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                g
            }
            if (e.Key == Key.Down)
            {

            }
        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key != Key.Up) { }
        }
    }
   


}
