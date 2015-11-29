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
    /// Interaction logic for Chair.xaml
    /// </summary>
    public partial class Chair : Window
    {
        public int Row = 0;
        public int Place = 0;
        public bool end = false;
        public Chair()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            end = false;
            try 
	{
        Row=Convert.ToInt32(textBox1.Text);
        Place = Convert.ToInt32(textBox2.Text);
        end = true;
                

	}
	catch (Exception)
	{

        MessageBox.Show("Неправильные данные");
	}
            this.Close();
           
        }
    }
}
