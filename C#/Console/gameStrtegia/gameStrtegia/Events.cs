using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameStrtegia
{
    class Events
    {
        Random rand = new Random();
        public void Navod(ref Resyrs tmp)
        {
            if(tmp.hobit-tmp.damba>0)
        {
            int dom = rand.Next(tmp.home / rand.Next(tmp.home));
            tmp.home -= dom;
            tmp.hobit -= dom * 20;
            Console.WriteLine("\n\n\n\t\t Осторожно!!!! Произошло наводнение!!!! Примите какие-то меры!!!");
            Console.ReadLine();
        }
        else
                Console.WriteLine("\n\n\n\t\t  Произошло наводнение!!!! Но дамба спасла");
            Console.ReadLine();
        }
        public void Ataka(ref Resyrs tmp)
        {
            int vrag = rand.Next(600);

            if ((tmp.security -= vrag) < 0)
            {
                tmp.hobit += tmp.security;
                tmp.home += (tmp.security / 20);
                tmp.security = 0;
                Console.WriteLine("\n\n\n\t\t Осторожно!!!! Произошла атака монстров!!!! Примите какие-то меры!!!");
                Console.ReadLine();
            }
            else
                Console.WriteLine("\n\n\n\t\t  Была отбита атака монстров!!!! ");
            Console.ReadLine();

        }
        public void Bolezn(ref Resyrs tmp)
        {
            int die = rand.Next(tmp.hobit - (tmp.medic * 5));
            tmp.hobit -= die;
            Console.WriteLine("\n\n\n\t\t Осторожно!!!! В городе вспалахнула епидемия гриппа и умирают люди!!!! Примите какие-то меры!!!");
            Console.ReadLine();
        }
        public void Golod(ref Resyrs tmp)
        {
            tmp.point -= 25;
            if (tmp.point < 100)
            {
                Bolezn(ref tmp);
                Console.WriteLine("\n\n\n\t\t Осторожно!!!! В городе голодомор!!!! Примите какие-то меры!!!");
                Console.ReadLine();
            }
        }
        public void Debosh(ref Resyrs tmp)
        {
            if (tmp.hobit / 20 > tmp.security)
            {
                tmp.hobit -= rand.Next(tmp.hobit - 20 * tmp.security);
                Console.WriteLine("\n\n\n\t\t Осторожно!!!! В городе массовые безпорядки!!!! Примите какие-то меры!!!");
                Console.ReadLine();
            }
            else
                Console.WriteLine("\n\n\n\t\t  Было остановлено дебоши");
        }
        public void Year(ref Resyrs tmp)
        {
            int p = -1;
            Math.Pow(p, rand.Next(10));
            tmp.hobit -= (p * rand.Next(Convert.ToInt32(tmp.hobit * 0.1)));
            tmp.point+=tmp.hobit*2;
            Console.WriteLine(" В связи с окончением года произошли демографические изменения в жителях");
            Console.ReadLine();
        }
        public void Procent(ref Resyrs tmp)
{
    switch(rand.Next(0,5))
        {
            case 0:
               {
                 if(rand.Next(10)<=5)
                    Navod(ref tmp);
                }
            break;
            case 1:
            {
                if (rand.Next(50) <= 10)
                    Ataka(ref tmp);
            }
            break;
            case 2:
            {
                if (rand.Next(50) <= 10)
                    Bolezn(ref tmp);
            }
            break;
            case 3:
            {
                if (rand.Next(50) <= 10)
                    Golod(ref tmp);
            }
            break;
            case 4:
            {
                if (rand.Next(100) <= 10)
                    Debosh(ref tmp);
            }
            break;
            
        }
}
    }
}
