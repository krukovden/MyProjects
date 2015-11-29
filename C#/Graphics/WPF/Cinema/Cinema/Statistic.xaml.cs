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
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        public Statistic()
        {
            InitializeComponent();
        }
        public Statistic(int r,int b, int a,int  aa)
        {
            InitializeComponent();
            res.Content = r.ToString();
            buy.Content = b.ToString();
            all.Content = a.ToString();
            maybe.Content = aa.ToString();
        }
    }
}
