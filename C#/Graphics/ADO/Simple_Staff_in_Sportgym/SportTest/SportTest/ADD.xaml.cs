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
    /// Логика взаимодействия для ADD.xaml
    /// </summary>
    /// 
    public partial class ADD : Window
    {
        DataLINQDataContext x;
        bool nextAction = true;
        public ADD()
        {
            InitializeComponent();

           x = new DataLINQDataContext();
           cbPol.ItemsSource = (from t in x.Users
                                select t.gender).Distinct().ToArray();
            
        }
        /// <summary>
        /// Добавление челеовека: true это клиент, false-тренер
        /// </summary>
        public ADD(bool who):this()
        {
            //InitializeComponent();
            //x = new DataLINQDataContext();
            //cbPol.ItemsSource = (from t in x.Users
            //                     select t.gender).Distinct().ToArray();
            nextAction = who;
            if (who)
            {
                lbTitle.Content = "Добавления клиента";
            }
            else
            {
                lbTitle.Content = "Добавление тренера";
            }

        }
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tbName.Text.Length < 3 || cbPol.SelectedValue ==null || tbTel.Text.Length < 5 || picker.SelectedDate == null)
            {
                MessageBox.Show("Неправильно введенные данные");
                return;
            }
          

            workDataFunk w = new workDataFunk();
           

           if( w.AddPeople(tbName.Text, (DateTime)picker.SelectedDate, cbPol.SelectedValue.ToString(), tbmail.Text, tbTel.Text))
           {
               BuyAbon b;
               if(nextAction)
                 b = new BuyAbon(false, w.GetCard(tbName.Text));
                else
                   b=new BuyAbon(true,w.GetCard(tbName.Text));
               b.ShowDialog();
               DialogResult = true;
           }
         
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();

        }
    }
}
