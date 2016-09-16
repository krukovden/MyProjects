using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;


namespace ExamForm
{
    public class Users
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int Count { get; set; }
        public int Online { get; set; }
        public int Offline { get; set; }
        public int Passed { get; set; }
    }

    public enum TypeForm { Users, User, Admin }
    /// <summary>
    /// Логика взаимодействия для WindowStatistic.xaml
    /// </summary>
    public partial class WindowStatistic : Window
    {
        TypeForm Type_;
        string Connection;
        string CurrentAdmin;
        int Id_user = 0;
        BackgroundWorker backWorker;

        public WindowStatistic(TypeForm type_, string connection, string currentAdmin, int id_user = 0)
        {
            InitializeComponent();
            Connection = connection;
            Type_ = type_;
            CurrentAdmin = currentAdmin;
            Id_user = id_user;
            backWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));
        }

        private void lo_Loaded(object sender, RoutedEventArgs e)
        {
            // ButtonRemove
            {
                Grid newGrid = new Grid() { Margin = new Thickness(5) };
                buttonRemove.Content = newGrid;
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage = new Image() { };
                newGrid.Children.Add(newImage);
                newImage.Source = new BitmapImage(new Uri("bin/Resource/Delete.ico", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage, 0);
            }
            // ButtonAdd
            {
                Grid newGrid = new Grid() { Margin = new Thickness(5) };
                buttonAdd.Content = newGrid;
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage = new Image() { };
                newGrid.Children.Add(newImage);
                newImage.Source = new BitmapImage(new Uri("bin/Resource/Add.ico", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage, 0);
            }

            // ButtonDetail
            {
                Grid newGrid = new Grid() { Margin = new Thickness(5) };
                buttonChange.Content = newGrid;
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage = new Image() { };
                newGrid.Children.Add(newImage);
                newImage.Source = new BitmapImage(new Uri("bin/Resource/Change.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage, 0);
            }

            switch (Type_)
            {
                case TypeForm.Admin:
                    {
                        #region
                        label.Visibility = Visibility.Hidden;
                        textBox.Visibility = Visibility.Hidden;
                        buttonChange.ToolTip = "Змінити дані адміністратора";
                        buttonAdd.ToolTip = "Додати адміністратора";
                        buttonRemove.ToolTip = "Видалити адміністратора";

                        GridView myGrid = new GridView();
                        myGrid.AllowsColumnReorder = true;

                        GridViewColumn colum1 = new GridViewColumn();
                        colum1.DisplayMemberBinding = new Binding("Login");
                        colum1.Header = "Логін";
                        colum1.Width = 120;
                        myGrid.Columns.Add(colum1);

                        GridViewColumn colum2 = new GridViewColumn();
                        colum2.DisplayMemberBinding = new Binding("Password");
                        colum2.Header = "Пароль";
                        colum2.Width = 120;
                        myGrid.Columns.Add(colum2);

                        GridViewColumn colum3 = new GridViewColumn();
                        colum3.DisplayMemberBinding = new Binding("Owner");
                        colum3.Header = "Хто створив";
                        colum3.Width = 120;
                        myGrid.Columns.Add(colum3);

                        GridViewColumn colum4 = new GridViewColumn();
                        colum4.DisplayMemberBinding = new Binding("Date");
                        colum4.Header = "Коли";
                        colum4.Width = 85;
                        myGrid.Columns.Add(colum4);

                        listView.View = myGrid;
                        listView.FontSize = 16;

                        UpdateListView();

                        break;
                        #endregion
                    }
                case TypeForm.Users:
                    {
                        #region
                        buttonAdd.Visibility = Visibility.Hidden;
                        buttonRemove.Visibility = Visibility.Hidden;
                        textBox.Text = "Пошук";
                        buttonChange.ToolTip = "Детальніше про користувача";
                      

                        GridView myGrid = new GridView();
                        myGrid.AllowsColumnReorder = true;

                        GridViewColumn colum2 = new GridViewColumn();
                        colum2.DisplayMemberBinding = new Binding("Lname");
                        colum2.Header = "Прізвище";
                        colum2.Width = 90;
                        myGrid.Columns.Add(colum2);

                        GridViewColumn colum1 = new GridViewColumn();
                        colum1.DisplayMemberBinding = new Binding("Fname");
                        colum1.Header = "Им'я";
                        colum1.Width = 90;
                        myGrid.Columns.Add(colum1);


                        GridViewColumn colum3 = new GridViewColumn();
                        colum3.DisplayMemberBinding = new Binding("Count");
                        colum3.Header = "Спроби";
                        colum3.Width = 65;
                        myGrid.Columns.Add(colum3);

                        GridViewColumn colum4 = new GridViewColumn();
                        colum4.DisplayMemberBinding = new Binding("Online");
                        colum4.Header = "Онлайн";
                        colum4.Width = 70;
                        myGrid.Columns.Add(colum4);

                        GridViewColumn colum5 = new GridViewColumn();
                        colum5.DisplayMemberBinding = new Binding("Offline");
                        colum5.Header = "Оффлайн";
                        colum5.Width = 80;
                        myGrid.Columns.Add(colum5);

                        GridViewColumn colum6 = new GridViewColumn();
                        colum6.DisplayMemberBinding = new Binding("Passed");
                        colum6.Header = "Успішно";
                        colum6.Width = 70;
                        myGrid.Columns.Add(colum6);

                        listView.View = myGrid;
                        listView.FontSize = 16;

                        UpdateListView();

                        break;
                        #endregion
                    }
                case TypeForm.User:
                    {
                        #region
                        buttonAdd.Visibility = Visibility.Hidden;
                        buttonRemove.Visibility = Visibility.Hidden;
                        using (DBWokrSql db = new DBWokrSql(Connection))
                        {
                            textBox.Text = db.GetUser(Id_user).Fname+"   "+ db.GetUser(Id_user).Lname;
                        }
                        textBox.FontSize = 18;
                        textBox.IsReadOnly = true;
                        buttonChange.ToolTip = "Детальніше про білет користувача";


                        GridView myGrid = new GridView();
                        myGrid.AllowsColumnReorder = true;

                        GridViewColumn colum1 = new GridViewColumn();
                        colum1.DisplayMemberBinding = new Binding("ID");
                        colum1.Header = "Білет №";
                        colum1.Width = 70;
                        myGrid.Columns.Add(colum1);

                        GridViewColumn colum2 = new GridViewColumn();
                        colum2.DisplayMemberBinding = new Binding("level");
                        colum2.Header = "Рівень";
                        colum2.Width = 120;
                        myGrid.Columns.Add(colum2);

                        GridViewColumn colum3 = new GridViewColumn();
                        colum3.DisplayMemberBinding = new Binding("date");
                        colum3.Header = "Дата";
                        colum3.Width = 85;
                        myGrid.Columns.Add(colum3);

                        GridViewColumn colum4 = new GridViewColumn();
                        colum4.DisplayMemberBinding = new Binding("IsOffline");
                        colum4.Header = "Онлайн";
                        colum4.Width = 80;
                        myGrid.Columns.Add(colum4);

                        GridViewColumn colum5 = new GridViewColumn();
                        colum5.DisplayMemberBinding = new Binding("IsPassed");
                        colum5.Header = "Здав";
                        colum5.Width = 80;
                        myGrid.Columns.Add(colum5);

                        listView.View = myGrid;
                        listView.FontSize = 16;

                        UpdateListView();

                        break;
                        #endregion
                    }
            }
        }

        private void TextInput(string text = "")
        {
            if (Type_ != TypeForm.Users) return;

            IEnumerable<object> data = new object[0];
            using (DBWokrSql db = new DBWokrSql(Connection))
            {
                data = db.GetStatisticUsersLike(textBox.Text + text);
            }

            listView.Items.Clear();

            foreach (var item in data)
                listView.Items.Add(item);

            label.Content = "Знайдено :" + listView.Items.Count;

        }

        private void UpdateListView()
        {
            progressbar.Visibility = Visibility.Visible;

            backWorker.RunWorkerAsync(Connection);
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            try
            {
                using (DBWokrSql db = new DBWokrSql(Connection))
                {
                    switch (Type_)
                    {
                        case TypeForm.Admin:
                            e.Result = db.GetAdmins();
                            break;
                        case TypeForm.Users:
                            e.Result = db.GetStatisticUsers();
                            break;
                        case TypeForm.User:
                            e.Result = db.GetTickets(Id_user);
                            break;
                    }

                    e.Cancel = false;

                }
            }
            catch (Exception ex)
            {
                e.Cancel = false;
                e.Result = "Не має зв`язку з базою: " + ex.Message;
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                listView.Items.Clear();

                foreach (var item in (e.Result as IEnumerable<object>))
                    listView.Items.Add(item);
            }

            label.Content = "Знайдено :" + listView.Items.Count;
            progressbar.Visibility = Visibility.Hidden;

        }

        private void listView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null) { MessageBox.Show("Спочатку треба обрати користувача", "Увага"); return; }

            if (listView.SelectedItem is AdminInfo)
                using (DBWokrSql db = new DBWokrSql(Connection))
                    try { db.DeleteAdmin((listView.SelectedItem as AdminInfo).Login); } catch (Exception ex) { MessageBox.Show("Помилка при видалені\n" + ex.Message, "Увага"); }

            UpdateListView();

        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null) { MessageBox.Show("Спочатку треба обрати користувача", "Увага"); return; }

            switch (Type_)
            {
                case TypeForm.Admin:
                    WindowAuthetification wau = new WindowAuthetification(Connection, AuthType.Update, CurrentAdmin, (listView.SelectedItem as AdminInfo));

                    wau.ShowDialog();

                    UpdateListView();

                    break;
                case TypeForm.Users:
                case TypeForm.User:
                    SelectUser();
                    break;                
                    
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAuthetification wau = new WindowAuthetification(Connection, AuthType.Add, CurrentAdmin, new AdminInfo());

            wau.ShowDialog();

            UpdateListView();
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextInput(e.Text);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextInput();
        }

        bool first1 = true;
        private void textBox_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (first1 && Type_ != TypeForm.User)
                textBox.Text = "";
            first1 = false;
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectUser();
        }

        private void SelectUser()
        {
            if (listView.SelectedItem == null) { MessageBox.Show("Спочатку треба обрати елемент у таблиці", "Увага"); return; }

            if (listView.SelectedItem is Users)
            {
                    Users user = listView.SelectedItem as Users;                
                    int id = 0;
                    using (DBWokrSql db = new DBWokrSql(Connection))
                        id = db.GetIdUser(user.Fname, user.Lname);
                    WindowStatistic ws = new WindowStatistic(TypeForm.User, Connection, CurrentAdmin, id);
                    ws.ShowDialog();
                
            }
            else
                if(listView.SelectedItem is Ticket)
            {
                Ticket ticket = listView.SelectedItem as Ticket;

                WindowTicket wt = new WindowTicket(Connection, ticket.ID);

                wt.ShowDialog();
            }
        }        
       
    }
}
