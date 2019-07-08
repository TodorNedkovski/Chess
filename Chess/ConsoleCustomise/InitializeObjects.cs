using RealChess.Contracts;
using RealChess.Coordinates;
using RealChess.Core;
using RealChess.DrawManagerTools;
using RealChess.Objects;
using System;
using System.Collections.Generic;

namespace RealChess.ConsoleCustomise
{
    public class InitializeObjects
    {
        private DrawManager drawManager;

        public InitializeObjects()
        {
            this.drawManager = new DrawManager();
        }

        public void InitializeBorders()
        {
            this.InitializeLeftVericalBorder();

            this.InitializeRightVerticalBorder();

            this.InitializeDownHorizontalBorder();

            this.InitializeUpHorizontalBorder();

            int top = 4;
            int left = 35;

            //Left vertical numbers
            for (int i = 8; i >= 1; i--)
            {
                Console.SetCursorPosition(29, top);
                Console.WriteLine(i);
                top += 4;
            }

            top = 4;

            //Right vertical numbers
            for (int i = 8; i >= 1; i--)
            {
                Console.SetCursorPosition(121, top);
                Console.WriteLine(i);
                top += 4;
            }

            //Up horizontal numbers
            for (int i = 1; i <= 8; i++)
            {
                Console.SetCursorPosition(left, 1);
                Console.WriteLine(i);
                left += 11;
            }

            left = 35;

            //Down horizontal numbers
            for (int i = 1; i <= 8; i++)
            {
                Console.SetCursorPosition(left, 36);
                Console.WriteLine(i);
                left += 11;
            }
        }

        public void InitializeCharazters()
        {
            //black

            this.PrintCharacter(new Rook(new Coordinate(32, 3), ConsoleColor.Black));

            this.PrintCharacter(new Rook(new Coordinate(32, 3), ConsoleColor.Black));

            this.PrintCharacter(new Knight(new Coordinate(43, 3), ConsoleColor.Black));

            this.PrintCharacter(new Bishop(new Coordinate(54, 3), ConsoleColor.Black));

            this.PrintCharacter(new Queen(new Coordinate(65, 3), ConsoleColor.Black));

            this.PrintCharacter(new King(new Coordinate(76, 3), ConsoleColor.Black));

            this.PrintCharacter(new Bishop(new Coordinate(87, 3), ConsoleColor.Black));

            this.PrintCharacter(new Knight(new Coordinate(98, 3), ConsoleColor.Black));

            this.PrintCharacter(new Rook(new Coordinate(109, 3), ConsoleColor.Black));

            this.PrintCharacter(new Pawn(new Coordinate(32, 7), ConsoleColor.Black));

            this.PrintCharacter(new Pawn(new Coordinate(43, 7), ConsoleColor.Black));

            this.PrintCharacter(new Pawn(new Coordinate(54, 7), ConsoleColor.Black));

            this.PrintCharacter(new Pawn(new Coordinate(65, 7), ConsoleColor.Black));

            this.PrintCharacter(new Pawn(new Coordinate(76, 7), ConsoleColor.Black));

            this.PrintCharacter(new Pawn(new Coordinate(87, 7), ConsoleColor.Black));

            this.PrintCharacter(new Pawn(new Coordinate(98, 7), ConsoleColor.Black));

            this.PrintCharacter(new Pawn(new Coordinate(109, 7), ConsoleColor.Black));

            // white

            this.PrintCharacter(new Rook(new Coordinate(32, 31), ConsoleColor.White));

            this.PrintCharacter(new Rook(new Coordinate(32, 31), ConsoleColor.White));

            this.PrintCharacter(new Knight(new Coordinate(43, 31), ConsoleColor.White));

            this.PrintCharacter(new Bishop(new Coordinate(54, 31), ConsoleColor.White));

            this.PrintCharacter(new Queen(new Coordinate(65, 31), ConsoleColor.White));

            this.PrintCharacter(new King(new Coordinate(76, 31), ConsoleColor.White));

            this.PrintCharacter(new Bishop(new Coordinate(87, 31), ConsoleColor.White));

            this.PrintCharacter(new Knight(new Coordinate(98, 31), ConsoleColor.White));

            this.PrintCharacter(new Rook(new Coordinate(109, 31), ConsoleColor.White));
            //
            this.PrintCharacter(new Pawn(new Coordinate(32, 27), ConsoleColor.White));

            this.PrintCharacter(new Pawn(new Coordinate(43, 27), ConsoleColor.White));

            this.PrintCharacter(new Pawn(new Coordinate(54, 27), ConsoleColor.White));

            this.PrintCharacter(new Pawn(new Coordinate(65, 27), ConsoleColor.White));

            this.PrintCharacter(new Pawn(new Coordinate(76, 27), ConsoleColor.White));

            this.PrintCharacter(new Pawn(new Coordinate(87, 27), ConsoleColor.White));

            this.PrintCharacter(new Pawn(new Coordinate(98, 27), ConsoleColor.White));

            this.PrintCharacter(new Pawn(new Coordinate(109, 27), ConsoleColor.White));
        }

