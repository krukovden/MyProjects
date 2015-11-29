using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace X0
{  
    class Program
    {
        static void Main(string[] args)
        {

            Game TTT = new Game();

            string temp;

            Console.WriteLine("\n\n\n\t\t ДОБРО ПОЖАЛОВАТЬ В ИГРУ КРЕСТИКИ И НОЛИКИ");
            Console.WriteLine("\n\n\n\n Вы хотите продолжить последнюю сохраненную игру (yes/no) ");
            temp = Console.ReadLine();
            
            if (temp == "yes")
                TTT.Load();

            Console.Clear();
            string gameover = "\n\n\n\n\t\t\tКОНЕЦ ИГРЫ";
            switch (TTT.Start())
            {
                case 111:
                    Console.Clear();
                    Console.WriteLine(gameover + " X-игрок Выиграл");
                    break;
                case 222:
                    Console.Clear();
                    Console.WriteLine(gameover + " O-игрок Выиграл");
                    break;
                case 333:
                    Console.Clear();
                    Console.WriteLine(gameover+"\n\t\t\tПоздравляем у вас НИЧЬЯ!!!!");
                    break;
                case 666:
                    TTT.Safe();
                    return;
                case 900:
                    return;
            }
            Console.ReadLine();
        }

    }
}
