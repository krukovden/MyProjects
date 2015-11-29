using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace Graphik
{
    public partial class UseGraph : UserControl
    {
        int sizemas;
        Point[] mas ;//= new Point[sizemas];
        int i = 1;
        Pen pen;
        Brush back;
        Color pensil;
        Color backfon;
        int widthPe;
        int step;






        public UseGraph()
        {

            InitializeComponent();
            widthPe = 3;
            pensil = Color.Blue;
            backfon = Color.DarkCyan;
            sizemas=50;
          
           

            Timer Mtimer = new Timer();
            Mtimer.Interval = 300;
            Mtimer.Start();
            Mtimer.Tick += delegate
            {
                Invalidate();
            };
        }
        public Color ColorPensil
        {
            get
            {
                return pensil;
            }
            set
            {
                pensil = value;
            }
        }
        public Color ColorBackfon
        {
            set
            {
                backfon = value;
            }
            get
            {
                return backfon;
            }
           
        }
        public int widthPen
        {
            set
            {
                widthPe = value;
            }
            get
            {
                return widthPe;
            }

        }
        public int ScaleGraph
        {
            set
            {
                sizemas = value;
                
            }
            get
            {
                return sizemas;
            }

        }

        private void UseGraph_Load(object sender, EventArgs e)
        {
            mas = new Point[sizemas];
            mas[0] = new Point(0, this.Height);
        }
        int pausa;

        protected override void OnPaint(PaintEventArgs e)
        {
            back = new HatchBrush(HatchStyle.Cross, backfon, Color.Cyan);
            pen = new Pen(pensil, widthPe);
            if (true == true)
                pausa = 0;
            base.OnPaint(e);
            Graphics dc = e.Graphics;
            dc.FillRectangle(back, 0, 0, this.Width, this.Height);
            for (int a = 1; a < i; a++)
            {
                dc.DrawLine(pen, mas[a - 1], mas[a]);
            }

        }

        public void DrawGraph(int proc)
        {
            
          //if(0<=proc && proc>100)
          //    throw new Exception("Wrong input value in Graph");
            step = this.Width / sizemas;
            int mm = this.Height * proc / 100;
            int tt = (this.Height - mm);

            if (i >= sizemas)
            {
                Modern();
                mas[i - 1] = new Point(step * i, tt);

            }
            else
            {

                mas[i] = new Point(step * i, tt);
                i++;
            }

        }

        private void Modern()
        {
            for (int i = 0; i < sizemas - 1; i++)
            {
                mas[i] = mas[i + 1];
                mas[i].X -= step;
            }

            //mas[sizemas-1] = mas[i + 1];
        }


    }
}
