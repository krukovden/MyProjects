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
    public partial class Language : Form
    {
        public Language( Languauge l)
        {
            InitializeComponent();
            if (l.CurentLang == KindLanguage.Russian)
            {
                radBatRus.Checked = true;
            }
            else
                radBatEnglish.Checked = true;
            ChangeLanguage(l);
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if(radBatEnglish.Checked)
            this.DialogResult = DialogResult.Cancel;
            else
                this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void ChangeLanguage(Languauge l)
        {
            groupBox1.Text = l.GroupBoxLanguage;
            radBatEnglish.Text = l.Eng;
            radBatRus.Text = l.Rus;
            btOk.Text = l.Ok;
            this.Text = l.NameFormLang;
        }
    }
}
