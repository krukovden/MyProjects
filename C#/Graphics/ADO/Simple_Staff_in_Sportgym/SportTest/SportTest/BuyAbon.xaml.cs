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
    /// Логика взаимодействия для BuyAbon.xaml
    /// </summary>
    public partial class BuyAbon : Window
    {
        workDataFunk funk;
        bool who;
         public BuyAbon() 
        {
            
            InitializeComponent();
            funk = new workDataFunk();
            lbCount.Visibility = Visibility.Hidden;
            tbCount.Visibility = Visibility.Hidden;
            lbName.Visibility = Visibility.Hidden;
            lbData.Visibility = Visibility.Hidden;
            lbTypeAbon.Visibility = Visibility.Hidden;
            cbType.Visibility = Visibility.Hidden;
            tbPrice.Visibility = Visibility.Hidden;
            lbPrice.Visibility = Visibility.Hidden;
            lbTitle.Content = "Абонемент клиента";
            
        }
        //false --abonement, true--coach
         public BuyAbon(bool f, int card) :this()
        {
           
           // InitializeComponent();
              funk = new workDataFunk();
              who = f;
             if(card>0)
             tbCard.Text = card.ToString();
            if (!f)
            {
                lbTitle.Content = "Абонемент клиента";
                cbType.ItemsSource = funk.infoAbon();
                lbCount.Content="Количество";
                cbType.Visibility = Visibility.Visible;
                lbTypeAbon.Visibility = Visibility.Visible;

            }
            else
            {
                lbTitle.Content = "Зарплата тренера";
                lbCount.Visibility = Visibility.Visible;
                tbCount.Visibility = Visibility.Visible;
                cbType.Visibility = Visibility.Hidden;
                lbTypeAbon.Visibility = Visibility.Hidden;
                lbCount.Content="грн\\час";
            }
          

        }

        
         void NameInLabel()
         {
             
             if ( Convert.ToInt32(tbCard.Text) > 0)
             {

                 if (funk.GetName(Convert.ToInt32(tbCard.Text)) == null)
                 {
                     MessageBox.Show("Такого пользователя не существует");
                     return;
                 }
                 else
                 {
                     lbName.Content = funk.GetName(Convert.ToInt32(tbCard.Text));
                     lbName.Visibility = Visibility.Visible;
                 }

                 
             }                                   
         }

         private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             switch (e.AddedItems[0].ToString())
             {
                 case"Утрений":
                 case"Дневной":
                 case"Вечерний":
                 case"Безлимит":
                       lbName.Visibility = Visibility.Visible;
                 lbData.Visibility = Visibility.Visible;
                 lbCount.Visibility = Visibility.Hidden;
                 tbCount.Visibility = Visibility.Hidden;
                 tbCount.Text ="0";
                     break;
                 case"Разовый":
                        lbCount.Visibility = Visibility.Visible;
                 tbCount.Visibility = Visibility.Visible;
                     break;

               
             }
             lbDatacount.Content = DateTime.Now.Day+"."+DateTime.Now.AddMonths(1).Month+"."+DateTime.Now.Year;
             lbDatacount.Visibility = Visibility.Visible;
             tbPrice.Visibility = Visibility.Visible;
             lbPrice.Visibility = Visibility.Visible;

         }

     
        private void tbCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameInLabel();
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(tbCard.Text) <= 0 || (!who && cbType.SelectedValue != null && tbPrice.Text.Length < 0))
            {
                                    
                MessageBox.Show("Не коректно заполнены поля");
                return;
            }
            if (who)
            {
                if (funk.AddInfoCoach(Convert.ToInt32(tbCount.Text), Convert.ToInt32(tbCard.Text)))
                {
                    DialogResult = true;
                    return;
                }
            }
            else
            {
                if (funk.AddAbonInfo(cbType.SelectedValue.ToString(), Convert.ToInt32(tbCount.Text), DateTime.Parse(lbDatacount.Content.ToString()), Convert.ToInt32(tbCard.Text), Convert.ToDecimal(tbPrice.Text)))
                {
                    //////////////////////////////
                    DialogResult = true;
                    return;
                }
            }
            
        }      
                
                
    }           
}
