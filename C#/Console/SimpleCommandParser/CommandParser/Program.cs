using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CommandParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Dictionary<char, string[]> Commands = new Dictionary<char, string[]>();

            Commands.Add('/', new string[] { "help", "?" });
            Commands.Add('-', new string[] { "print [value]", "k [key value]", "ping", "music [-s(space) -g(good)] ", "exit" });

            string exit = "";
            if (args.Length == 0)
                args = new string[] { "/?" };
            do
            {
                if (!(args.Length == 1) || !args[0].Equals(""))
                {
                    bool once = true;
                    for (int i = 0; i < args.Length; i++)
                        if (args[i].StartsWith(@"/") || args[i].StartsWith(@"-"))
                            switch (Regex.Replace(args[i].ToLower(), @"[^\w\?]", ""))
                            {
                                case "?":
                                case "help":
                                    if (once)
                                        foreach (var key in Commands)
                                            foreach (var item in key.Value)
                                                Console.WriteLine("\t" + key.Key + item);
                                    once = false;
                                    break;
                                case "print":
                                    if (args.Length > i + 1)
                                        Console.WriteLine("> " + new string('-', 15) + "> <" + args[++i] + ">");
                                    else
                                        Console.WriteLine("> " + new string('-', 15) + "> <without message>");
                                    break;

                                case "ping":
                                    Console.Write(">Ping");
                                    do
                                    {
                                        Console.Write('g');
                                        Console.Beep(800, 500);

                                    } while (!Console.KeyAvailable);
                                    Console.ReadLine();
                                    break;
                                case "music":
                                    if (args.Length > i + 1)
                                    {
                                        if (args[i + 1].StartsWith("-s"))
                                        { SpaceMusic(); i++; }
                                        else if (args[i + 1].StartsWith("-g"))
                                        { GoodMusic(); i++; }
                                    }
                                    else
                                        Console.WriteLine(">Error param with command <-music> turn <-g> or <-s>");

                                    break;
                                case "k":
                                    if (args.Length > i + 1)
                                        while (i < args.Length - 1 && Regex.IsMatch(args[i + 1], @"^[a-z0-9]"))
                                        {
                                            i++;
                                            Console.WriteLine("> " + args[i] + " - " + (i + 1 < args.Length - 1 ? (Regex.IsMatch(args[i + 1], @"^[a-z0-9]") ? args[++i] : "null") : "null"));
                                        }
                                    else
                                        Console.WriteLine(">Error param with command <-k> turn <key> and <value>");

                                    break;

                                default:
                                    Console.WriteLine("> Command <" + args[i] + "> not supported use </?> to see set of allowed commands");
                                    break;
                            }
                        else
                            Console.WriteLine("> Command <" + args[i] + "> not supported use </?> to see set of allowed commands");
                }

                Console.ForegroundColor = ConsoleColor.Yellow;

                exit = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;

                args = exit.Split(new char[] { ' ' });



            } while (!exit.Equals("-exit"));

        }

        static void GoodMusic()
        {

            for (int i = 0; i < 2; i++)
            {
                Console.Beep(247, 400);
                Console.Beep(220, 400);
                Console.Beep(196, 400);
                Console.Beep(220, 400);
                Console.Beep(247, 400);
                Console.Beep(247, 400);
                Console.Beep(247, 800);
                Console.Beep(220, 400);
                Console.Beep(220, 400);
                Console.Beep(220, 800);
                Console.Beep(247, 400);
                Console.Beep(294, 400);
                Console.Beep(294, 800);
            }

        }
        static void SpaceMusic()
        {
            Random rand = new Random();

            for (int i = 0; i < 15; i++)
            {
                CorrectRand(ref rand);

                int Note = rand.Next(140, 500);

                CorrectRand(ref rand);

                int Pause = rand.Next(150, 300);

                Console.Beep(Note, Pause);
            }
        }
        static void CorrectRand(ref Random r)
        {
            int max = r.Next(DateTime.Now.Second);
            for (int i_ = 0; i_ < max; i_++)
            {
                r.Next();
            }
        }
    }
}