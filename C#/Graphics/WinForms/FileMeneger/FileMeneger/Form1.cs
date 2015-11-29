using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace FileMeneger
{
    public partial class Form1 : Form
    {
        private ListViewItem p;
        // ImageList small, big;

        List<string> puthAll = new List<string>();


        public Form1()
        {
            InitializeComponent();
            listView1.View = (View)1;
            listView1.Columns.Add("name", 100);
            listView1.Columns.Add("data creation", 150);
            listView1.Columns.Add("puth", 100);

            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            //small = new ImageList();
            //small.ImageSize = new Size(16, 16);
            //small.ColorDepth = ColorDepth.Depth16Bit;
            //small.TransparentColor = Color.Magenta;
            //small.Images.Add(Image.FromFile("green16(16).bmp"));
            //small.Images.Add(Image.FromFile("red16(16).bmp"));

            //big = new ImageList();
            //big = new ImageList();
            //big.ImageSize = new Size(32, 32);
            //big.ColorDepth = ColorDepth.Depth16Bit;
            //big.Images.Add(Image.FromFile("green32(16).bmp"));
            //big.Images.Add(Image.FromFile("red32(16).bmp"));

            //listView1.SmallImageList = small;
            //listView1.LargeImageList = big;

            

            comboView.Items.AddRange(new string[] { "LargeIcon", "Details", "SmallIcon", "List", "Tile" });
            comboView.Text = "Details";


        }

        private void btFill_Click(object sender, EventArgs e)
        {
            puthAll.RemoveAt(puthAll.Count - 1);
            listView1.Items.Clear();

            DirectoryInfo direct = new DirectoryInfo(ToStringList(puthAll));
            AddList(direct);

        }



        private void comboView_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.View = (View)comboView.SelectedIndex;
        }

        private void comboDriers_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            listView1.Items.Clear();
            puthAll.Clear();
            string puth = ((DriveInfo)comboDriers.SelectedItem).Name;
            DirectoryInfo direct = new DirectoryInfo(puth);
            try
            {
                puthAll.Add(puth);

                AddList(direct);
            }
            catch (Exception)
            {

                MessageBox.Show("Поблема с выбором диска!! Это возможно дисковод");
                return;

            }
            

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem tmp = listView1.SelectedItems[0];
            puthAll.Add(tmp.Text + "\\");
            listView1.Items.Clear();
            DirectoryInfo direct = new DirectoryInfo(ToStringList(puthAll));
            AddList(direct);


        }

        private string ToStringList(List<string> all)
        {
            string rezult = "";
            foreach (string item in all)
            {
                rezult += item;
            }
            return rezult;
        }
        private void AddList(DirectoryInfo direct)
        {
            direct = new DirectoryInfo(ToStringList(puthAll));
            foreach (var item in direct.GetDirectories())
            {
                p = new ListViewItem(item.Name);
                p.SubItems.Add(Convert.ToString(item.CreationTime));
                p.SubItems.Add(item.FullName);
                listView1.Items.Add(p);

            }
        }

        private void comboDriers_Click(object sender, EventArgs e)
        {
            comboDriers.Items.Clear();
            foreach (var item in System.IO.DriveInfo.GetDrives())
            {
                comboDriers.Items.Add(item);
            }
        }
    }
}
