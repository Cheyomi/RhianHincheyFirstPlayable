using System;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using System.Text;

class Test
{

    static string path = @"RPGMap.txt";
    static string[] map = File.ReadAllLines(path, Encoding.UTF8); //Reading all lines of the file into an array
    public static void Main()
    {
        StartGame();
        DisplayMap();

        //▒ - Grass (Majority)
        //▓ - Mountain
        //░ - Forest


    }


    static void StartGame()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║                                                      ║");
        Console.WriteLine("║        Click any button to start the game!           ║");
        Console.WriteLine("║                                                      ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.ReadKey();
        Console.Clear();

    }

    static void DisplayMap()
    {
        foreach (string line in map)
        {
            line.Replace("\n", "");

            foreach (char tile in line)
            {
                if (tile == '▒')
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else if (tile == '▓')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (tile == '░')
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else if (tile == '+')
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else if (tile == '-')
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else if (tile == '|')
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }


                    Console.Write(tile);
                

            }

            Console.WriteLine();
            Console.ResetColor();



        }




    }




}
