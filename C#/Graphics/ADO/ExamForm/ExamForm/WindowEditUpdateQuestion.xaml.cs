using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для WindowEditUpdateQuestion.xaml
    /// </summary>
    public partial class WindowEditUpdateQuestion : Window
    {
        string Connection = "";
        ObservableCollection<Data> topics = new ObservableCollection<Data>();
        ObservableCollection<Data> levels = new ObservableCollection<Data>();

        Dictionary<QuestionInfo, IEnumerable<AnswerInfo>> currentQuestions = new Dictionary<QuestionInfo, IEnumerable<AnswerInfo>>();
        int currentQuest = 0;
        BackgroundWorker backWorker;

        public WindowEditUpdateQuestion(string connection)
        {
            InitializeComponent();
            Connection = connection;
            progressbar.Visibility = Visibility.Visible;
            backWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));
            backWorker.RunWorkerAsync(Connection);
        }

        ObservableCollection<Data> Add(IDictionary<int, string> mas)
        {
            ObservableCollection<Data> datamas = new ObservableCollection<Data>();
            datamas.Add(new Data(0, "Усі"));
            foreach (var item in mas)
                datamas.Add(new Data(item.Key, item.Value));

            return datamas;
        }

        private void buttonright_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Cquest = currentQuestions.Keys.ElementAt(currentQuest = ++currentQuest % currentQuestions.Keys.Count);

                frameView.Navigate(new UserControlNewQuestion(Connection, currentQuest, currentQuestions.Keys.Count, Cquest, currentQuestions[Cquest]));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (textBox.Text == "Номер питання")
                textBox.Text = "";
        }

        void TextInputBox(string text = "")
        {
            int id_question = 0;

            if (string.IsNullOrWhiteSpace(textBox.Text)) { SearchQuestion(); return; }

            if (!int.TryParse(textBox.Text + text, out id_question) || id_question < 0) return;

            using (DBWokrSql db = new DBWokrSql(Connection))
            {
                var quest = db.GetIDQuestionLike(id_question);

                if (quest.Count() == 0) { labelSearch.Content = "Знайдено : 0"; frameView.Navigate(new UserControlNewQuestion(Connection)); return; }

                currentQuestions.Clear();

                foreach (var item in quest)
                {
                    var answers_ = db.GetAnswers(item.ID);

                    currentQuestions.Add(item, answers_);
                }
                currentQuest = 0;

                var Cquest = currentQuestions.Keys.ElementAt(currentQuest);

                labelSearch.Content = "Знайдено :" + currentQuestions.Keys.Count;

                frameView.Navigate(new UserControlNewQuestion(Connection, currentQuest, currentQuestions.Keys.Count, Cquest, currentQuestions[Cquest]));

            }
        }
      

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchQuestion();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchQuestion();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //button right
            {
                Grid newGrid = new Grid() { Margin = new Thickness(5) };
                buttonright.Content = newGrid;
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage = new Image() { };
                newGrid.Children.Add(newImage);
                newImage.Source = new BitmapImage(new Uri("bin/Resource/Right.ico", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage, 0);
            }

            //button add new question
            {
                Grid newGrid = new Grid() { Margin = new Thickness(5) };
                buttonAdd.Content = newGrid;
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage = new Image() { };
                newGrid.Children.Add(newImage);
                newImage.Source = new BitmapImage(new Uri("bin/Resource/Add.ico", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage, 0);
            }

            //button remove new question
            {
                Grid newGrid = new Grid() { Margin = new Thickness(5) };
                buttonDelete.Content = newGrid;
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage = new Image() { };
                newGrid.Children.Add(newImage);
                newImage.Source = new BitmapImage(new Uri("bin/Resource/Delete.ico", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage, 0);
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (comboBox == null) return;

            TextInputBox();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            SearchQuestion();

            frameView.Navigate(new UserControlNewQuestion(Connection, currentQuestions.Keys.Count, currentQuestions.Keys.Count, new QuestionInfo() { ID = 0, Info = "", Level = "", Question = "", Topic = "" }, new List<AnswerInfo>()));

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (DBWokrSql db = new DBWokrSql(Connection))

                db.DisableQuestion(currentQuestions.Keys.ElementAt(currentQuest).ID);

            SearchQuestion();
        }

        T CastType<T>(object obj, T type) { return (T)obj; }

        void SearchQuestion()
        {
            try
            {
                currentQuestions = new Dictionary<QuestionInfo, IEnumerable<AnswerInfo>>();
                var a = (comboBox1.SelectedItem as Data) == null ? 0 : (comboBox1.SelectedItem as Data).ID;
                var b = (comboBox.SelectedItem as Data) == null ? 0 : (comboBox.SelectedItem as Data).ID;
                currentQuestions.Clear();
                using (DBWokrSql db = new DBWokrSql(Connection))
                {
                    foreach (var item in db.GetIDQuestions(a, b))
                    {
                        var question_ = db.GetQuestion(item);

                        var answers_ = db.GetAnswers(item);

                        currentQuestions.Add(question_, answers_);
                    }
                }

                labelSearch.Content = "Знайдено :" + currentQuestions.Keys.Count;

                if (currentQuestions.Keys.Count == 0)
                {
                    frameView.Navigate(new UserControlNewQuestion(Connection)); return;
                }

                currentQuest = 0;

                var Cquest = currentQuestions.Keys.ElementAt(currentQuest);

                frameView.Navigate(new UserControlNewQuestion(Connection, currentQuest, currentQuestions.Keys.Count, Cquest, currentQuestions[Cquest]));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Question " + ex.Message);
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (DBWokrSql db = new DBWokrSql(Connection))
                {
                    var levels_ = (Dictionary<int, string>)db.GetLevels();
                    var topics_ = (Dictionary<int, string>)db.GetTopics();

                    var qq = new Dictionary<QuestionInfo, IEnumerable<AnswerInfo>>();

                    foreach (var item in db.GetIDQuestions(0, 0))
                    {
                        var question_ = db.GetQuestion(item);

                        var answers_ = db.GetAnswers(item);

                        qq.Add(question_, answers_);
                    }

                    e.Cancel = false;
                    e.Result = new { lev = levels_, topi = topics_, q = qq };
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
                var rez = CastType(e.Result, new { lev = new Dictionary<int, string>(), topi = new Dictionary<int, string>(), q = new Dictionary<QuestionInfo, IEnumerable<AnswerInfo>>() });

                if (rez != null)
                {
                    comboBox.ItemsSource = levels = Add(rez.lev);
                    comboBox1.ItemsSource = topics = Add(rez.topi);
                    currentQuestions = rez.q;

                    labelSearch.Content = "Знайдено :" + currentQuestions.Keys.Count;

                    if (currentQuestions.Keys.Count == 0)
                    {
                        frameView.Navigate(new UserControlNewQuestion(Connection)); progressbar.Visibility = Visibility.Hidden; return;
                    }

                    currentQuest = 0;

                    var Cquest = currentQuestions.Keys.ElementAt(currentQuest);

                    frameView.Navigate(new UserControlNewQuestion(Connection, currentQuest, currentQuestions.Keys.Count, Cquest, currentQuestions[Cquest]));
                }
                else
                    MessageBox.Show(e.Result.ToString());
            }

            progressbar.Visibility = Visibility.Hidden;

        }

    }
}
