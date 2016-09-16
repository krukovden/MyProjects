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
using System.Windows.Shapes;

namespace ExamForm.Manager
{
    /// <summary>
    /// Логика взаимодействия для MenuManager.xaml
    /// </summary>
    public partial class MenuManager : Window
    {
        string ConnectionString;
        string CurrentAdmin;
        public MenuManager(string connect, string login)
        {
            InitializeComponent();
            ConnectionString = connect;
            CurrentAdmin = login;            
        }

        private void btQuestions_Click(object sender, RoutedEventArgs e)
        {
            WindowEditUpdateQuestion wd = new WindowEditUpdateQuestion(ConnectionString);
            wd.Show();
        }

        private void buttonAdmins_Click(object sender, RoutedEventArgs e)
        {
            WindowStatistic ws = new WindowStatistic(TypeForm.Admin, ConnectionString, CurrentAdmin);
            ws.ShowDialog();
        }

        private void buttonProtocol_Click(object sender, RoutedEventArgs e)
        {
            WindowStatistic ws = new WindowStatistic(TypeForm.Users, ConnectionString, CurrentAdmin);
            ws.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // ButtonAdmin
            {
                Grid newGrid = new Grid() { Margin = new Thickness(5) };
                buttonAdmins.Content = newGrid;
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage = new Image() { };
                newGrid.Children.Add(newImage);
                newImage.Source = new BitmapImage(new Uri(@"../bin/Resource/Admin.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage, 0);
            }
            // ButtonUser
            {
                Grid newGrid_user = new Grid() { Margin = new Thickness(5) };
                buttonProtocol.Content = newGrid_user;
                newGrid_user.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Image newImage_user = new Image() { };
                newGrid_user.Children.Add(newImage_user);
                newImage_user.Source = new BitmapImage(new Uri(@"../bin/Resource/User.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                Grid.SetRow(newImage_user, 0);
            }

            // ButtonQuestion
            Grid newGrid_questio = new Grid() { Margin = new Thickness(5) };
            btQuestions.Content = newGrid_questio;
            newGrid_questio.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            Image newImage_question = new Image() { };
            newGrid_questio.Children.Add(newImage_question);
            newImage_question.Source = new BitmapImage(new Uri(@"../bin/Resource/Question.ico", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            Grid.SetRow(newImage_question, 0);

            // ButtonQuestion
            Grid newGrid_protocol = new Grid() { Margin = new Thickness(5) };
            buttonProtocol_Real.Content = newGrid_protocol;
            newGrid_protocol.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            Image newImage_Protocol = new Image() { };
            newGrid_protocol.Children.Add(newImage_Protocol);
            newImage_Protocol.Source = new BitmapImage(new Uri(@"../bin/Resource/Clipboard.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            Grid.SetRow(newImage_Protocol, 0);

        }

        private void buttonProtocol_Real_Click(object sender, RoutedEventArgs e)
        {
            WindowProtocol prot = new WindowProtocol(ConnectionString);
            prot.ShowDialog();
        }
    }
}
