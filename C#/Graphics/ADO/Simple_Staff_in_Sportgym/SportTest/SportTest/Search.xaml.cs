using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SportTest
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public delegate void DelEvent(int number);
    public partial class Search : Window
    {
        static public string action;
        public event DelEvent Number;
        workDataFunk w = new workDataFunk();
        public void SendNumber(int number)
        {
            if (Number != null)
                Number(number);
        }
        public Search()
        {
            InitializeComponent();
        }
        public Search(string kind)
        {
            InitializeComponent();
            action = kind;
            radioButton2.IsChecked = true;
            switch (kind)
            {
                case"exit":
                case "visit":
                    radioButton1.Visibility = Visibility.Collapsed;
                    radioButton2.Visibility = Visibility.Collapsed;
                    
                    this.Title = "Посещение";
                    break;
                case "search":
                    radioButton1.IsChecked = true;
                    radioButton1.Visibility = Visibility.Visible;
                    radioButton2.Visibility = Visibility.Visible;
                    this.Title = "Поиск";
                    break;
                case "delete":
                    this.Title = "Номер карточки удаления клиента";
                    radioButton1.Visibility = Visibility.Collapsed;
                    radioButton2.Visibility = Visibility.Collapsed;

                    break;
            }


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int card;

            if (action == "visit" || action == "exit")
            {
                try
                {
                    card = Convert.ToInt32(textBox1.Text);
                    SendNumber(card);
                    DialogResult = true;
                }
                catch
                {
                    MessageBox.Show("Только цыфры в номере карточки должны быть!!!");
                    DialogResult = false;
                }

            }
            else if(action=="search")
            {
                if (radioButton2.IsChecked == true)
                {
                    foreach (char item in textBox1.Text)
                    {
                        if (!Char.IsDigit(item))
                        {
                            MessageBox.Show("Только цыфры в номере карточки должны быть!!!");
                            textBox1.Text = "";
                            DialogResult = false;
                            return;
                        }
                    }
                    int num = Convert.ToInt32(textBox1.Text);
                    if (w.IsExistCard(num))
                    {
                        DetailInfo info = new DetailInfo(num);
                        info.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Таких здесь нет");
                    }

                }
                else
                {
                    
                    string num = textBox1.Text;
                    if (w.IsExistName(num))
                    {
                        DetailInfo info = new DetailInfo(num);
                        info.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Таких здесь нет");
                    }


                }

                
            }
            else if (action == "delete")
            {
                foreach (char item in textBox1.Text)
                {
                    if (!Char.IsDigit(item))
                    {
                        MessageBox.Show("Только цыфры в номере карточки должны быть!!!");
                        textBox1.Text = "";
                        DialogResult = false;
                        return;
                    }
                }
                int num = Convert.ToInt32(textBox1.Text);
                if (w.IsExistCard(num))
                {
                    if(w.delete(num))
                    {
                        MessageBox.Show("Человек удален");
                    }
                    else{
                        MessageBox.Show("Произошла ошибка");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Таких здесь нет");
                }

            }
            
        }
    }
}
