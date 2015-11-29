using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameStrtegia
{
    class Program
    {
          
        static void Main(string[] args)
        {
           
            
          ConsoleKeyInfo klava;
          Resyrs tmp = new Resyrs();
          Action move=new Action();
          Events doit = new Events();
          int cursor=0;
          move.Print(cursor, tmp);
          

          
         do 
        {
            klava = Console.ReadKey(true);
            if(klava.Key==ConsoleKey.UpArrow)
                {
                    cursor--;
                    move.Print(cursor,tmp);
                }
            if(klava.Key==ConsoleKey.DownArrow)
            {
                cursor++;
                move.Print(cursor, tmp);
            }
            if (klava.Key == ConsoleKey.Enter)
            {
                doit.Procent(ref tmp);
                tmp.day++;
                if (cursor >= 5)
                    cursor = (cursor % 5);
                else
                    if (cursor < 0)
                        cursor = (cursor % 5) + 5;
                switch (cursor)
                {
                    case 0:
                        move.TurnScurity(ref tmp);
                    break;
                    case 1:
                        move.GetDamba(ref tmp);
                    break;
                    case 2:
                        move.TurnHome(ref tmp);
                    break;
                    case 3:
                        move.Napast(ref tmp);
                    break;
                    case 4:
                        move.GetMedic(ref tmp);
                    break;
                }
            if(tmp.day>=12)
                {
                    tmp.day=0;
                    doit.Year(ref tmp);
                }
            move.Print(cursor, tmp);
            Console.WriteLine("\n\n Твой ход");
            }
        }while(klava.Key!=ConsoleKey.Escape);
        
        
        
        //Console.ReadLine();

//        if (klava.Key==ConsoleKey.UpArrow)
//{
//Console.WriteLine("upppppppp");
//}

       
        }
    }
}
