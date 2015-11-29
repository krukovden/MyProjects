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
    /// Interaction logic for Print.xaml
    /// </summary>
    public partial class Print : Window
    {
        public Print()
        {
            InitializeComponent();
        }
        public Print( string Film, string Start,int Row,int Place, int Price)
        {
            InitializeComponent();
            FilmName.Content = Film;
            startfilm.Content = Start;
            row.Content = Row;
            place.Content = Place;
            price.Content = Price;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("OK");
            this.Close();
        }
    }
}
