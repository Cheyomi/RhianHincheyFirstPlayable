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

        static int Clamp(this int value, int min, int max)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        public static void Main()
        {
            
            StartGame();
            DrawMap();
            DrawPlayer();


            while (isPlaying)
            {
               
                ProcessInput();
                DrawPlayer();
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
            int distanceX = playerX - enemyX;
            int distanceY = playerY - enemyY;

            if (Math.Abs(distanceX) > Math.Abs(distanceY))
            {
                if (distanceX > 0)
                {
                    MoveEnemyToSpot(enemyX + 1, enemyY);
                }
                else
                {
                    MoveEnemyToSpot(enemyX - 1, enemyY);
                }
            }
            else
            {
                if (distanceY > 0)
                {
                    MoveEnemyToSpot(enemyX, enemyY + 1);
                }
                else
                {
                    MoveEnemyToSpot(enemyX, enemyY - 1 );
                }
            }

        }

        static void MoveEnemyToSpot(int x, int y)
        {
            ClearEnemy();

            if (map[y][x] == '▒' || map[y][x] == '░')
            {
                enemyY = y;
                enemyX = x;
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



    }
}