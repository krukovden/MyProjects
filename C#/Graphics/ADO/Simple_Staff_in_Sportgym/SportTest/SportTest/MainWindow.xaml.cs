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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SportTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataLINQDataContext x;



        workDataFunk wd;
        public MainWindow()
        {
            InitializeComponent();
            btExit.Click += btEnter_Click;
            x = new DataLINQDataContext();
            //x =new DataStepLinqDataContext();
            wd = new workDataFunk();



        }
       
        //add client
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ADD v = new ADD(true);
            if (v.ShowDialog()==true)
            {
                MessageBox.Show("Операция прошла удачно!!!");
            }
        
        }
        //delete client
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Search s = new Search("delete");
            s.ShowDialog();
        }
        //all info about clients
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

            OnlyView o = new OnlyView("client");
            o.ShowDialog();
        }
        /// <summary>
        /// добавить тренера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            ADD v = new ADD(false);
            if (v.ShowDialog() == true)
            {
                MessageBox.Show("Операция прошла удачно!!!");
            }
        }

       
        //list of all coaches
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            OnlyView o = new OnlyView("coach");
            o.ShowDialog();
        }

    
        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            OnlyView o = new OnlyView("payment");
            o.ShowDialog();
           
        }
        /// <summary>
        /// оплаченные клиенты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            OnlyView o = new OnlyView("goodClient");
            o.ShowDialog();
        }

        //Button Exit Enter
        private void btEnter_Click(object sender, RoutedEventArgs e)
        {

            string str = "";
            if (sender == btEnter)
            {
                str = "visit";

            }
            else if (sender == btExit)
            {
                str = "exit";
            }
            Search s = new Search(str);
            s.Number += new DelEvent(wd.Enter);

            if (s.ShowDialog() == false)
                return;

            var qwe = from t in wd.infoClients()
                      where workDataFunk.lsClient.ContainsKey(t.ID)
                      select new { name = t.Name, card = t.Card, dateoff = t.Dateoff, sex = t.Sex, Abonement = t.Abonement, count = t.Count };
           
            lbCount.Content = qwe.ToArray().Count();

            list.ItemsSource = qwe.ToArray();

        }
       
        //buy abon
        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            BuyAbon sho = new BuyAbon(false,0);
             sho.ShowDialog();
        }
        //search
        private void MenuItem_Click_12(object sender, RoutedEventArgs e)
        {
            Search s = new Search("search");
            s.ShowDialog();
        }
        //List coaches in gym
        private void MenuItem_Click_13(object sender, RoutedEventArgs e)
        {
            
            OnlyView o = new OnlyView("existCoach");
            o.ShowDialog();
           
        }

        private void MenuItem_Click_14(object sender, RoutedEventArgs e)
        {
            OnlyView o = new OnlyView("visitCoach");
            o.ShowDialog();
        }

        private void MenuItem_Click_15(object sender, RoutedEventArgs e)
        {
            OnlyView o = new OnlyView("visitClient");
            o.ShowDialog();
        }

        private void MenuItem_Click_16(object sender, RoutedEventArgs e)
        {
            OnlyView o = new OnlyView("historyBuy");
            o.ShowDialog();
        }



    }
}
