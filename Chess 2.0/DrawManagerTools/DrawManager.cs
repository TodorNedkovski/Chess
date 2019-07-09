using RealChess.Contracts;
using RealChess.Coordinates;
using RealChess.Table;
using System;
using System.Collections.Generic;

namespace RealChess.DrawManagerTools
{
    public class DrawManager
    {
        private readonly BoardColors boardColors;

        public DrawManager()
        {
            this.boardColors = new BoardColors();
        }

        public void PrintCharacter(IFigure figure, Coordinate coordinates)
        {
            int top = coordinates.Y;
            int left = coordinates.X;

            ConsoleColor color = figure.Color;

            for (int i = 0; i < 4; i++)
            {
                for (int b = 0; b < 11; b++)
                {
                    if (figure.Design[i, b])
                    {
                        Console.SetCursorPosition(left, top);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(left, top);
                        Console.BackgroundColor = color;
                    }

                    left++;
                }

                top++;

                left = coordinates.X;
            }
        }

        public void UndoCharacter(Coordinate coordinates)
        {
            var blockToUndo = this.boardColors.ColorBoard[coordinates.X - 1, coordinates.Y - 1];

            int left = 32;
            int top = 3;

            if (coordinates.X == 1)
            {
                top += 28;
            }
            else if (coordinates.X == 2)
            {
                top += 24;
            }
            else if (coordinates.X == 3)
            {
                top += 20;
            }
            else if (coordinates.X == 4)
            {
                top += 16;
            }
            else if (coordinates.X == 5)
            {
                top += 12;
            }
            else if (coordinates.X == 6)
            {
                top += 8;
            }
            else if (coordinates.X == 7)
            {
                top += 4;
            }

            if (coordinates.Y == 2)
            {
                left += 11;
            }
            else if (coordinates.Y == 3)
            {
                left += 22;
            }
            else if (coordinates.Y == 4)
            {
                left += 33;
            }
            else if (coordinates.Y == 5)
            {
                left += 44;
            }
            else if (coordinates.Y == 6)
            {
                left += 55;
            }
            else if (coordinates.Y == 7)
            {
                left += 66;
            }
            else if (coordinates.Y == 8)
            {
                left += 77;
            }

            int buffer = left;

            for (int i = 0; i < 4; i++)
            {
                for (int b = 0; b < 11; b++)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(left, top);
                    Console.BackgroundColor = blockToUndo;

                    left++;
                }

                top++;

                left = buffer;
            }
        }

        public void Draw(string symbol, List<Coordinate> coordinates)
        {
            for (int i = 0; i < coordinates.Count; i++)
            {
                Console.SetCursorPosition(coordinates[i].X, coordinates[i].Y);
                Console.WriteLine(symbol);
            }
        }
    }
}