using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAPERmodern
{
    public partial class EnterMy : Form
    {
        private int lavel;

        public int Lavel
        {
            get { return lavel; }
            set { lavel = value; }
        }

        public EnterMy()
        {
            InitializeComponent();
            comboBox1.Items.Add("Новичек");
            comboBox1.Items.Add("Бывалый");
            comboBox1.Items.Add("Любитель");
            comboBox1.Items.Add("Профессионал");

        }

      

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lavel = comboBox1.SelectedIndex;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
