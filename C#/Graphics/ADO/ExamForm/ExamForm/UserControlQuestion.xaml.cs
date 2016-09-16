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

namespace ExamForm
{
    /// <summary>
    /// Логика взаимодействия для UserControlQuestion.xaml
    /// </summary>
    public partial class UserControlQuestion : UserControl
    {
        Dictionary<int,string> Answers = new Dictionary<int,string>();

        int id_question;
        public int IDquestion { get { return id_question; }  }
        bool IsCheck = false;
               

        public UserControlQuestion(QuestionInfo question, IEnumerable<AnswerInfo> answers, bool isCheck=false)
        {
            InitializeComponent();

            id_question = question.ID;
            labelTopic.Text = question.Topic;
            labelQuestion.Text = question.Question;
            labeInfo.Text = question.Info;

            IsCheck = isCheck;            

            foreach (AnswerInfo answer in answers)
            {
                if (!IsCheck)
                {
                    CheckBox ch = new CheckBox();
                    ch.Margin = new Thickness(10);
                    ch.Content = answer.Text;
                    ch.FontSize = 16;
                    ch.FontFamily = new FontFamily("Times New Roman");
                    StackAnswer.Children.Add(ch);
                    Answers.Add(answer.ID, answer.Text);///????????????????????????
                }
                else
                {
                    Label ch = new Label();
                    ch.Margin = new Thickness(10);
                    ch.Content = answer.Text;
                    ch.FontSize = 16;
                    ch.FontFamily = new FontFamily("Times New Roman");
                    if (answer.IsTrue)
                        ch.Background = Brushes.GreenYellow;
                    else
                        ch.Background = Brushes.Red;

                    StackAnswer.Children.Add(ch);
                }
                
            }            
        }

        public IEnumerable<int> GetUserAnswers()
        {
            List<int> answer = new List<int>();
            if(!IsCheck)
            foreach (CheckBox Answer in StackAnswer.Children)
                if(Answer.IsChecked==true)
                     answer.Add(Answers.FirstOrDefault(x=>x.Value==Answer.Content.ToString()).Key); /// check

            return answer;
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
           // bool Is
        }
    }
}
