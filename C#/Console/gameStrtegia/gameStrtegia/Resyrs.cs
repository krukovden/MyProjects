using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameStrtegia
{
    class Resyrs
    {
        public int point=10000;
        public int hobit = 1000;
        public int home = 500;
        public int security = 500;
        public int medic = 1000;
        public int damba = 1000;
        public int day=0;

public void Show()
{
    Console.WriteLine("________________________________________________________________________________");
Console.WriteLine("\n\t\t\t| деньги :"+point+
                  "\n\t\t\t| жители: " + hobit +
                  "\n\t\t\t| дома: " + home +
                  "\n\t\t\t| охрана: " + security +
                  "\n\t\t\t| медицина: " + medic);
Console.WriteLine("________________________________________________________________________________\n\n\n");
}
    }
}
