using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PictureOnControl
{
    public partial class Form1 : Form
    {
        enum MySide
        {
            Left,Right,Up,Down
        }
        MySide last;
        int X = 359;
        int Y = 221;
        string puth;
        string Fgcolor;
        Timer auto;
        public Form1()
        {
            InitializeComponent();
            auto = new Timer();
            auto.Interval = 100;
            auto.Tick += new EventHandler(auto_Tick);

            btUp.Click += new EventHandler(btClick);
            btDown.Click += new EventHandler(btClick);
            btLeft.Click += new EventHandler(btClick);
            btRight.Click += new EventHandler(btClick);
            puth = "rect\\";
            Fgcolor = "green.jpg";
            radioCircul.CheckedChanged += new EventHandler(radioCheckedChanged);
            radioRect.CheckedChanged += new EventHandler(radioCheckedChanged);



        }

        void auto_Tick(object sender, EventArgs e)
        {
            int t = (int)last;
            switch (t)
            {
                case 0:
                    X--;
                    break;
                case 1:
                    X++;
                    break;
                case 2:
                    Y--;
                    break;
                case 3:
                    Y++;
                    break;

            }
            if (checkWarning.CheckState == CheckState.Checked)
                Check();
            else
                Fgcolor = "green.jpg";
            if (checkTeleport.CheckState == CheckState.Checked)
                Telepo();
            else if (checkTeleport.CheckState == CheckState.Unchecked || checkTeleport.CheckState == CheckState.Indeterminate)
                Stena();
            panelObj.BackgroundImage = Image.FromFile(puth + Fgcolor);
            panelObj.Location = new Point(X, Y);
        }

        void radioCheckedChanged(object sender, EventArgs e)
        {
            if (sender == radioRect)
                puth = "rect\\";
            else
                puth = "circ\\";

            panelObj.BackgroundImage = Image.FromFile(puth + Fgcolor);


        }

        void btClick(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            switch (tmp.Name)
            {
                case "btUp":
                    {
                        Y -= 5;
                        last=MySide.Up;
                    }
                    break;
                case "btDown":
                    {
                        Y += 5;
                        last = MySide.Down;
                    }
                    break;
                case "btRight":
                    {
                        X += 5;
                        last = MySide.Right;
                    }
                    break;
                case "btLeft":
                    {
                        X -= 5;
                        last = MySide.Left;
                    }
                    break;


            }
            if (checkWarning.CheckState == CheckState.Checked)
                Check();
            else
                Fgcolor = "green.jpg";
            if (checkTeleport.CheckState == CheckState.Checked)
                Telepo();
            else if(checkTeleport.CheckState == CheckState.Unchecked || checkTeleport.CheckState == CheckState.Indeterminate)
                Stena();
            if (checkMove.CheckState == CheckState.Checked)
                auto.Start();
            else
                auto.Stop();

            panelObj.BackgroundImage = Image.FromFile(puth + Fgcolor);
            panelObj.Location = new Point(X, Y);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bTCenter_Click(object sender, EventArgs e)
        {
            X = 359;
            Y = 221;
            Fgcolor = "green.jpg";
            panelObj.BackgroundImage = Image.FromFile(puth + Fgcolor);
            panelObj.Location = new Point(X, Y);
        }


        void Check()
        {
            if ((X < 10) || (X > 728) || (Y < 10) || (Y > 437))
            {
                //panelObj.BackgroundImage = Image.FromFile(puth+"red.jpg");
                Fgcolor = "red.jpg";

            }
            else if ((X < 128) || (X > 610) || (Y < 128) || (Y > 319))
                Fgcolor = "yelow.jpg";                      //panelObj.BackgroundImage = Image.FromFile(puth + "yelow.jpg");
            else
                Fgcolor = "green.jpg";                         //panelObj.BackgroundImage = Image.FromFile(puth + "green.jpg");


        }

        void Telepo()
        {
            if (X < 0)
                X = 738;
            if (Y < 0)
                Y = 447;
            if (X > 738)
                X = 0;
            if (Y > 447)
                Y = 0;
        }
        void Stena()
        {
            if (X < 0)
                X = 0;
            if (Y < 0)
                Y = 0;
            if (X > 738)
                X = 737;
            if (Y > 447)
                Y = 446;
        }

      






    }
}
