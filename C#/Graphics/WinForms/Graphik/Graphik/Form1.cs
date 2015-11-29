using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graphik
{
    public partial class Form1 : Form
    {
        //UseGraph u;
        public Form1()
        {
            //u = new UseGraph();
            //u.Location = new Point(0, 0);
            //u.Size = new System.Drawing.Size(200, 200);
            //this.Controls.Add(u);
            InitializeComponent();
            this.useGraph1.ColorPensil = Color.Red;
            useGraph1.widthPen = 3;
            //useGraph1.ColorBackfon = Color.Red;
            useGraph1.ScaleGraph = 100;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            useGraph1.DrawGraph(int.Parse(textBox1.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void useGraph1_Load(object sender, EventArgs e)
        {

        }
        
    }
}
