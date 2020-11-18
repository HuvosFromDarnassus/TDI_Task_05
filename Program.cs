using System;
using System.IO;
using System.Text;

namespace TDI_Task_05
{
    class Program
    {
        static string[] newFile = File.ReadAllLines($"D:/DOCUMENTS/STADYING/PRACTICE/TrainingPractice_01/TDI_Task_05/map1.txt");
        static char[,] map = new char[newFile.Length, newFile[0].Length];

        static int HeroX;
        static int HeroY;

        static int E_enX;
        static int E_enY;

        static int V_enX;
        static int V_enY;

        static int O_enX;
        static int O_enY;

        static int Fx;
        static int Fy;


        static ConsoleKeyInfo key;
        static Random rnd = new Random();
        static int Helth = 10;
        static bool mist = false;

        static char[,] ReadMap(string mapName)
        {
            HeroX = 0;
            HeroY = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == '■')
                    {
                        HeroX = i;
                        HeroY = j;
                    }
                    else if (map[i, j] == 'F')
                    {
                        Fx = i;
                        Fy = j;
                    }
                    else if (map[i, j] == 'E')
                    {
                        E_enX = i;
                        E_enY = j;
                    }
                    else if (map[i, j] == 'V')
                    {
                        V_enX = i;
                        V_enY = j;
                    }
                    else if (map[i, j] == 'O')
                    {
                        O_enX = i;
                        O_enY = j;
                    }

                }
            }
            return map;
        }


        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void En_way(char n, ref int x, ref int y)
        {
            int w;

        Sicl:
            w = rnd.Next(1, 4);
            switch (w)
            {
                case 1:
                    if ((map[x, y - 1] != '█') && (y > 0))
                    {
                        map[x, y - 1] = n;
                        map[x, y] = ' ';
                        y--;
                    }
                    else goto Sicl;
                    break;

                case 2:
                    if ((map[x - 1, y] != '█') && (x > 1))
                    {
                        map[x - 1, y] = n;
                        map[x, y] = ' ';
                        x--;
                    }
                    else goto Sicl;
                    break;

                case 3:
                    if ((map[x, y + 1] != '█') && (y < map.GetLength(1)))
                    {
                        map[x, y + 1] = n;
                        map[x, y] = ' ';
                        y++;
                    }
                    else goto Sicl;
                    break;

                case 4:
                    if ((map[x + 1, y] != '█') && (x < map.GetLength(0)))
                    {
                        map[x + 1, y] = n;
                        map[x, y] = ' ';
                        x++;
                    }
                    else goto Sicl;
                    break;
            }
        }

        static void HP_Mistakes()
        {
            Console.Write('[');
            for (int i = 0; i < Helth; i++)
            {
                Console.Write('#');
            }
            if (Helth < 10)
            {
                for (int i = 0; i < 10 - Helth; i++)
                {
                    Console.Write('_');
                }
            }
            Console.Write("]\n");
            if (mist == true)
            {
                Console.WriteLine("There's a wall ahead, there's no way\n");
                mist = false;
            }
            else Console.Write("\n\n");
        }


        static void Main(string[] args)
        {

            char[,] map = ReadMap("map.txt");


            do
            {
                HP_Mistakes();
                DrawMap(map);

                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.LeftArrow)
                {
                    if ((map[HeroX, HeroY - 1] != '█') && (HeroY != 0))
                    {
                        map[HeroX, HeroY - 1] = '■';
                        map[HeroX, HeroY] = '*';
                        HeroY--;
                    }
                    else mist = true;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if ((map[HeroX, HeroY + 1] != '█') && (HeroY != map.GetLength(1)))
                    {
                        map[HeroX, HeroY + 1] = '■';
                        map[HeroX, HeroY] = '*';
                        HeroY++;
                    }
                    else mist = true;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if ((map[HeroX - 1, HeroY] != '█') && (HeroX != 0))
                    {
                        map[HeroX - 1, HeroY] = '■';
                        map[HeroX, HeroY] = '*';
                        HeroX--;
                    }
                    else mist = true;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if ((map[HeroX + 1, HeroY] != '█') && (HeroX != map.GetLength(0)))
                    {
                        map[HeroX + 1, HeroY] = '■';
                        map[HeroX, HeroY] = '*';
                        HeroX++;
                    }
                    else mist = true;
                }
                if ((HeroX == E_enX) && (HeroY == E_enY) || (HeroX == V_enX) && (HeroY == V_enY) || (HeroX == O_enX) && (HeroY == V_enY)) Helth--;

                En_way('E', ref E_enX, ref E_enY);
                En_way('V', ref V_enX, ref V_enY);
                En_way('O', ref O_enX, ref O_enY);

                if ((HeroX == E_enX) && (HeroY == E_enY) || (HeroX == V_enX) && (HeroY == V_enY) || (HeroX == O_enX) && (HeroY == V_enY)) Helth--;

                Console.Clear();

            } while ((map[HeroX, HeroY] != map[Fx, Fy]) && (Helth > 0));

            if (map[HeroX, HeroY] == map[Fx, Fy])
            {

            }
        }
        }
}
