using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAPERmodern
{

    public partial class Form1 : Form
    {
        int numberBomba;
        Random rand = new Random();
        Timer dt;
        int locSdvig=0;
        DateTime fixTime;
        EnterMy en;
        int sizePol;
        int sizeBut = 50;
        CELL[][] pole;
        int numberBombFlags;
        Point[] vokryg = new Point[8] { new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1), new Point(1, 1), new Point(-1, 1), new Point(1, -1), new Point(-1, -1) };
        public Form1()
        {
            LoadGame();
           
        }

        private void LoadGame()
        {
            en = new EnterMy();
            en.ShowDialog();
            switch (en.Lavel)
            {
                case 0:
                    sizePol = 4;
                    numberBomba = numberBombFlags = 2;
                    locSdvig = 150;
                    break;
                case 1:
                    sizePol = 8;
                    numberBomba = numberBombFlags = 6;

                    break;
                case 2:
                    sizePol = 12;
                    numberBomba = numberBombFlags = 15;

                    break;
                case 3:
                    sizePol = 14;
                    numberBomba = numberBombFlags = 30;

                    break;
                default:
                    sizePol = 8;
                    numberBomba = 6;
                    break;

            }
            InitializeComponent();
            this.Visible = true;
            pole = new CELL[sizePol][];
            for (int i = 0; i < sizePol; i++)
            {

                pole[i] = new CELL[sizePol];

                for (int j = 0; j < sizePol; j++)
                {
                    pole[i][j] = new CELL();
                    pole[i][j].CellValue = 0;
                    pole[i][j].SizePanel = sizeBut;
                    pole[i][j].LocationPanel = new Point((i * sizeBut)+locSdvig, j * sizeBut + 70);
                    pole[i][j].BackImage = Image.FromFile(@"../imag/all.png");
                    pole[i][j].Panelpic.MouseDown += new MouseEventHandler(Panelpic_MouseDown);
                    this.Controls.Add(pole[i][j].Panelpic);
                }


            }
            this.Size = new System.Drawing.Size(((sizePol * sizeBut + 15) < 331) ? 480 : (sizePol * sizeBut + 15), sizePol * sizeBut + sizeBut * 2 + 10);
            dt = new Timer();
            fixTime = DateTime.Now;
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = 300;
            dt.Start();

            GetBomba(numberBomba);
        }
        void Panelpic_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {

                for (int i = 0; i < sizePol; i++)
                {
                    for (int j = 0; j < sizePol; j++)
                    {
                        if (sender == pole[i][j].Panelpic)
                        {
                            if (pole[i][j].FlagBomba == false)
                            {
                                pole[i][j].BackImage = Image.FromFile(@"../imag/flag.png");
                                pole[i][j].FlagBomba = true;
                              numberBombFlags--;
                              if (numberBombFlags < 0)
                                  numberBombFlags = 0;

                            }
                            else
                            {
                                pole[i][j].BackImage = Image.FromFile(@"../imag/all.png");
                                pole[i][j].FlagBomba = false;
                            }
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < sizePol; i++)
                {
                    for (int j = 0; j < sizePol; j++)
                    {
                        if (sender == pole[i][j].Panelpic)
                        {
                            if (pole[i][j].Bomba == false)
                            {
                                ShowValue(pole[i][j], i, j);

                            }
                            else
                            {
                                pole[i][j].BackImage = Image.FromFile(@"../imag/bomba.png");
                                ShowAllBomb();
                                dt.Stop();
                                MessageBox.Show("Game OVER");
                            }





                        }
                    }
                }
            }
            CheckWin();
        }
        void dt_Tick(object sender, EventArgs e)
        {
            TimeSpan rez = DateTime.Now - fixTime;



            if (rez.Minutes <= 9)
            {
                panelMin1.BackgroundImage = Image.FromFile(@"../imag/timer1/0.png");
                panelMin2.BackgroundImage = Image.FromFile(@"../imag/timer1/" + rez.Minutes + ".png");
            }
            else
            {
                panelMin1.BackgroundImage = Image.FromFile(@"../imag/timer1/" + rez.Minutes / 10 + ".png");
                panelMin2.BackgroundImage = Image.FromFile(@"../imag/timer1/" + rez.Minutes % 10 + ".png");
            }


            if (rez.Seconds <= 9)
            {
                panelSec1.BackgroundImage = Image.FromFile(@"../imag/timer1/0.png");
                panelSec2.BackgroundImage = Image.FromFile(@"../imag/timer1/" + rez.Seconds + ".png");
            }
            else
            {
                panelSec1.BackgroundImage = Image.FromFile(@"../imag/timer1/" + rez.Seconds / 10 + ".png");
                panelSec2.BackgroundImage = Image.FromFile(@"../imag/timer1/" + rez.Seconds % 10 + ".png");
            }

            if (numberBomba <= 9)
            {
                panelBomb1.BackgroundImage = Image.FromFile(@"../imag/timer1/0.png");
                panelBomb2.BackgroundImage = Image.FromFile(@"../imag/timer1/" +  numberBombFlags + ".png");
            }
            else
            {
                panelBomb1.BackgroundImage = Image.FromFile(@"../imag/timer1/" + numberBombFlags / 10 + ".png");
                panelBomb2.BackgroundImage = Image.FromFile(@"../imag/timer1/" +  numberBombFlags % 10 + ".png");
            }



        }
        private void GetBomba(int count)
        {
            int x;
            int y;
            for (int i = 0; i < count; i++)
            {
                x = rand.Next(0, sizePol - 1);
                y = rand.Next(0, sizePol - 1);
                pole[x][y].Bomba = true;
                //pole[x][y].BackImage = Image.FromFile(@"../imag/bomba.png");

            }
            GetCountBomba();
        }
        private void GetCountBomba()
        {
            int tmpX, tmpY;

            for (int i = 0; i < sizePol; i++)
            {
                for (int j = 0; j < sizePol; j++)
                {
                    if (pole[i][j].FlagBomba == false)
                    {
                        for (int e = 0; e < vokryg.Count(); e++)
                        {
                            tmpX = i + vokryg[e].X;
                            tmpY = j + vokryg[e].Y;
                            if ((tmpX >= 0 && tmpX < sizePol) && (tmpY >= 0 && tmpY < sizePol))
                            {
                                if (pole[tmpX][tmpY].Bomba == true)
                                    pole[i][j].CellValue++;
                            }
                            else
                                continue;
                        }
                    }
                    else
                        continue;
                }
            }
        }
        private void ShowAllBomb()
        {
            for (int i = 0; i < sizePol; i++)
            {
                for (int j = 0; j < sizePol; j++)
                {
                    if (pole[i][j].Bomba == true)
                        pole[i][j].BackImage = Image.FromFile(@"../imag/bomba.png");
                }
            }

        }
        private void ShowValue(CELL p,int x,int y)
        {
            if (p.CellValue == 0)
            {
                pole[x][y].BackImage = Image.FromFile(@"../imag/0.png");
                
                CheckNull(x, y);
            }
            else
            {
                pole[x][y].BackImage=Image.FromFile(@"../imag/greenNum/" + pole[x][y].CellValue + ".png");
               
            }
            pole[x][y].IsOpen = true;
        }
        private void CheckWin()
        {
            int countSumOnREzult = 0;
            for (int i = 0; i < sizePol; i++)
            {
                for (int j = 0; j < sizePol; j++)
                {
                    if (pole[i][j].IsOpen==false)
                        countSumOnREzult++;
                }
            }
            if (countSumOnREzult == numberBomba)
            {
                dt.Stop();
                ShowAllBomb();
                MessageBox.Show("Your WIN!!!!!");
                

            }



        }
        private void CheckNull(int x, int y)
        {
            int tmpX;
            int tmpY;

            for (int i = 0; i < vokryg.Count(); i++)
            {
                tmpX = x + vokryg[i].X;
                tmpY = y + vokryg[i].Y;

                if ((tmpX >= 0 && tmpX < sizePol) && (tmpY >= 0 && tmpY < sizePol))
                {
                    if (pole[tmpX][tmpY].IsOpen == true)
                        continue;
                    if (pole[tmpX][tmpY].FlagBomba == true)
                        continue;

                    if (pole[tmpX][tmpY].Bomba == false)
                    {
                       
                        if (pole[tmpX][tmpY].CellValue == 0)
                        {
                            pole[tmpX][tmpY].BackImage = Image.FromFile(@"../imag/0.png");
                            pole[tmpX][tmpY].IsOpen = true;
                            CheckNull(tmpX, tmpY);
                        }
                        else
                        {
                            pole[tmpX][tmpY].BackImage = Image.FromFile(@"../imag/greenNum/" + pole[tmpX][tmpY].CellValue + ".png");
                            pole[tmpX][tmpY].IsOpen = true;
                        }
                    }
                    else
                        continue;
                   
                   
                   
                    

                }
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Visible = false;
            Controls.Clear();
            LoadGame();
        }

     

        
    }
}
