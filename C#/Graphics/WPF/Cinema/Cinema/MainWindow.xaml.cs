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




namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int sizeroom,sizerow;
        int priceLow, priceHight, priceMiddle; 
        Sofa[][] room ;
        List<buffer> buf = new List<buffer>();
        System.Windows.Threading.DispatcherTimer mytimer = new System.Windows.Threading.DispatcherTimer();
        int HOUR, MIN;
       
        public MainWindow()
        {
            InitializeComponent();
            string namefilm = "Tor";
            int priceHight = 100;
            int priceMiddle = 85;
            int priceLow = 65;
          
            AllLoad(namefilm, priceHight, priceMiddle, priceLow,4,4); 
        


        }
        public MainWindow(string namefilm, int priceHight, int priceMiddle, int priceLow, int hour, int minute)
        {
            InitializeComponent();
            AllLoad(namefilm, priceHight, priceMiddle, priceLow, hour, minute);

            

        }

        string namef;
        void AllLoad(string name, int h, int m, int low, int hours, int minutes)
        {
            namef = name;
            priceHight = h;
            priceMiddle = m;
            priceLow = low;
            HOUR = hours;
            MIN = minutes;
            sizeroom = 10;
            sizerow = 7;

            room = new Sofa[sizerow][];
            label1.Content = priceLow + " грн";
            label2.Content = priceMiddle + " грн";
            label3.Content = priceHight + " грн";

            mytimer.Tick += new EventHandler(mytimer_Tick);
            mytimer.Interval = new TimeSpan(0, 0,1);
            mytimer.Start();



            for (int i = 0; i < sizerow; i++)
            {
                room[i] = new Sofa[sizeroom];

                for (int j = 0; j < sizeroom; j++)
                {

                    if (j == 0)
                    {
                        Label l = new Label();
                        l.Width = 20;
                        l.Height = 25;
                        l.BorderThickness = new Thickness(1);
                        l.Content = sizerow - i;
                        kino.Children.Add(l);
                        continue;
                    }

                    room[i][j] = new Sofa();
                    GetStyle(i, j);

                    room[i][j].butt.Click += new RoutedEventHandler(butt_Click);
                    ToolTip tt = new System.Windows.Controls.ToolTip();
                    tt.Content = (sizerow - i) + " ряд  " + (j) + " место  " + " цена " + room[i][j].Cost + " грн";
                    room[i][j].butt.ToolTip = tt;
                    kino.Children.Add(room[i][j].butt);

                }
               
               
               
            }

            string tmp = "";

            using (StreamReader file = File.OpenText(@"../films/" + namef + ".txt"))
            {
                while (file.Peek() >= 0)
                {
                    tmp += file.ReadLine();
                }
            }
            Info.Text = tmp;

            


            
            ImageSourceConverter imgs = new ImageSourceConverter();
            FAce.SetValue(Image.SourceProperty, imgs.ConvertFromString(@"../films/" + namef + ".jpg"));
            screen.SetValue(Image.SourceProperty, imgs.ConvertFromString(@"../timer/im.png"));
            
        }
       
        
        void mytimer_Tick(object sender, EventArgs e)
        {
            ImageSourceConverter imgs = new ImageSourceConverter();
            DateTime dt = DateTime.Now;
            int rezH = ((HOUR - dt.Hour) < 0) ? ((HOUR - dt.Hour) + 24) : (HOUR - dt.Hour);
            int rezM = ((MIN - dt.Minute) < 0) ? ((MIN - dt.Minute) + 60) : (MIN - dt.Minute); 
                
              
            
            
            

            if (rezH <= 9)
            {
                min1.SetValue(Image.SourceProperty, imgs.ConvertFromString(@"../timer/0.png"));
                min2.SetValue(Image.SourceProperty, imgs.ConvertFromString(@"../timer/" + rezH + ".png"));
            }
            else
            {
                min1.SetValue(Image.SourceProperty, imgs.ConvertFromString(@"../timer/" + rezH / 10 + ".png"));
                min2.SetValue(Image.SourceProperty, imgs.ConvertFromString(@"../timer/" + rezH % 10 + ".png"));
            }

            if (rezM <= 9)
            {
                sec1.SetValue(Image.SourceProperty, imgs.ConvertFromString(@"../timer/0.png"));
                sec2.SetValue(Image.SourceProperty, imgs.ConvertFromString(@"../timer/" + rezM + ".png"));
            }
            else
            {
                sec1.SetValue(Image.SourceProperty, imgs.ConvertFromString(@"../timer/" + rezM / 10 + ".png"));
                sec2.SetValue(Image.SourceProperty, imgs.ConvertFromString(@"../timer/" + rezM % 10 + ".png"));
            }

           
        }
     
        void GetStyle(int i, int j)
        {
            if (i == 0)
            {
                room[i][j].butt.Style = (System.Windows.Style)FindResource("hight_price");
                room[i][j].Cost = priceHight;  
            }
            else if ((i >= 1 && i <= 4) && (j != 1 && j != 9))
            {
                room[i][j].butt.Style = (System.Windows.Style)FindResource("midle_price");
                room[i][j].Cost = priceMiddle;
            }
            else
            {
                room[i][j].butt.Style = (System.Windows.Style)FindResource("low_price");
                room[i][j].Cost = priceLow;
            }
        }
        
        void butt_Click(object sender, RoutedEventArgs e)
        {
          for(int i=0; i<sizerow; i++)
              for (int j = 1; j < sizeroom; j++)
              {
                  if (sender == room[i][j].butt)
                  {

                      if (!room[i][j].ISChecked)
                      {
                          room[i][j].butt.Style = (System.Windows.Style)FindResource("tmp");
                          room[i][j].ISChecked = true;
                          buf.Add(new buffer(i, j));
                      }
                      else
                      {
                          GetStyle(i, j);
                          room[i][j].ISChecked = false;
                          for (int eo = 0; eo < buf.Count; eo++)
                          {
                              if (buf[eo].W == j && buf[eo].H == i)
                                  buf.RemoveAt(eo);
                          }
                      }
                      
                  }
              }
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            if (buf.Count <= 0)
                return;
            if (buf.Count <= 0)
                return;
            AutorizationBuy aut = new AutorizationBuy();
            aut.ShowDialog();
            if (aut.DialRez)
            {
               
                for (int i = 0; i < buf.Count; i++)
                {
                    room[buf[i].W][buf[i].H].butt.IsEnabled = false;
                    room[buf[i].W][buf[i].H].ISBuy = true;
                    room[buf[i].W][buf[i].H].Owner = aut.NameData;
                    room[buf[i].W][buf[i].H].Mobile = aut.PhoneData;
                    room[buf[i].W][buf[i].H].Card = aut.Card;

                     if (MessageBoxResult.Yes == MessageBox.Show("Хотите напечатать билет "+(buf[i].W+1)+" ряд "+buf[i].H+" место?", "Внимание", MessageBoxButton.YesNo))
                        {
                            Print p = new Print(namef, ""+HOUR+":"+MIN, buf[i].W+1, buf[i].H, room[buf[i].W][buf[i].H].Cost);
                            p.ShowDialog();
                        }
                }
                buf.Clear();
            }
        }

        private void reserve_Click(object sender, RoutedEventArgs e)
        {
           

            if (buf.Count <= 0)
                return;
            Autorization aut = new Autorization();
            aut.ShowDialog();
            if (aut.DialRez)
            {
                for (int i = 0; i < buf.Count; i++)
                {
                    room[buf[i].W][buf[i].H].butt.IsEnabled = false;
                    room[buf[i].W][buf[i].H].ISReserve = true;
                    room[buf[i].W][buf[i].H].Owner = aut.NameData;
                    room[buf[i].W][buf[i].H].Mobile = aut.PhoneData;
                }
                buf.Clear();
            }   
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int countReserve = 0;
            int countBuy = 0;
            int Sum=0;
            int Sumall = 0;
            for (int i = 0; i < sizerow; i++)
            {
                for (int j = 1; j < sizeroom; j++)
                {
                    if (room[i][j].ISReserve)
                    {
                        countReserve++;
                        Sumall += room[i][j].Cost;
                    }
                    if (room[i][j].ISBuy)
                    {
                        countBuy++;
                        Sum += room[i][j].Cost;
                        Sumall += room[i][j].Cost;
                    }
                }
            }
            Statistic st = new Statistic(countReserve, countBuy, Sum, Sumall);
            st.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Chair okno = new Chair();
            okno.ShowDialog();
            if (okno.end)
            {
                InfoChair inf = new InfoChair(namef, HOUR + ":" + MIN, okno.Row + 1, okno.Place, room[okno.Row + 1][okno.Place].Cost, room[okno.Row + 1][okno.Place].Owner, room[okno.Row + 1][okno.Place].Mobile);
                inf.ShowDialog();
            }
        }
        
    }
}
