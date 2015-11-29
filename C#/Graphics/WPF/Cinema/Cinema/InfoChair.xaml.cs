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
    /// Interaction logic for InfoChair.xaml
    /// </summary>
    public partial class InfoChair : Window
    {
        public InfoChair()
        {
            InitializeComponent();
        }
        public InfoChair(string Film, string Start, int Row, int Place, int Price, string nameowner, string pho )
        {
            InitializeComponent();
            FilmName.Content = Film;
            startfilm.Content = Start;
            row.Content = Row;
            place.Content = Place;
            price.Content = Price;
            owner.Content = nameowner;
            mobile.Content = pho;
        }
    }
}
