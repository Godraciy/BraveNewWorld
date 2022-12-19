﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp4
{
    internal class Program
    {
        //Сделать игровую карту с помощью двумерного массива.Сделать функцию рисования карты.
        //Помимо этого, дать пользователю возможность перемещаться по карте и взаимодействовать с элементами (например пользователь не может пройти сквозь стену)
        //Все элементы являются обычными символами

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            bool isPlaying = true;
            int playerX, playerY;
            int playerDX = 0, playerDY = 1;
            char[,] map = ReadMap("Map", out playerX, out playerY);

            DrawMap(map);

            while (isPlaying)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeDirection(key, ref playerDX, ref playerDY);

                    if (map[playerX + playerDX, playerY + playerDY] != '#')
                    {
                        Move(ref playerX,ref playerY, playerDX, playerDY);
                    }
                }
            }
        }

        static void ChangeDirection(ConsoleKeyInfo key, ref int DX,ref int DY)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    DX = -1; DY = 0;
                    break;
                case ConsoleKey.DownArrow:
                    DX = 1; DY = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    DX = 0; DY = -1;
                    break;
                case ConsoleKey.RightArrow:
                    DX = 0; DY = 1;
                    break;
            }
        }

        static void Move(ref int X, ref int Y, int DX, int DY)
        {
            Console.SetCursorPosition(Y, X);
            Console.Write(' ');

            X += DX;
            Y += DY;

            Console.SetCursorPosition(Y, X);
            Console.Write("@");
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

        static char[,] ReadMap(string mapName, out int playerX,out int playerY)
        {
            playerX = 0;
            playerY = 0;
            string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i,j] = newFile[i][j];

                    if(map[i,j] == '@')
                    {
                        playerX= i;
                        playerY= j;
                    }
                }
            }

            return map;
        }
    }
}
