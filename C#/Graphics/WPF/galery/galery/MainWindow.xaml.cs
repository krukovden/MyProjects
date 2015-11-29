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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace galery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<int> dataMarks = new List<int>();
        int curentimag = 0;
        string curentLocation = "all";
        public MainWindow()
        {
            InitializeComponent();

            radioButton1.Click += new RoutedEventHandler(radioButton_Click);
            radioButton2.Click += new RoutedEventHandler(radioButton_Click);
            radioButton3.Click += new RoutedEventHandler(radioButton_Click);
            radioButton4.Click += new RoutedEventHandler(radioButton_Click);
            radioButton5.Click += new RoutedEventHandler(radioButton_Click);
            radioButton6.Click += new RoutedEventHandler(radioButton_Click);
            ReadInfo();
            ImagOnDisplay(0);
            InfForList();



        }

        void radioButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender == radioButton6)
                dataMarks[curentimag] = 0;
            else if (sender == radioButton1)
                dataMarks[curentimag] = 1;
            else if (sender == radioButton2)
                dataMarks[curentimag] = 2;
            else if (sender == radioButton3)
                dataMarks[curentimag] = 3;
            else if (sender == radioButton4)
                dataMarks[curentimag] = 4;
            else if (sender == radioButton5)
                dataMarks[curentimag] = 5;

            WriteInfo();
            //switch (sender.ToString())
            //{
            //    case "radioButton1":

            //        break;
            //    case "radioButton2":
            //        level[curentimag] = 2;
            //        break;
            //    case "radioButton3":
            //        level[curentimag] = 3;
            //        break;
            //    case "radioButton4":
            //        level[curentimag] = 4;
            //        break;
            //    case "radioButton5":
            //        level[curentimag] = 5;
            //        break;
            //}
        }

        private void Vpered_Click(object sender, RoutedEventArgs e)
        {

            curentimag++;
            AllAction();
        }


        void AllAction()
        {
            if (curentimag > dataMarks.Count - 1)
                curentimag = 0;
            else if (curentimag < 0)
                curentimag = dataMarks.Count - 1;
            slider.Value = curentimag;
            ShowLevel(dataMarks[curentimag]);
            ImagOnDisplay(curentimag);
            //Curentsize.Content = imag.RenderSize;
        }
        void ImagOnDisplay(int q)
        {
            imag.Width = 450;
            BitmapImage myBitMap = new BitmapImage();
            myBitMap.BeginInit();

            myBitMap.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\imag\" + curentLocation + @"\" + q + ".jpg");
            //myBitMap.DecodePixelWidth = 450;
            myBitMap.EndInit();
            imag.Source = myBitMap;
            Curentsize.Content = String.Format("{0}x{1}", myBitMap.Width, myBitMap.Height);
            // Curentsize.Content = imag.RenderSize;

        }
        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            curentimag--;

            AllAction();
        }
        void ShowLevel(int numberImag)
        {
            switch (numberImag)
            {
                case 1:
                    radioButton1.IsChecked = true;
                    break;
                case 2:
                    radioButton2.IsChecked = true;

                    break;
                case 3:
                    radioButton3.IsChecked = true;
                    break;
                case 4:
                    radioButton4.IsChecked = true;
                    break;
                case 5:
                    radioButton5.IsChecked = true;
                    break;
                default:
                    radioButton6.IsChecked = true;
                    break;
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            ImagOnDisplay((int)slider.Value);
        }

        void InfForList()
        {
            DirectoryInfo df = new DirectoryInfo(@"imag");
            foreach (var item in df.GetDirectories())
            {
                listBox1.Items.Add(item.Name);
            }


        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curentLocation = listBox1.Items[listBox1.SelectedIndex].ToString();
            dataMarks.Clear();
            ImagOnDisplay(0);
            curentimag = 0;
            slider.Value = 0;
            
            ReadInfo();
            ShowLevel(dataMarks[curentimag]);

        }

        void ReadInfo()
        {
            try
            {
                using (StreamReader sr = File.OpenText(@"imag\" + curentLocation + @"\inf.txt"))
                {
                    do
                    {
                        dataMarks.Add(Convert.ToInt32(sr.ReadLine()));
                    }
                    while (!sr.EndOfStream);
                    sr.Close();
                    slider.Maximum = dataMarks.Count - 1;
                }
            }
            catch (Exception ex)
            {
                DirectoryInfo dd = new DirectoryInfo(@"imag\" + curentLocation);
                FileInfo[] fil = dd.GetFiles("*.jpg");
                StreamWriter sw = File.CreateText(@"imag\" + curentLocation + @"\inf.txt");
                foreach (var item in fil)
                {
                    sw.WriteLine("0");
                }
                sw.Close();

            }


        }
        void WriteInfo()
        {
            StreamWriter sw = File.CreateText(@"imag\" + curentLocation + @"\inf.txt");
            foreach (var item in dataMarks)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }

    }
}
