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
    /// Interaction logic for Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public bool DialRez;

        public string NameData { get; set; }
        public string PhoneData { get; set; }
      

       

        
        public Autorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DialRez = false;
            this.Close();
            
        }

       
        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            if (Nametext.Text.Count() > 3)
            {
                if (number_Phone.Text.Count() > 5)
                {
                    NameData = Nametext.Text;
                    PhoneData = number_Phone.Text;
                       DialRez = true;
                    this.Close();
                }
            }
            else
                MessageBox.Show("Некоректно заполнены поля");
        }
    }
}
