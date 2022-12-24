using System;
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
            int playerPositionX;
            int playerPositionY;
            int playerDirectionX = 0;
            int playerDirectionY = 1;
            char[,] map = ReadMap("Map", out playerPositionX, out playerPositionY);
            char wallImage = '#';

            DrawMap(map);

            while (isPlaying)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeDirection(key, ref playerDirectionX, ref playerDirectionY);

                    if (map[playerPositionX + playerDirectionX, playerPositionY + playerDirectionY] != wallImage)
                    {
                        Move(ref playerPositionX,ref playerPositionY, playerDirectionX, playerDirectionY);
                    }
                }
            }
        }

        static void ChangeDirection(ConsoleKeyInfo key, ref int directionX, ref int directionY)
        {
            const ConsoleKey MoveUp = ConsoleKey.UpArrow;
            const ConsoleKey MoveDown = ConsoleKey.DownArrow;
            const ConsoleKey MoveLeft = ConsoleKey.LeftArrow;
            const ConsoleKey MoveRight = ConsoleKey.RightArrow;

            switch (key.Key)
            {
                case MoveUp:
                    directionX = -1; 
                    directionY = 0;
                    break;
                case MoveDown:
                    directionX = 1; 
                    directionY = 0;
                    break;
                case MoveLeft:
                    directionX = 0; 
                    directionY = -1;
                    break;
                case MoveRight:
                    directionX = 0; 
                    directionY = 1;
                    break;
            }
        }

        static void Move(ref int positionX, ref int positionY, int directionX, int directionY)
        {
            char playerImage = '@';
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(' ');

            positionX += directionX;
            positionY += directionY;

            Console.SetCursorPosition(positionY, positionX);
            Console.Write(playerImage);
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

        static char[,] ReadMap(string mapName, out int playerPositionX, out int playerPositionY)
        {
            char playerImage = '@';
            playerPositionX = 0;
            playerPositionY = 0;
            string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i,j] = newFile[i][j];

                    if(map[i,j] == playerImage)
                    {
                        playerPositionX = i;
                        playerPositionY = j;
                    }
                }
            }

            return map;
        }
    }
}
