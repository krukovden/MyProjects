using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
            Tafternoon.Click += new RoutedEventHandler(button1_Click);
            Tevening.Click += new RoutedEventHandler(button1_Click);
            Gmor.Click += new RoutedEventHandler(button1_Click);
            Gafter.Click += new RoutedEventHandler(button1_Click);
            Gevening.Click += new RoutedEventHandler(button1_Click);
            Mmor.Click += new RoutedEventHandler(button1_Click);
            Mafter.Click += new RoutedEventHandler(button1_Click);
            Mevening.Click += new RoutedEventHandler(button1_Click);

            
            
        }

        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int h = 0;
                int m=0;
            string name="";
            switch(b.Name)
            {
                case "Tmor":
                    h = 12;
                    m = 00;
                    name = "Tor";
                    break;
                case "Tafternoon":
                     h = 16;
                    m = 45;
                    name = "Tor";
                    break;
                case "Tevening":
                     h = 22;
                    m = 00;
                    name = "Tor";
                    break;

                case "Mmor":
                    h = 10;
                    m = 00;
                    name = "Machete";
                    break;
                case "Mafter":
                    h = 14;
                    m = 30;
                    name = "Machete";
                    break;
                case "Mevening":
                    h = 20;
                    m = 00;
                    name = "Machete";
                    break;

                case "Gmor":
                    h = 9;
                    m = 00;
                    name = "Gork";
                    break;
                case "Gafter":
                    h = 17;
                    m = 45;
                    name = "Gork";
                    break;
                case "Gevening":
                    h = 19;
                    m = 00;
                    name = "Gork";
                    break;


            }
            MainWindow mai = new MainWindow(name, 65, 85, 100, h, m);
            mai.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
