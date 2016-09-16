using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.IO;

namespace ExamForm
{
    class InputParams
    {
        public int Id_level;
        public int Id_user;
        public string TicketStore;
        public bool IsNew;
    }

    class OutputParams
    {
        public int id_Ticket;
        public IDictionary<QuestionInfo, IEnumerable<AnswerInfo>> Ticket_;
    }

    public partial class WindowTicket : Window
    {
        bool SendRezult = false;
        bool IsTEST = false;
        string ConnectString = "";
        Timer timer;
        DateTime start;

        int Id_ticket;
        int Id_level;
        int Id_user;
        string TicketStore;
        bool IsShow = false;

        /*
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Button Choose
            {
                Grid newGrid_online = new Grid() { Margin = new Thickness(5) };
                buttonChoose.Content = newGrid_online;
                newGrid_online.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage_online = new Image() { };
                newGrid_online.Children.Add(newImage_online);
                newImage_online.Source = new BitmapImage(new Uri("bin/Resource/My Documents.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage_online, 0);
            }

            //Button Check
            {
                Grid newGrid_online = new Grid() { Margin = new Thickness(5) };
                buttonChoose.Content = newGrid_online;
                newGrid_online.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage_online = new Image() { };
                newGrid_online.Children.Add(newImage_online);
                newImage_online.Source = new BitmapImage(new Uri("bin/Resource/reload.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage_online, 0);
            }
        }
        */

        BackgroundWorker backWorker;

        public WindowTicket(string connectString, int id_ticket)
        {
            InitializeComponent();
            ConnectString = connectString;
            Id_ticket = id_ticket;
            IsShow = true;

            backWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));

            progressbar.Visibility = Visibility.Visible;

            backWorker.RunWorkerAsync();
        }

        public WindowTicket(string connectString, int id_level, int id_user, string ticketStore)
        {
            InitializeComponent();
            ConnectString = connectString;
            IsShow = false;
            Id_level = id_level;
            Id_user = id_user;
            TicketStore = ticketStore;

            backWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));

            progressbar.Visibility = Visibility.Visible;

            backWorker.RunWorkerAsync();

        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!IsShow)
                try
                {
                    using (DBWokrSql db = new DBWokrSql(ConnectString))
                    {
                        var Ticket = db.CreateTicket(Id_level);

                        var user = db.GetUser(Id_user);
                        bool IsOnline = string.IsNullOrWhiteSpace(TicketStore);
                        if (user.ID != 13)
                            Id_ticket = db.AddTicket(Ticket, Id_user, !IsOnline);
                        else
                            IsTEST = true;

                        if (!IsOnline)
                        {
                            TicketStore = Path.ChangeExtension(Path.Combine(Path.GetDirectoryName(TicketStore), Path.GetFileNameWithoutExtension(TicketStore) + Id_ticket), "docx");
                            //Write to file
                            if (db.WriteTicketToWord(Ticket, user, Id_ticket, TicketStore))
                                System.Diagnostics.Process.Start("WINWORD.EXE", "\"" + TicketStore + "\"");
                            else
                                MessageBox.Show("Увага!! Помилка при створені файлу");
                        }
                        else
                        {
                            e.Result = new OutputParams() { id_Ticket = Id_ticket, Ticket_ = Ticket };
                        }

                    }
                }
                catch (Exception ex)
                {
                    e.Cancel = false;
                    e.Result = "Не має зв`язку з базою: " + ex.Message;
                    throw new Exception(ex.Message);
                }
            else
            {
                using (DBWokrSql db = new DBWokrSql(ConnectString))
                {
                    e.Result = new OutputParams() { id_Ticket = Id_ticket, Ticket_ = db.GetTicketRezults(Id_ticket) };
                }
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
                if (!string.IsNullOrWhiteSpace(TicketStore)) { SendRezult = true; this.Close(); }
                else
                if (e.Result is OutputParams)
                {
                    var param = (OutputParams)e.Result;

                    labelNameTicket.Content = "Білет № " + param.id_Ticket;

                    int color = 0;

                    foreach (var item in param.Ticket_)
                    {
                        var panel = new UserControlQuestion(item.Key, item.Value, IsShow);
                        panel.Margin = new Thickness(10);
                        panel.Background = color % 2 == 0 ? Brushes.AliceBlue : Brushes.White; color += 1;
                        StackPanelQuestion.Children.Add(panel);
                    }
                    if (!IsShow)
                    {
                        timer = new Timer(1000);
                        timer.Elapsed += Timer_Elapsed;
                        timer.AutoReset = true;
                        timer.Enabled = true;
                        start = DateTime.Now;
                    }
                }



            progressbar.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (start.AddMinutes(20) < DateTime.Now)
            { MessageBox.Show("Час вийшов"); this.Close(); }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void CheckRezult(bool IsTest = false)
        {
            if (IsShow) return;

            timer.Stop();
            timer.Dispose();

            #region SendRezult            

            if (!IsTest && !SendRezult && !IsShow)
                using (var db = new DBWokrSql(ConnectString))
                {
                    foreach (UserControlQuestion item in StackPanelQuestion.Children)
                        foreach (var ans in item.GetUserAnswers())
                            db.AddRezult(Id_ticket, ans, item.IDquestion);

                    SendRezult = true;
                    #endregion

                    #region CheckRezult                 

                    int error = 0,
                    countAnswerTrue = 0,
                    userCountAnser = 0;

                    foreach (UserControlQuestion item in StackPanelQuestion.Children)
                    {
                        var answers = db.GetAnswers(item.IDquestion, true).Select(x => x.ID);

                        countAnswerTrue += answers.Count();

                        userCountAnser += item.GetUserAnswers().Count();

                        if (answers.Count() != item.GetUserAnswers().Count()) { error++; continue; }

                        foreach (var ans in item.GetUserAnswers())
                        {
                            if (!answers.Contains(ans))
                            {
                                error++; break;
                            }
                        }

                    }
                    //update Ticket column isPasses
                    if (error >= 3 || countAnswerTrue != userCountAnser)
                        MessageBox.Show("Ви не склали іспит");
                    else
                        MessageBox.Show("Вітаємо! Ви успішно склали іспит. Нам дуже приємно працювати з такими фахівцями як Ви.");

                    db.UpdateTicketRezult(Id_ticket, error < 3);
                    #endregion
                }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!SendRezult)
            {
                CheckRezult(IsTEST);

                if (!IsShow) { new WindowTicket(ConnectString, Id_ticket).Show(); }
            }
        }


    }
}
