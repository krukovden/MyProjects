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
    /// Логика взаимодействия для OnlyView.xaml
    /// </summary>
    public partial class OnlyView : Window
    {
        public OnlyView()
        {
            InitializeComponent();
        }
        public OnlyView(string type)
            : this()
        {
            workDataFunk w = new workDataFunk();
            DataLINQDataContext x = new DataLINQDataContext();
            switch (type)
            {
                case "client":
                    lbTitle.Content = "Список клиентов";
                    Datagrid.ItemsSource = w.infoClients();
                    this.Width = 930;
                    break;
                case "goodClient":
                    lbTitle.Content = "Список проплаченых клиентов";
                    Datagrid.ItemsSource = w.ClientProplacheno();
                    this.Width = 930;
                    break;
                case "coach":
                    lbTitle.Content = "Список тренеров";
                    Datagrid.ItemsSource = w.infoCoach();
                    this.Width = 930;
                    break;
                case "existCoach":
                    lbTitle.Content = "Список тренеров в зале";
                    var qwe = from t in w.infoCoach()
                              where workDataFunk.lsCoaches.ContainsKey(t.ID)
                              select new { ФИО = t.Name, Номер_карточки = t.Card, Пол = t.Sex, Время_прихода = workDataFunk.lsCoaches[t.ID] };
                    Datagrid.ItemsSource = qwe.ToList();
                    break;
                case "payment":
                    lbTitle.Content = "Зарплата тренеров";
                    var rez = x.Visiting.Where(s => x.Coach.Select(q => q.id_user).Contains(s.id_user)).ToList();
                    var rez2 = rez.Where(s => s.deteExit.Value.Month == DateTime.Now.Month).Select(s => s).ToList();
                    var rez3 = rez.Select(s => new { id_user = s.id_user, time = (s.deteExit.Value - s.dateEnter.Value).TotalMinutes });
                    var rez1 = from t in rez3
                               group t by t.id_user into grouping
                               select new { id_user = grouping.Key, time = grouping.Sum(f => f.time) };
                    var rezOtvet = from t in rez1
                                   join f in w.infoCoach()
                                   on t.id_user equals f.ID
                                   select new { ФИО = f.Name, зароботал = (decimal)(t.time / 60) * (f.Money), время_работы = (decimal)(t.time / 60), сумма_в_час = f.Money };
                    Datagrid.ItemsSource = rezOtvet.ToList();
                    break;
                case "visitCoach":
                    lbTitle.Content = "История посещения тренеров";
                    var visitCo = from t in w.infoCoach()
                              join f in x.Visiting
                              on t.ID equals f.id_user
                              select new { Имя = t.Name, Вход = f.dateEnter, Выход = f.deteExit };
                    Datagrid.ItemsSource = visitCo.ToList();
                    break;
                case "visitClient":
                    lbTitle.Content = "История посещения клиентов";
                    var visitCli = from t in w.infoClients()
                                  join f in x.Visiting
                                  on t.ID equals f.id_user
                                  select new { Имя = t.Name, Вход = f.dateEnter, Выход = f.deteExit };
                    Datagrid.ItemsSource = visitCli.ToList();
                    break;
                case "historyBuy":
                        lbTitle.Content = "История покупок клиентов";
                    var history = from t in w.infoClients()
                                  join f in x.History_buy
                                  on t.ID equals f.id_user
                                  select new { Имя = t.Name, Дата_покупки = f.dates, Вид_абонимента = t.Abonement, Цена=f.price };

                    Datagrid.ItemsSource = history.ToList();
                        break;
                    
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
