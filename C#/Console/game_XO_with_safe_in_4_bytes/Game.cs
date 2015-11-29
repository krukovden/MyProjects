using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace X0
{
    class Game
    {
        int Params;

        public const int x = 0x1;

        public const int y = 0x2;

        int[,] mas = new int[3, 3];

        public bool IsMoveX { get { return countMove % 2 == 0; } set { } }

        int countMove = 0;

        public bool Xhod(int pos)
        {
            hod(x, pos, ref Params);
            return Check(88);
        }

        public bool Yhod(int pos)
        {
            hod(y, pos, ref  Params);
            return Check(79);
        }

        public void hod(int d, int pos, ref int c)
        {
            c = c | (d << (pos * 2));
            mas[(pos / 3), (pos % 3)] = IsMoveX ? 88 : 79;

        }

        public void Show()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\"s\"--выйти и сохранить");
            Console.WriteLine("\"q\"--выйти без сохранения");

            Console.WriteLine("\n\n\n");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("\t");

                    if (mas[i, j] > 10)
                    {
                        if (mas[i, j] == 79)
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                        Console.ForegroundColor = ConsoleColor.Cyan;


                    if (mas[i, j] > 10)
                        Console.Write(" " + (char)mas[i, j]);
                    else
                        Console.Write(" " + mas[i, j]);
                }
                Console.Write("\n");
            }
        }

        public void InitMas()
        {
            int zapol = 1;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    mas[i, j] = zapol++;
        }

        public int Exem(int gamerRezult)
        {

            int move = 0;
            int posit = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (((gamerRezult >> (posit * 2)) & 0x1) == x)
                    {
                        mas[i, j] = 88;
                        move++;
                    }
                    else if (((gamerRezult >> (posit * 2)) & 0x2) == y)
                    {
                        mas[i, j] = 79;
                        move++;
                    }
                    else
                        mas[i, j] = posit;
                    posit++;
                }

            }
            return move;
        }

        bool Check(int symbol)
        {
            //Проверяем горизонталь и вертикаль
            int mdig, supdig, hor, ver;

            for (int i = 0; i < 3; i++)
            {
                hor = 0; ver = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (mas[i, j] == symbol)
                        hor++;
                    if (mas[j, i] == symbol)
                        ver++;
                }
                if (hor == 3 || ver == 3)
                    return true;
            }

            mdig = 0; supdig = 0;
            //Диагонали
            for (int i = 0; i < 3; i++)
            {
                if (mas[i, i] == symbol)
                    mdig++;

                if (mas[i, 2 - i] == symbol)
                    supdig++;

            }
            if (mdig == 3 || supdig == 3)
                return true;

            return false;
        }

        public int Start()
        {
            if (Params == 0)
                InitMas();

            int rezultMove = 0;
            do
            {
                switch (rezultMove = Move(IsMoveX, ref countMove))
                {
                    case -1:
                        Console.WriteLine("Это поле занято");
                        break;

                    case 0:
                        Console.WriteLine("Такого поля не существует");
                        break;                    
                    case 10:
                        break;
                }

            } while (rezultMove < 90);

            return rezultMove;

        }

        public int Move(bool X, ref int count)
        {
            Show();
            Console.WriteLine(" \n Введите номер ячейки в которую хотите поставить ход");
            if (!X)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\n\t\t\tХод для  {0}-gamer: ", X ? "X" : "0");

            Console.ForegroundColor = ConsoleColor.Cyan;

            string tmp = Console.ReadLine();

            if (tmp == "s")
                return 666;
            else if (tmp == "q")
                return 900;

            int vub = 0;
            if (!Int32.TryParse(tmp, out vub))
                return 0;

            if (vub < 1 || vub > 10)
                return 0;
            vub--;
            if (mas[(vub / 3), (vub % 3)] < 10)
                mas[(vub / 3), (vub % 3)] = X ? 88 : 79;
            else
                return -1;

            if (X)
                if (Xhod(vub))
                    return 111;
                else
                    if (Yhod(vub))
                        return 222;

            if (++count == 9)
                return 333;
            return 10;
        }

        public void Safe()
        {
            using (var Mopen = File.CreateText("1.txt"))
                Mopen.Write(Params);
        }

        public void Load()
        {
            try
            {
                using (var Mopen = File.OpenText("1.txt"))
                {
                    Params = Convert.ToInt32(Mopen.ReadLine());

                    countMove = Exem(Params);

                    IsMoveX = countMove % 2 == 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Файл с сохраненными настройками не вышло загрузить : " + ex.Message);
            }
        }

    }
}
