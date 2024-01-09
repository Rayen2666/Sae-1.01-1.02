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

namespace Sae_1._01_1._02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // booléens pour monter et descendre
        private bool goUp, goDown = false;
        int speed = 5;
        
        ImageBrush ArrierePlanSprite = new ImageBrush();
        DispatcherTimer jeuTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            Menu fenetreDebut = new Menu();
            fenetreDebut.ShowDialog();

            ArrierePlanSprite.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "image\\Fond.png"));
            ArrierePlan.Fill = ArrierePlanSprite;
            ArrierePlan2.Fill = ArrierePlanSprite;

            jeuTimer.Interval = TimeSpan.FromMilliseconds(20);
            jeuTimer.Tick += BoucleJeu; 



            DebutJeu();
        }

        private void BoucleJeu(object? sender, EventArgs e)
        {
            if (goUp && Canvas.GetTop(joueur) > 0)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - speed);
            }
        }



        private void DebutJeu()
        {
            jeuTimer.Start();
            goUp = false;
            goDown = false;
                      


        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
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
