using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using ExamForm.Manager;
using System.Threading;

namespace ExamForm
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ConnectionString = @"Data Source=(localdb)\mssqllocaldb;AttachDbFilename=D:\Exam.mdf;Integrated Security=True";// @"Data Source =192.168.202.5; Initial Catalog = Exam; User ID=XXXX; Password=XXXX;";

        WindowConfig winConf;

        BackgroundWorker backWorker;

        public MainWindow()
        {
            InitializeComponent();

            radioButtonOnline.IsChecked = true;

            backWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));

            progressbar.Visibility = Visibility.Hidden;

        }

        private string NameFile = @"C:/Users/Public/";

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txSname.Text))
            { MessageBox.Show("Спочатку треба вказати прізвище"); return; }

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Білет_" + txSname.Text;
            dlg.DefaultExt = ".docx";
            Nullable<bool> rezult = dlg.ShowDialog();
            if (rezult == true)
                NameFile = dlg.FileName;

        }

        private void radioButtonOffline_Checked(object sender, RoutedEventArgs e)
        {
            buttonSave.Visibility = Visibility.Visible;
            buttonOk.ToolTip = "Створити";
        }

        private void radioButtonOnline_Checked(object sender, RoutedEventArgs e)
        {
            buttonSave.Visibility = Visibility.Hidden;
            buttonOk.ToolTip = "Розпочати";
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idLevel, idUser = 0;

                if (String.IsNullOrWhiteSpace(txSname.Text))
                { MessageBox.Show("Спочатку треба вказати ім'я"); return; }

                if (String.IsNullOrWhiteSpace(txFname.Text))
                { MessageBox.Show("Треба вказати прізвище"); return; }

                if (comboBoxLevel.SelectedValue == null || String.IsNullOrWhiteSpace(comboBoxLevel.SelectedValue.ToString()))
                { MessageBox.Show("Треба вказати рівень білета"); return; }

                if (String.IsNullOrWhiteSpace(txNumber.Text))
                { MessageBox.Show("Спочатку треба вказати табельний номер (тільки цифри)"); return; }

                if (!int.TryParse(txNumber.Text, out idUser))
                { MessageBox.Show("Табельний номер - це тільки цифри"); return; }

                using (DBWokrSql db = new DBWokrSql(ConnectionString))
                {
                    idLevel = db.GetIDLevel(comboBoxLevel.SelectedValue.ToString());
                    idUser = db.AddUser(idUser, txFname.Text, txSname.Text);
                }

                WindowTicket wticket = new WindowTicket(ConnectionString,
                                                      idLevel, idUser,
                                                      (bool)radioButtonOffline.IsChecked ? NameFile : ""
                                                      );
                if (!(bool)radioButtonOffline.IsChecked) wticket.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("buttonOk_Click " + ex.Message);
            }
            this.Close();
        }

        private void comboBoxLevel_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void comboBoxLevel_DropDownOpened(object sender, EventArgs e)
        {
            progressbar.Visibility = Visibility.Visible;
            comboBoxLevel.Items.Clear();
            comboBoxLevel.Items.Add("conection...");
            backWorker.RunWorkerAsync(ConnectionString);

        }

        private void buttonSetting_Click(object sender, RoutedEventArgs e)
        {
            winConf.ShowDialog();

            winConf = new WindowConfig();

            ConnectionString = winConf.ConnectionString;
        }

        private void buttonEnterAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowAuthetification wa = new WindowAuthetification(ConnectionString, AuthType.Enter);

                wa.ShowDialog();

                if (wa.DialogResult == true)
                {
                    MenuManager menu = new MenuManager(ConnectionString, wa.Login);

                    menu.ShowDialog();
                }
                else
                    MessageBox.Show("Дані аутинтифікації не вірні!", "Помилка");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            winConf.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Images
            // ButtonStart
            Grid newGrid = new Grid() { Margin = new Thickness(5) };
            buttonOk.Content = newGrid;
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            Image newImage = new Image() { };
            newGrid.Children.Add(newImage);
            newImage.Source = new BitmapImage(new Uri("bin/Resource/Pen.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            Grid.SetRow(newImage, 0);

            //Button-RadioButton Word
            Grid newGrid_offline = new Grid() { Margin = new Thickness(5) };
            radioButtonOffline.Content = newGrid_offline;
            newGrid_offline.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            Image newImage_offline = new Image() { };
            newGrid_offline.Children.Add(newImage_offline);
            newImage_offline.Source = new BitmapImage(new Uri("bin/Resource/Word 2007.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            Grid.SetRow(newImage_offline, 0);

            //Button-RadioButton Online
            Grid newGrid_online = new Grid() { Margin = new Thickness(5) };
            radioButtonOnline.Content = newGrid_online;
            newGrid_online.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            Image newImage_online = new Image() { };
            newGrid_online.Children.Add(newImage_online);
            newImage_online.Source = new BitmapImage(new Uri("bin/Resource/Clipboard.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            Grid.SetRow(newImage_online, 0);

            //Button Save
            Grid newGrid_save = new Grid() { Margin = new Thickness(5) };
            buttonSave.Content = newGrid_save;
            newGrid_save.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            Image newImage_save = new Image() { };
            newGrid_save.Children.Add(newImage_save);
            newImage_save.Source = new BitmapImage(new Uri("bin/Resource/My Documents.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            Grid.SetRow(newImage_save, 0);
            #endregion

            winConf = new WindowConfig();

            ConnectionString = winConf.ConnectionString;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                List<string> listLevels = new List<string>();
                using (DBWokrSql db = new DBWokrSql(ConnectionString))
                    foreach (var level in db.GetLevels())
                        listLevels.Add(level.Value);
                e.Cancel = false;
                e.Result = listLevels;
            }
            catch (Exception ex)
            {
                e.Cancel = false;
                e.Result = "Не має зв`язку з базою: " + ex.Message;
            }


        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            comboBoxLevel.Items.Clear();

            progressbar.Visibility = Visibility.Hidden;

            if (!e.Cancelled)

                if (e.Result is List<string>)
                {
                    List<string> ll = (List<string>)e.Result;

                    (ll).ForEach(item => comboBoxLevel.Items.Add(item));
                }
                else
                    MessageBox.Show(e.Result.ToString());


        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void txNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txNumber.Text)) { txNumber.Background = Brushes.AliceBlue; txFname.Text = txSname.Text = ""; return; }

            int number;

            if (!int.TryParse(txNumber.Text, out number))
            { txNumber.Background = Brushes.MediumVioletRed; return; }
            else
                txNumber.Background = Brushes.AliceBlue;

            using (DBWokrSql db = new DBWokrSql(ConnectionString))
                try
                {
                    var user = db.GetUserLike(number);
                    if (user.ID > 0)
                    {
                        txFname.Text = user.Fname;
                        txSname.Text = user.Lname;
                    }
                    else
                        txFname.Text = txSname.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
