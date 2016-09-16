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

namespace ExamForm
{
    public enum AuthType { Enter, Add, Update }
    /// <summary>
    /// Логика взаимодействия для WindowAuthetification.xaml
    /// </summary>
    public partial class WindowAuthetification : Window
    {
        string Connection;
        string Father_;
        AdminInfo original;

        AuthType TYPE_;

        public string Login
        {
            get; private set;
        }       

        public WindowAuthetification(string connection, AuthType type, string currentAdmin=null, AdminInfo originalInfo=null)
        {
            InitializeComponent();

            Connection = connection;
            TYPE_ = type;
            Father_ = currentAdmin;
            original = originalInfo;
            
        }

        bool first = true, first1 = true;

        private void textBox1Password_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (first)
                textBoxLogin.Text = "";
            first = false;
        }

        private void textBoxPassword_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (first1)
                textBoxPassword.Text = "";
            first1 = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (TYPE_)
            {
                case AuthType.Enter:
                    buttonOk.Content = "Далі";
                    this.Title = "Аутентифікація";
                    break;
                case AuthType.Add:
                    buttonOk.Content = "Додати";
                    this.Title = "Новий користувач";
                    break;
                case AuthType.Update:
                    buttonOk.Content = "Змінити";
                    this.Title = "Виправлення користувача";
                    textBoxLogin.Text = original.Login;
                    textBoxPassword.Text = original.Password;
                    break;
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            using (DBWokrSql db = new DBWokrSql(Connection))
                try
                {
                    switch (TYPE_)
                    {
                        case AuthType.Enter:

                            int id = db.GetIDAdmin(textBoxLogin.Text, textBoxPassword.Text);

                            Login = textBoxLogin.Text;

                            if (id > 0)
                                this.DialogResult = true;
                            else
                                this.DialogResult = false;                            
                            break;
                        case AuthType.Add:

                            if (db.AddAdmin(textBoxLogin.Text, textBoxPassword.Text, Father_) < 0)
                            { MessageBox.Show("Користувач з таким ім'ям вже існує, спробуйте ще.", "Увага"); return; }

                            MessageBox.Show(string.Format("Користувач {0} з паролем {1} доданий.",  textBoxLogin.Text,  textBoxPassword.Text), "");

                            break;

                        case AuthType.Update:

                            int idAdmin = db.GetIDAdmin(original.Login);

                            db.UpdateAdmin(idAdmin, textBoxLogin.Text, textBoxPassword.Text);

                            MessageBox.Show(string.Format("Логін {0} змінений на {1}\n Пароль {2} змінений на {3}", original.Login.PadRight(10), textBoxLogin.Text.PadRight(10), original.Password.PadRight(10), textBoxPassword.Text.PadRight(10)), "");

                            break;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Увага"); }

            this.Close();
        }
    }
}
