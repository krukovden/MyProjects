using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Doctor_L_O_B_A_N_O_V
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            view.CheckOnClick = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Zapolnenie mas = new Zapolnenie();
            view.Items.Add("опух нос");
            view.Items.Add("насморк");
            view.Items.Add("утрудненное дыхание");
            view.Items.Add("зуд в области носа");
            view.Items.Add("кровоточит нос");
            view.Items.Add("потеря обояния носа");
            view.Items.Add("боль в области носа");
            view.Items.Add("выдиление с уха");
            view.Items.Add("ухо стало плохо слышать");
            view.Items.Add("зуд в область уха");
            view.Items.Add("кровоточит ухо");
            view.Items.Add("боль в области уха");
            view.Items.Add("слезится глаз");
            view.Items.Add("болят глаза");
            view.Items.Add("зуд в глазах");
            view.Items.Add("покраснение глаз");
            view.Items.Add("тик глаз");
            view.Items.Add("просто хрень с организмом");
            view.Items.Add("отек глаз");
            view.Items.Add("воспаление глаз");
            view.Items.Add("гнойное выделение с глаз");
            view.Items.Add("болит рука или нога");
            view.Items.Add("опухла рука или нога");
            view.Items.Add("кровоизлияние руки или ноги");
            view.Items.Add("отек рука или нога");
            view.Items.Add("боль в суставе");
            view.Items.Add("покраснение рука или нога");
            view.Items.Add("тугоподвижность рука или нога");
            view.Items.Add("темперетура");
            view.Items.Add("кашель");
            view.Items.Add("боль в груди");
            view.Items.Add("хрип в легких");
            view.Items.Add("слабость ");
            view.Items.Add("потливость");
            view.Items.Add("боль в мышцах");
            view.Items.Add("отдышка");
            view.Items.Add("втяжение межреберных промежутков");
            view.Items.Add("тахикардия");
            view.Items.Add("головная боль");

        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            listBox1.Items.Clear();
            Zapolnenie med=new Zapolnenie();
            int count;
            foreach (var item in med.medicina)
            {
                count = 0;
                foreach (var elem_num in item.Value)
                {
                    foreach (var item2 in view.CheckedIndices)
                    {
                     
                        if ((int)elem_num == ((int)item2))
                            count++;
                     }

                }
                int val = count * 100 / item.Value.Count;
                
                    if (val > 70)
                        listBox1.Items.Add("У вас большая вероятность " + item.Key);
                    else if (val > 50)
                        listBox1.Items.Add("У вас возможно " + item.Key);
                    else if (val > 30)
                        listBox1.Items.Add("У вас малая вероятность " + item.Key);
                    
                
                //listBox1.Items.Add((BAD.Nose)item);
                
               
            }



        }
    }
}
