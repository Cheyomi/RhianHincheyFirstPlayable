using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;


namespace RhianHincheyFirstPlayable
{
    public static class Program
    {

        static string path = @"RPGMap.txt";
        static string[] map = File.ReadAllLines(path, Encoding.UTF8); //Reading all lines of the file into an array

        static int playerX = 9;
        static int playerY = 2;
        static int playerHealth = 10;
        static bool isPlaying = true;

        static int enemyX = 15;
        static int enemyY = 10;
        static int enemyHealth = 3;
        static int enemyDamage = 1;

        static char[] allKeybindings = new char[]
        {
        'W', 'A', 'S', 'D',
        };

        public static void Main()
        {
            
            StartGame();
            DrawMap();
            DrawPlayer();


            while (isPlaying)
            {
                HUD();
                ProcessInput();
                DrawPlayer();
                AttackEnemy();
                Thread.Sleep(100);
                MoveEnemy();
                DrawEnemy();
                
            }
                



            //▒ - Grass (Majority)
            //▓ - Mountain
            //░ - Forest


        }


        static void StartGame()
        {
            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("On that fateful day...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("It was time for your revenge...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("He ate your beloved friend..");
            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("HE ATE NINE! AVENGE YOUR FRIEND!");
            Console.ReadKey();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                      ║");
            Console.WriteLine("║            Click any button to avenge Nine.          ║");
            Console.WriteLine("║                                                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }

        static void ProcessInput()
        {
            int playerInputX = 0;
            int playerInputY = 0;

            ConsoleKey input = ConsoleKey.NoName;
            while (!allKeybindings.Contains(((char)input)))
            {
                input = Console.ReadKey(true).Key;
            }


            if (input == ConsoleKey.A) playerInputX = -1;
            if (input == ConsoleKey.D) playerInputX = 1;
            if (input == ConsoleKey.W) playerInputY = -1;
            if (input == ConsoleKey.S) playerInputY = 1;

            int newY = playerY + playerInputY;
            int newX = playerX + playerInputX;

            if (newY >= 0 && newX >= 0 && newY < map.GetLength(0) && newX < map[0].Length)
            {
                if (map[newY][newX] == '▒' || map[newY][newX] == '░')
                {
                    ClearPlayer();
                    playerX = newX;
                    playerY = newY;
                }

            }

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }


        }


        static void DrawPlayer()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(playerX, playerY);
            Console.Write('7');
            Console.BackgroundColor = ConsoleColor.Black;
        }

        static void ClearPlayer()
        {
            Console.SetCursorPosition(playerX, playerY);
            DrawTile(map[playerY][playerX]);
        }


        static void DrawMap()
        {
            Console.SetCursorPosition(0, 0);

            foreach (string line in map)
            {
                foreach (char tile in line)
                {
                    DrawTile(tile);
                }
                Console.WriteLine();
                Console.ResetColor();
            }



        }

        static void DrawTile(char tile)
        {
            switch (tile)
            {
                case '▒':
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(tile);
                    break;
                case '▓':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(tile);
                    break;
                case '░':
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(tile);
                    break;
                default:
                    Console.ResetColor();
                    Console.Write(tile);
                    break;
                case '+':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(tile);
                    break;
                case '|':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(tile);
                    break;
                case '-':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(tile);
                    break;
            }
        }

        static void MoveEnemy()
        {
            ClearEnemy();
            if (playerX < enemyX)
            {
                enemyX--;
            }
            else if (playerX > enemyX)
            {
                enemyX++;
            }
            else if (playerY < enemyY)
            {
                enemyY--;
            }
            else if (playerY > enemyY)
            {
                enemyY++;
            }


        }

        static void DrawEnemy()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(enemyX, enemyY);
            Console.Write('6');
            Console.BackgroundColor = ConsoleColor.Black;
        }

        static void ClearEnemy()
        {
            Console.SetCursorPosition(enemyX, enemyY);
            DrawTile(map[enemyY][enemyX]);
        }

        static void AttackEnemy()
        {
            if (playerX == enemyX && playerY == enemyY)
            {
                enemyHealth--;
            }
        }

        static void HUD()
        {
            Console.SetCursorPosition(5, 19);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("================ LEGEND ================");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("      6  -> Player");
            Console.WriteLine("      Player Health: " + playerHealth);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("      7  -> Enemy");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("       Enemy Health: " + enemyHealth);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("      ▒  -> Grass");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("      ▓  -> Water");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("      ░  -> Forest");

            


            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("      Player Position: X" + playerX + ", Y" + playerY + "     ");
            Console.WriteLine("      Enemy Position: X" + enemyX + ", Y" + enemyY + "      ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("     ======================================");

            Console.ResetColor();
        }




    }
}