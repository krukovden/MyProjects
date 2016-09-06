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

namespace ExamForm
{
    /// <summary>
    /// Interaction logic for WindowProtocol.xaml
    /// </summary>
    public partial class WindowProtocol : Window
    {
        String Connection = "";
        BackgroundWorker backWorker;
        List<Protocol> rezult = new List<Protocol>();

        public WindowProtocol(string connection)
        {
            InitializeComponent();
            Connection = connection;
            progressbar.Visibility = Visibility.Hidden;
            backWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var rez = CastType(e.Argument, new { dateFrom = new DateTime(), dateTo = new DateTime(), rezTest=new RezultTest() });

                using (DBWokrSql db = new DBWokrSql(Connection))
                    e.Result = db.GetProtocol(rez.dateFrom, rez.dateTo, rez.rezTest);
            }
            catch (Exception ex)
            {
                e.Result = "Не має зв`язку з базою: " + ex.Message;
            }
        }
        T CastType<T>(object obj, T type) { return (T)obj; }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listView.Items.Clear();

            if (e.Result is string)
                MessageBox.Show((string)e.Result);
            else
                if (e.Result is IEnumerable<Protocol>)
            {
                foreach (var item in (e.Result as IEnumerable<Protocol>))
                    listView.Items.Add(item);

                rezult = (e.Result as IEnumerable<Protocol>).ToList();

            }

            progressbar.Visibility = Visibility.Hidden;

        }

        private void Search()
        {
            if (pick1.SelectedDate == null || pick2.SelectedDate == null) { MessageBox.Show("Вкажіть період !!!"); return; }

            progressbar.Visibility = Visibility.Visible;

            RezultTest RezTest = RezultTest.All;

            int count = VisualTreeHelper.GetChildrenCount(groupBox);

            for (int i = 0; i < count; i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(groupBox, i);

                if (childVisual is RadioButton)
                {
                    RadioButton rb = (RadioButton)childVisual;

                    if ((bool)rb.IsChecked)
                    {
                        if (rb.Content.ToString() == "Успішні")
                            RezTest = RezultTest.Good;
                        else
                             if (rb.Content.ToString() == "Провалені")
                            RezTest = RezultTest.Bad;

                        break;
                    }
                }
            }

            object param_ = new { dateFrom = (DateTime)pick1.SelectedDate, dateTo = (DateTime)pick2.SelectedDate, rezTest = RezTest };

            backWorker.RunWorkerAsync(param_);
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {            
            Search();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            //button search
            {
                Grid newGrid = new Grid() { Margin = new Thickness(5) };
                buttonSearch.Content = newGrid;
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage = new Image() { };
                newGrid.Children.Add(newImage);
                newImage.Source = new BitmapImage(new Uri("bin/Resource/Change.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage, 0);
            }

            //button wrod
            {
                Grid newGrid = new Grid() { Margin = new Thickness(5) };
                buttonWord.Content = newGrid;
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage = new Image() { };
                newGrid.Children.Add(newImage);
                newImage.Source = new BitmapImage(new Uri("bin/Resource/Word 2007.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage, 0);
            }

            GridView myGrid = new GridView();
            myGrid.AllowsColumnReorder = true;

            GridViewColumn colum1 = new GridViewColumn();
            colum1.DisplayMemberBinding = new Binding("ticket.user.ID");
            colum1.Header = "Таб номер";
            myGrid.Columns.Add(colum1);

            GridViewColumn colum2 = new GridViewColumn();
            colum2.DisplayMemberBinding = new Binding("ticket.user.Lname");
            colum2.Header = "Прізвище";
            colum2.Width = 120;
            myGrid.Columns.Add(colum2);

            GridViewColumn colum3 = new GridViewColumn();
            colum3.DisplayMemberBinding = new Binding("ticket.level");
            colum3.Header = "Рівень";
            colum3.Width = 100;
            myGrid.Columns.Add(colum3);

            GridViewColumn colum4 = new GridViewColumn();
            colum4.DisplayMemberBinding = new Binding("ticket.date");
            colum4.Header = "Дата";
            colum4.Width = 85;
            myGrid.Columns.Add(colum4);

            GridViewColumn colum5 = new GridViewColumn();
            colum5.DisplayMemberBinding = new Binding("ticket.IsPassed");
            colum5.Header = "Результат";
            myGrid.Columns.Add(colum5);

            listView.View = myGrid;
            listView.FontSize = 16;

        }

        private void buttonWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rezult.Count() == 0)
                {
                    MessageBox.Show("За цей період або нічого не знайдено, або ви не відібрали інформацію яку потрібно записати до файлу. Спочатку вкажіть період проведення тестування та натисніть кнопку Пошук для того, щоб відібрати результати");
                    return;
                }
                string NameFile;
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Протокол тестування_" + DateTime.Now.ToString("yyyy-MM-d--HH-mm-ss");
                dlg.DefaultExt = ".docx";
                Nullable<bool> rezultDLG = dlg.ShowDialog();
                if (rezultDLG == true)
                    NameFile = dlg.FileName;
                else return;

                if (DBWokrSql.WriteProtocol(rezult, NameFile, (DateTime)pick1.SelectedDate, (DateTime)pick2.SelectedDate))
                    System.Diagnostics.Process.Start("WINWORD.EXE", "\"" + NameFile + "\"");
                else
                    MessageBox.Show("Увага!! Помилка при створені файлу");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rezult.Count == 0) return;            

                Search();
                
            
        }
    }
}
