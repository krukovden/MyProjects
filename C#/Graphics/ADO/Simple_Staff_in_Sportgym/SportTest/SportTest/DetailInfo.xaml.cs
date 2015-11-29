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

namespace SportTest
{
    /// <summary>
    /// Логика взаимодействия для DetailInfo.xaml
    /// </summary>
    public partial class DetailInfo : Window
    {
        workDataFunk w;
        int num;
        public DetailInfo()
        {
            InitializeComponent();
            w = new workDataFunk();
            num = 0;
        }
        public DetailInfo(int card ):this()
        {
            Zapolnit(card);
        }
        public DetailInfo(string name)
            : this()
        {
            Zapolnit(name);
        }

        public void Zapolnit( int n)
        {
            try
            {
                if (!w.GetInfo(n))
                    return;
                Mozgi();
            }
            catch
            {
            }

        }
        public void Zapolnit(string n)
        {
            try
            {
                if (!w.GetInfo(n))
                    return;
                Mozgi();
            }
            catch
            {
            }

        }
        void Mozgi()
        {
            if (w.SearchDataClien != null)
            {
                Abon1.Visibility = Visibility.Visible;
                Abon2.Visibility = Visibility.Visible;
                Abon3.Visibility = Visibility.Visible;
                Count1.Visibility = Visibility.Visible;
                Count2.Visibility = Visibility.Visible;
                Count3.Visibility = Visibility.Visible;
                DataEnd.Visibility = Visibility.Visible;
                Price.Visibility = Visibility.Visible;
                //client = w.SearchDataClien;
                Name.Content = w.SearchDataClien.Name;
                Prof.Content = "клиент";
                Card.Content = w.SearchDataClien.Card;
                Birthday.Content = w.SearchDataClien.Birthday.Value.Date;
                Email.Content = w.SearchDataClien.Mail;
                Tel.Content = w.SearchDataClien.Tel;

                Money2.Content = "";
                Money1.Visibility = Visibility.Collapsed;
                Money2.Visibility = Visibility.Collapsed;


                Abon2.Content = w.SearchDataClien.Abonement;
                Count2.Content = w.SearchDataClien.Count;
                if (w.SearchDataClien.Count < 1)
                {
                    Count1.Visibility = Visibility.Collapsed;
                    Count2.Visibility = Visibility.Collapsed;
                    Count3.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Count1.Visibility = Visibility.Visible;
                    Count2.Visibility = Visibility.Visible;
                    Count3.Visibility = Visibility.Visible;
                }


                DataEnd.Content = w.SearchDataClien.Dateoff;
                Price.Content = "";



            }
            else
            {
                Name.Content = w.SearchDataCoach.Name;
                Prof.Content = "тренер";
                Card.Content = w.SearchDataCoach.Card;
                Birthday.Content = w.SearchDataCoach.Birthday.Value.Date;
                Email.Content = w.SearchDataCoach.Mail;
                Tel.Content = w.SearchDataCoach.Tel;

                Money2.Content = w.SearchDataCoach.Money;
                Money1.Visibility = Visibility.Visible;
                Money2.Visibility = Visibility.Visible;

                Abon1.Visibility = Visibility.Hidden;
                Abon2.Visibility = Visibility.Hidden;
                Abon3.Visibility = Visibility.Hidden;

                Count1.Visibility = Visibility.Hidden;
                Count2.Visibility = Visibility.Hidden;
                Count3.Visibility = Visibility.Hidden;

                DataEnd.Visibility = Visibility.Hidden;
                Price.Visibility = Visibility.Hidden;
            }
        }

    }
}
