using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameStrtegia
{
    class Action
    {
        Random rand = new Random();
public void TurnScurity( ref Resyrs tmp)
    {
        if (tmp.point > 100 && tmp.hobit>20)
        {
        tmp.point -= 100;
        tmp.security+=20;
        tmp.hobit-=20;
        }
        else
            Console.WriteLine(" \n\n\n\t\tУ тебя не достаточно людей"); 
    }
public void GetDamba(ref Resyrs tmp)
    {
        if (tmp.point > 1000)
        {
            tmp.point -= 1000;
            tmp.damba+=100;
        
    }
        else
            Console.WriteLine(" \n\n\n\t\t У тебя не достаточно денег"); 
    }
public void GetMedic(ref Resyrs tmp)
        {
            if (tmp.point > 100  && tmp.hobit>20)
                {
                    tmp.point -= 100;
                    tmp.medic += 20;
                    tmp.hobit -= 20;
                }
            else
                Console.WriteLine(" \n\n\n\t\t У тебя не достаточно людей"); 
        }
public void Napast(ref Resyrs tmp)
    {
        if(tmp.point>1000 && tmp.security>700)
    {
        tmp.point+=rand.Next(500,10000)-1000;
        tmp.security-=rand.Next(400);
        tmp.medic-=rand.Next(150);
    }
    else
        {
            Console.WriteLine(" \n\n\n\t\t У тебя не достаточно или денег или воинов"); 
        Console.ReadLine();}
    }
public void TurnHome(ref Resyrs tmp)
    {
        if (tmp.point > 100)
        {
        tmp.point -= 100;
        tmp.home+=1;
        tmp.hobit+=20;
    }
        else
            Console.WriteLine(" У тебя не достаточно денег"); 

    }

public string[] menu = { "улутчшить охрану --(100$) ", "построить дамбу--(1000$) ", "построить дома--(100$) ", "напасть на соседей--(1000$ и охрана>700) ", "улутчшить медицину--(100$) " };
public void Print(int cursor,Resyrs tmp)
{
    Console.Clear();
    Console.WriteLine("\t\t Приветствуем в игре \"ЧТО ПОПАЛО\"");
    tmp.Show();
    if ( cursor>=5)
     cursor =(cursor%5); 
    else
        if(cursor<0)
            cursor = (cursor % 5)+5;
    for(int i=0; i<5; i++)
    {
        if (cursor==i)
             Console.WriteLine("=> "+menu[i]);
        else
        Console.WriteLine("  "+menu[i]);
    }
}
}
}
