using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Anketa
{

    public partial class MainInform : Form
    {
        FunkWithStudent forma;
        List<Student> all;
        Student tmp;
        Languauge lang;
        int curentIndex = 0;
        


        public MainInform()
        {
            InitializeComponent();
            lang = new Languauge(KindLanguage.Russian);

            all = new List<Student>();
            tmp = new Student();
           
            //lngMenu=new Student();

            //labName.DataBindings.Add("Text", lngMenu, "Name");
            //label2.DataBindings.Add("Text", lngMenu, "Surname");
            //label3.DataBindings.Add("Text", lngMenu, "Lastname");
            //label4.DataBindings.Add("Text", lngMenu, "Age"); ///////////
            //label5.DataBindings.Add("Text", lngMenu, "Pol");
            //label6.DataBindings.Add("Text", lngMenu, "StudentGroup.GroupStudent");

            btNew.Click += new EventHandler(buttonClick);
            btEdit.Click += new EventHandler(buttonClick);
            btAllRight.Click += new EventHandler(btAll_Click);
            btAllLeft.Click += new EventHandler(btAll_Click);
            txtName.DataBindings.Add("Text", tmp, "Name");
            txtSurname.DataBindings.Add("Text", tmp, "Surname");
            txtSur.DataBindings.Add("Text", tmp, "Lastname");
            txtAge.DataBindings.Add("Text", tmp, "Age"); ///////////
            txtPol.DataBindings.Add("Text", tmp, "Pol");
            txtGroup.DataBindings.Add("Text", tmp, "StudentGroup.GroupStudent");
            btDelete.Enabled = false;
            btEdit.Enabled = false;
            btAllLeft.Enabled = false;
            btOneLeft.Enabled = false;
            btOneRight.Enabled = false;
            btAllRight.Enabled = false;
            btSave.Enabled = false;
            openToolStripMenuItem.Click += new EventHandler(btOpen_Click);
            saveToolStripMenuItem.Click += new EventHandler(btSave_Click);
            newToolStripMenuItem.Click += new EventHandler(buttonClick);
            changeToolStripMenuItem.Click += new EventHandler(buttonClick);
            deleteToolStripMenuItem.Click += new EventHandler(btDelete_Click);
            menuStrip1.BackColor = Color.Cyan;
            ChangeLangForm1();


            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.ShowShortcutKeys = true;

        }

        private void buttonClick(object sender, EventArgs e)
        {

            if (sender == btEdit)
            {
                forma = new FunkWithStudent(all[curentIndex], lang);
            }
            else
                forma = new FunkWithStudent(lang);
            forma.ShowDialog();
            if (forma.DialogResult == DialogResult.OK)
            {
                if (sender == btNew)
                {
                    all.Add(forma.GetDAta());
                    Enable();
                }
                else if (sender == btEdit)
                    all[curentIndex] = forma.GetDAta();
                Copy(all[curentIndex],tmp);
            }



        }

        private void btOneRight_Click(object sender, EventArgs e)
        {

            curentIndex++;
            if (curentIndex >= all.Count)
                curentIndex = 0;
            Copy(all[curentIndex],tmp);


        }

        private void btOneLeft_Click(object sender, EventArgs e)
        {

            curentIndex--;
            if (curentIndex < 0)
                curentIndex = all.Count - 1;
            Copy(all[curentIndex], tmp);
        }

        public void Copy(Student elem, Student kyda)
        {
            kyda.Name = elem.Name;
            kyda.Surname = elem.Surname;
            kyda.Lastname = elem.Lastname;
            kyda.Age = elem.Age;
            kyda.Pol = elem.Pol;
            kyda.StudentGroup = elem.StudentGroup;


        }


        private void btDelete_Click(object sender, EventArgs e)
        {
            all.RemoveAt(curentIndex);
            curentIndex--;
            if (curentIndex < 0)
                curentIndex = all.Count - 1;
            if (all.Count == 0)
            {
                Copy(new Student(),tmp);
                curentIndex = 0;
            }
            else
            {

                Copy(all[curentIndex],tmp);

            }
            Enable();

        }

        private void btAll_Click(object sender, EventArgs e)
        {
            if (sender == btAllLeft)
                curentIndex = 0;
            else if (sender == btAllRight)
                curentIndex = all.Count - 1;
            Copy(all[curentIndex],tmp);

        }


        public void Enable()
        {
            if (all.Count > 1)
            {
                btAllLeft.Enabled = true;
                btOneLeft.Enabled = true;
                btOneRight.Enabled = true;
                btAllRight.Enabled = true;

            }
            else
            {
                btAllLeft.Enabled = false;
                btOneLeft.Enabled = false;
                btOneRight.Enabled = false;
                btAllRight.Enabled = false;


            }
            if (all.Count == 0)
            {
                btDelete.Enabled = false;
                btEdit.Enabled = false;
                btSave.Enabled = false;
            }
            else
            {
                btSave.Enabled = true;
                btDelete.Enabled = true;
                btEdit.Enabled = true;
            }
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            StreamReader read;
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                read = File.OpenText(op.FileName);
                Student prom = new Student();
                all.Clear();
                do
                {
                    prom.Name = read.ReadLine();
                    prom.Lastname = read.ReadLine();
                    prom.Surname = read.ReadLine();
                    prom.Age = Convert.ToInt32(read.ReadLine());
                    prom.Pol = read.ReadLine();
                    prom.StudentGroup.GroupStudent = read.ReadLine();
                    read.ReadLine();
                    all.Add(prom);

                } while (read.EndOfStream);
                read.Close();
                Copy(all[curentIndex],tmp);
                Enable();
            }




        }

        private void btSave_Click(object sender, EventArgs e)
        {
            StreamWriter write;
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            s.RestoreDirectory = true;

            if (s.ShowDialog() == DialogResult.OK)
            {
                write = File.CreateText(s.FileName);
                foreach (Student item in all)
                {
                    write.WriteLine(item.Name);
                    write.WriteLine(item.Lastname);
                    write.WriteLine(item.Surname);
                    write.WriteLine(item.Age);
                    write.WriteLine(item.Pol);
                    write.WriteLine(item.StudentGroup.GroupStudent);
                    write.WriteLine(" ");

                }


                write.Close();



            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Imagemm my = new Imagemm();
            my.ShowDialog();
        }

        
        void ChangeLangForm1()
        {
            this.Text = lang.NameForm;
            labName.Text = lang.Name;
            label2.Text = lang.Lastname;
            label3.Text = lang.Surname;
            label4.Text = lang.Age;
            label5.Text = lang.Pol;
            label6.Text = lang.Group;

            btNew.Text = lang.NewStud;
             btDelete.Text = lang.DeleteStud;
             btEdit.Text = lang.ChangeStud;
           
            menuToolStripMenuItem.Text = lang.Menu;
            openToolStripMenuItem.Text = lang.OpenStud;
            saveToolStripMenuItem.Text = lang.SaveStud;
            
            pravkaToolStripMenuItem.Text = lang.Edit;
            newToolStripMenuItem.Text = lang.NewStud;
            changeToolStripMenuItem.Text = lang.ChangeStud;
            deleteToolStripMenuItem.Text = lang.DeleteStud;
            
            langugeToolStripMenuItem.Text = lang.Lang;

            btSave.Text = lang.SaveStud;
            btOpen.Text = lang.OpenStud;
            
            groupBox2.Text = lang.GroupBox;


        }
        private void langugeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language formLanguage = new Language(lang);
            formLanguage.ShowDialog();
            if (formLanguage.DialogResult == DialogResult.OK)
                lang =new Languauge( KindLanguage.Russian);
            else if (formLanguage.DialogResult == DialogResult.Cancel)
                lang = new Languauge(KindLanguage.English);
             ChangeLangForm1();
        }

    }
}
