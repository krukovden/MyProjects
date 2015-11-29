using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Doctor_L_O_B_A_N_O_V
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
