using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
namespace ExamForm
{
    /// <summary>
    /// Логика взаимодействия для UserControlNewTicket.xaml
    /// </summary>
    public partial class UserControlNewQuestion : UserControl
    {
        ObservableCollection<AnswerInfo> ans = new ObservableCollection<AnswerInfo>();

        int ID;

        string Connect = "";      

        public UserControlNewQuestion(string Connection, int current, int all, QuestionInfo question, IEnumerable<AnswerInfo> answers)
        {
            InitializeComponent();

            listView.ItemsSource = ans;
            Connect = Connection;

            if (question == null) return;
                
            ID = question.ID;
            textBoxLevel.Text = question.Level;
            textBoxTopic.Text = question.Topic;
            textBoxInfo.Text = question.Info;
            textBoxQuestion.Text = question.Question;
            labelID.Content = "№ " + question.ID;
            labelCurrent.Content = string.Format(@"{0}/{1}", (current + 1), all);
            foreach (var item in answers)
                ans.Add(item);  
        }

        public UserControlNewQuestion(string connection) : this(connection, 0, 0, null, null){}

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(String.Format("Питання буде {0} у базі. Ви впевнені?", ID == 0 ? "збережено" : "змінено"), "Увага", MessageBoxButton.OKCancel) != MessageBoxResult.OK) return;

            if (String.IsNullOrWhiteSpace(textBoxLevel.Text)) { MessageBox.Show("Перевірте поле Рівень"); return; }
            if (String.IsNullOrWhiteSpace(textBoxTopic.Text)) { MessageBox.Show("Перевірте поле Тема "); return; }
            if (String.IsNullOrWhiteSpace(textBoxQuestion.Text)) { MessageBox.Show("Перевірте поле питання ");return; }

            int idTick = 0;
            using (DBWokrSql db = new DBWokrSql(Connect))
            {
                if (ID == 0)
                {
                    //create question
                    idTick = db.AddNewQuestion(
                        new QuestionInfo() { ID = 0, Topic = textBoxTopic.Text, Info = textBoxInfo.Text, Level = textBoxLevel.Text, Question = textBoxQuestion.Text },
                        ans
                        );
                }
                else
                {
                    //update question
                    var questionCurrent = db.GetQuestion(ID);
                    if (
                        !(questionCurrent.Info.Equals(textBoxInfo.Text.Replace('\'', ' ')) &&
                        questionCurrent.Level.Equals(textBoxLevel.Text) &&
                        questionCurrent.Question.Equals(textBoxQuestion.Text.Replace('\'', ' ')) &&
                        questionCurrent.Topic.Equals(textBoxTopic.Text))
                        )
                        idTick = db.UpdateQuestion(questionCurrent);

                    if (idTick == 0) //question didn`t change
                    {
                        //update answers
                        var answersCurrent = db.GetAnswers(ID);

                        //get all new answers and check them by similar in database, if they were not found -- add them
                        foreach (var item in ans)
                        {
                            if (answersCurrent.IsGetValue(item))
                            {
                                var ss = answersCurrent.Where(s => s.ID == item.ID).First();

                                if (ss.IsTrue == item.IsTrue && ss.Text.Equals(item.Text.Replace('\'', ' ')))
                                    continue;
                                else
                                    db.UpdateAnswer(ID, item);
                            }
                            else
                            {
                                int idAnswer = db.AddAnswer(item.Text);

                                db.AddQuestionAnswer(ID, idAnswer, item.IsTrue);
                            }

                        }

                        //marked all answers that not use
                        foreach (var item in answersCurrent)
                            if (!ans.IsGetValue(item))
                                db.DisableAnswer(item.ID);
                    }
                    else
                    {// add answers to new question
                        foreach (var item in ans)
                        {
                            int idAnswer = db.AddAnswer(item.Text);

                            db.AddQuestionAnswer(idTick, idAnswer, item.IsTrue);
                        }
                    }
                }

            }

            MessageBox.Show("Питання було успішно збережено під номером " + (idTick == 0 ? ID : idTick));
        }       

        private void textBoxLevel_TextChanged(object sender, TextChangedEventArgs e)
        {
            button.Visibility = Visibility.Visible;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            ans.Add(new AnswerInfo() { ID = 0, IsTrue = false, Text = "" });
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {            
            ans.Remove((AnswerInfo)listView.SelectedItem);

        }

        private void userControl_Loaded(object sender, RoutedEventArgs e)
        {
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

            // ButtonRemove
            {
                Grid newGrid = new Grid() { Margin = new Thickness(5) };
                buttonRemove.Content = newGrid;
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage = new Image() { };
                newGrid.Children.Add(newImage);
                newImage.Source = new BitmapImage(new Uri("bin/Resource/Remove.ico", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage, 0);
            }
        }
    }
}
