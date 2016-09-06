using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ComponentModel;
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
using System.Xml;
using System.IO;

namespace ExamForm
{
    /// <summary>
    /// Логика взаимодействия для WindowConfig.xaml
    /// </summary>
    public partial class WindowConfig : Window
    {
        SqlConnectionStringBuilder _connectionString = new SqlConnectionStringBuilder();

        BackgroundWorker backWorker;
        
        public string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder tmp = new SqlConnectionStringBuilder();
                if (comboBoxType.SelectedIndex == 0)
                {
                    tmp.DataSource = localSource;
                    tmp.AttachDBFilename = _connectionString.AttachDBFilename;
                    tmp.IntegratedSecurity = _connectionString.IntegratedSecurity;
                }
                else
                {
                    tmp.DataSource = netSource;
                    tmp.InitialCatalog = _connectionString.InitialCatalog;
                    tmp.UserID = _connectionString.UserID;
                    tmp.Password = _connectionString.Password;
                }



                return tmp.ConnectionString;
            }
        }

        string localSource = "";
        string netSource = "";
        string NameConfig = @"Connect.xml";

        public WindowConfig()
        {
            InitializeComponent();
            comboBoxType.Items.Add("Локальне підключення");
            comboBoxType.Items.Add("Мережеве підключення");

            comboBoxType.SelectedIndex = 0;

            comboBoxSec.Items.Add("Так");
            comboBoxSec.Items.Add("Ні");

            FillForm(ReadFile(NameConfig));
            ShowCorrect(comboBoxType.SelectedIndex == 0);           

            backWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));

            progressbar.Visibility = Visibility.Hidden;

        }

        private void comboBox_DropDownOpened(object sender, EventArgs e)
        {

        }

        private void ShowCorrect(bool IsLocal)
        {
            if (IsLocal)
            {
                labelCatalog.Visibility = Visibility.Hidden;
                textBoxCatalog.Visibility = Visibility.Hidden;
                labelUser.Visibility = Visibility.Hidden;
                textBoxLogin.Visibility = Visibility.Hidden;
                labelPass.Visibility = Visibility.Hidden;
                textBoxPass.Visibility = Visibility.Hidden;

                buttonChoose.Visibility= Visibility.Visible;
                labelFile.Visibility = Visibility.Visible;
                textBoxAttachFile.Visibility = Visibility.Visible;
                labelSecurity.Visibility = Visibility.Visible;
                comboBoxSec.Visibility = Visibility.Visible;
                textBoxSource.Text = localSource;

            }
            else
            {
                labelFile.Visibility = Visibility.Hidden;
                textBoxAttachFile.Visibility = Visibility.Hidden;
                labelSecurity.Visibility = Visibility.Hidden;
                comboBoxSec.Visibility = Visibility.Hidden;
                buttonChoose.Visibility = Visibility.Hidden;

                labelCatalog.Visibility = Visibility.Visible;
                textBoxCatalog.Visibility = Visibility.Visible;
                labelUser.Visibility = Visibility.Visible;
                textBoxLogin.Visibility = Visibility.Visible;
                labelPass.Visibility = Visibility.Visible;
                textBoxPass.Visibility = Visibility.Visible;
                textBoxSource.Text = netSource;
            }
        }

        private SqlConnectionStringBuilder ReadFile(string nameFile)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(nameFile);//@"../Connect.xml");

                SqlConnectionStringBuilder rezult = new SqlConnectionStringBuilder();

                var root = doc.GetElementsByTagName("Connect");

                if (!string.IsNullOrWhiteSpace(root[0]["SourceDataL"].InnerText))
                    localSource = root[0]["SourceDataL"].InnerText;
                

                if (!string.IsNullOrWhiteSpace(root[0]["SourceDataN"].InnerText))
                    netSource = root[0]["SourceDataN"].InnerText;

                if (!string.IsNullOrWhiteSpace(root[0]["InitialCatalog"].InnerText))
                    rezult.InitialCatalog = root[0]["InitialCatalog"].InnerText;
              
                if (!string.IsNullOrWhiteSpace(root[0]["AttachDbFilename"].InnerText))
                    rezult.AttachDBFilename = root[0]["AttachDbFilename"].InnerText;
                else
                    rezult.AttachDBFilename = System.IO.Path.Combine(Directory.GetCurrentDirectory(), string.IsNullOrWhiteSpace(root[0]["InitialCatalog"].InnerText) ? "" : root[0]["InitialCatalog"].InnerText + ".mdf");


                if (!string.IsNullOrWhiteSpace(root[0]["Security"].InnerText))
                    rezult.IntegratedSecurity = bool.Parse(root[0]["Security"].InnerText);

                if (!string.IsNullOrWhiteSpace(root[0]["User"].InnerText))
                    rezult.UserID = root[0]["User"].InnerText;

                if (!string.IsNullOrWhiteSpace(root[0]["Password"].InnerText))
                    rezult.Password = root[0]["Password"].InnerText;

                if (!string.IsNullOrWhiteSpace(root[0]["Islocal"].InnerText))
                    if (bool.Parse(root[0]["Islocal"].InnerText))
                        comboBoxType.SelectedIndex = 0;
                    else
                        comboBoxType.SelectedIndex = 1;


                _connectionString = rezult;

                return rezult;
            }
            catch (Exception ec)
            {
                MessageBox.Show("Проблеми з читанням файлу налаштувань "+ ec.Message);
                return new SqlConnectionStringBuilder();
            }
        }

        private void WriteFile(string nameFile)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(nameFile);

            var root = doc.GetElementsByTagName("Connect");

            if (comboBoxType.SelectedIndex == 0)
            {
                root[0]["SourceDataL"].InnerText = localSource;
                root[0]["AttachDbFilename"].InnerText = _connectionString.AttachDBFilename;
                root[0]["Security"].InnerText = _connectionString.IntegratedSecurity.ToString();
            }
            else
            {
                root[0]["SourceDataN"].InnerText = netSource;
                root[0]["InitialCatalog"].InnerText = _connectionString.InitialCatalog;
                root[0]["User"].InnerText = _connectionString.UserID;
                root[0]["Password"].InnerText = _connectionString.Password;
            }
            root[0]["Islocal"].InnerText = comboBoxType.SelectedIndex == 0 ? "true" : "false";
            doc.Save(nameFile);

        }

        private void comboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowCorrect(comboBoxType.SelectedIndex == 0);

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReadForm();
                //backWorker.RunWorkerAsync(ConnectionString);
                WriteFile(NameConfig);
            }
            catch (Exception)
            {
                MessageBox.Show("Перевірте параметри\n" + ConnectionString);
            }
            DialogResult = true;
            this.Close();
        }

        private void FillForm(SqlConnectionStringBuilder rezult)
        {
            if (!string.IsNullOrWhiteSpace(rezult.DataSource))
                textBoxSource.Text = rezult.DataSource;
            if (!string.IsNullOrWhiteSpace(rezult.InitialCatalog))
                textBoxCatalog.Text = rezult.InitialCatalog;
            if (!string.IsNullOrWhiteSpace(rezult.AttachDBFilename))
                textBoxAttachFile.Text = rezult.AttachDBFilename;

            comboBoxSec.SelectedValue = rezult.IntegratedSecurity ? "Так" : "Ні";

            if (!string.IsNullOrWhiteSpace(rezult.UserID))
                textBoxLogin.Text = rezult.UserID;
            if (!string.IsNullOrWhiteSpace(rezult.Password))
                textBoxPass.Text = rezult.Password;


        }

        private void ReadForm()
        {

            if (comboBoxType.SelectedIndex == 0)//local
            {
                localSource = textBoxSource.Text;
                _connectionString.AttachDBFilename = textBoxAttachFile.Text;
                _connectionString.IntegratedSecurity = comboBoxSec.SelectedIndex == 0 ? true : false;
            }
            else
            {
                netSource = textBoxSource.Text;
                _connectionString.InitialCatalog = textBoxCatalog.Text;
                if (!string.IsNullOrWhiteSpace(textBoxLogin.Text))
                    if (!string.IsNullOrWhiteSpace(textBoxPass.Text))
                    {
                        _connectionString.UserID = textBoxLogin.Text;
                        _connectionString.Password = textBoxPass.Text;
                    }
                    else
                        MessageBox.Show("Якщо ввели логін--пароль тоже потрібен");
            }
        }

        private void buttonCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                buttonCheck.IsEnabled = false;
                button.IsEnabled = false;
                progressbar.Visibility = Visibility.Visible;
                ReadForm();
                backWorker.RunWorkerAsync(ConnectionString);
                WriteFile(NameConfig);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        { 
            try
            {
                string connetcString = e.Argument.ToString();
                using (var db = new DBWokrSql(connetcString))
                    e.Cancel = true;               
            }
            catch (Exception ex)
            {
                e.Result =(object) "Не має зв`язку!\n" + ex.Message;               
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            progressbar.Visibility = Visibility.Hidden;

            if (e.Cancelled)
                MessageBox.Show("Усе добре!");
            else
                MessageBox.Show(e.Result.ToString());

            buttonCheck.IsEnabled = true;
            button.IsEnabled = true;
        }

        private void buttonChoose_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Обераємо файл бази даних";

            dlg.Filter = "mdf files (*.mdf)|*.mdf|All files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;

            Nullable<bool> rezult = dlg.ShowDialog();
            if (rezult == true)
                textBoxAttachFile.Text= dlg.FileName;

        }

        private void textBoxAttachFile_TextChanged(object sender, TextChangedEventArgs e) { }

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
                buttonCheck.Content = newGrid_online;
                newGrid_online.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage_online = new Image() { };
                newGrid_online.Children.Add(newImage_online);
                newImage_online.Source = new BitmapImage(new Uri("bin/Resource/reload.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage_online, 0);
            }
        }
    }
}