        private void PrintCharacter(IFigure figure)
        {
            var color = figure.Color;

            int col = figure.Coordinates.Y;
            int row = figure.Coordinates.X;

            for (int i = 0; i < 4; i++)
            {
                for (int b = 0; b < 11; b++)
                {
                    if (figure.Design[i, b])
                    {
                        Console.SetCursorPosition(row, col);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(row, col);
                        Console.BackgroundColor = color;
                    }

                    row++;
                }

                col++;

                row = figure.Coordinates.X;
            }
        }

        public void InitializeSqures()
        {
            int col = 3;
            int row = 31;

            for (int f = 0; f < 4; f++)
            {
                for (int r = 0; r < 4; r++)
                {
                    //one row
                    for (int c = 0; c < 4; c++)
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            Console.SetCursorPosition(row, col);
                            Console.WriteLine(" ");
                            Console.BackgroundColor = ConsoleColor.Gray;
                            row++;
                        }

                        for (int i = 0; i < 11; i++)
                        {
                            Console.SetCursorPosition(row, col);
                            Console.WriteLine(" ");
                            Console.BackgroundColor = ConsoleColor.DarkGray;

                            row++;
                        }
                    }

                    col++;
                    row = 31;
                    Console.SetCursorPosition(row, col);
                    Console.BackgroundColor = ConsoleColor.White;
                }

                for (int r = 0; r < 4; r++)
                {
                    for (int c = 0; c < 4; c++)
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            Console.SetCursorPosition(row, col);
                            Console.WriteLine(" ");
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            row++;
                        }

                        for (int i = 0; i < 11; i++)
                        {
                            Console.SetCursorPosition(row, col);
                            Console.WriteLine(" ");
                            Console.BackgroundColor = ConsoleColor.Gray;
                            row++;
                        }
                    }

                    col++;
                    row = 31;
                    Console.SetCursorPosition(row, col);
                    Console.BackgroundColor = ConsoleColor.White;
                }
            }
        }

        private void InitializeLeftVericalBorder()
        {
            for (int y = 2; y < 35; y++)
            {
                this.drawManager.Draw("|", new List<Coordinate> { new Coordinate(30, y) });
            }
        }

        private void InitializeRightVerticalBorder()
        {
            for (int y = 2; y < 35; y++)
            {
                this.drawManager.Draw("|", new List<Coordinate> { new Coordinate(120, y) });
            }
        }

        private void InitializeUpHorizontalBorder()
        {
            for (int x = 31; x < 120; x++)
            {
                this.drawManager.Draw("-", new List<Coordinate> { new Coordinate(x, 2) });
            }
        }

        private void InitializeDownHorizontalBorder()
        {
            for (int x = 31; x < 120; x++)
            {
                this.drawManager.Draw("-", new List<Coordinate> { new Coordinate(x, 35) });
            }
        }
    }
}