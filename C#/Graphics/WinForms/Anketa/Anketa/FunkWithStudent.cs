using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Anketa
{
    public partial class FunkWithStudent : Form
    {
        MainInform inform;
        public FunkWithStudent(Languauge l)
        {
            InitializeComponent();
            inform = new MainInform();

            comboPol.Items.Add(l.Man);
            comboPol.Items.Add(l.Woman);
            this.Text = l.NameFormNew;
            ChangeLang(l);
        }
        public FunkWithStudent(Student s,Languauge l)
        {

            InitializeComponent();
            txtName.Text = s.Name;
            txtSur.Text = s.Surname;
            txtSurname.Text = s.Lastname;
            numericAge.Value = s.Age;
            comboPol.Items.Add(l.Man);
            comboPol.Items.Add(l.Woman);
            comboPol.SelectedItem = s.Pol;
            comboGroup.Text = s.StudentGroup.GroupStudent;
            this.Text = l.NameFormEdit;
            ChangeLang(l);
            

        }
        
       
        public Student GetDAta()
        {
            return new Student(txtName.Text, txtSurname.Text, txtSur.Text, Convert.ToInt32(numericAge.Value), comboPol.Text,comboGroup.Text);
            
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length < 3)
            {
                MessageBox.Show("Неправильное имя");
                return;
            }
            else if (txtSurname.Text.Length < 3)
            {
                MessageBox.Show("Неправильное Отчество");
                return;
            }
            else if (txtSur.Text.Length < 3)
            {
                MessageBox.Show("Неправильное Фамилия");
                return;
            }
            else if (numericAge.Value < 3)
            {
                MessageBox.Show("Неправильный возраст");
                return;
            }
            else if (comboPol.SelectedItem == null)
            {
                MessageBox.Show("Неправильный пол");
                return;
            }
            else if (comboGroup.Text.Length<1)
            {
                MessageBox.Show(" Group");
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            




        }

        void ChangeLang(Languauge b)
        {
            btCancel.Text = b.Cancel;
            btOk.Text = b.Ok;
            labName.Text = b.Name;
            label2.Text = b.Lastname;
            label3.Text = b.Surname;
            label4.Text = b.Age;
            label5.Text = b.Pol;
            label6.Text = b.Group;
            

        }
       
    }
}
