using RealChess.ConsoleCustomise;
using RealChess.DrawManagerTools;
using System.Collections.Generic;
using RealChess.Objects.Players;
using RealChess.Coordinates;
using RealChess.Contracts;
using RealChess.Objects;
using RealChess.Table;
using System;

namespace RealChess.Core
{
    public class Engine
    {
        private int xCoordinateToPrintMessage = 141;

        private bool hasTheBlackKingMoved = false;

        private bool hasTheBlackRookMoved = false;

        private bool hasTheWhiteKingMoved = false;

        private bool hasTheWhiteRookMoved = false;

        private readonly InitializeObjects initializeObjects;
        private readonly BoardColors boardColors;
        private readonly DrawManager drawManager;
        private readonly Player whitePlayer;
        private readonly Player blackPlayer;
        private readonly Board board;

        public Engine()
        {
            this.initializeObjects = new InitializeObjects();
            this.boardColors = new BoardColors();
            this.drawManager = new DrawManager();
            this.whitePlayer = new Player();
            this.blackPlayer = new Player();
            this.board = new Board();
            this.InitilizeInitialBoard();
        }

        public void Run()
        {
            try
            {
                Console.SetCursorPosition(127, 4);
                Console.WriteLine("Do you want to end? y/n");

                string answer = string.Empty;

                while (true)
                {
                    Console.SetCursorPosition(135, 10);
                    Console.WriteLine("Move: ");
                    Console.SetCursorPosition(141, 10);
                    var coordinates = Console.ReadLine();

                    if (coordinates == "y") break;

                    var coordinate = coordinates.Split(new string[] { " ", "-" },
                        StringSplitOptions.RemoveEmptyEntries);

                    int firstX = int.Parse(coordinate[0]);
                    int firstY = coordinate[1].ToCharArray()[0] - 96;
                    int secondX = int.Parse(coordinate[2]);
                    int secondY = coordinate[3].ToCharArray()[0] - 96;

                    if (this.CheckingPosition(new Coordinate(firstX, firstY),
                        new Coordinate(secondX, secondY))
                        && this.IsThereSameColorFigureInFront(new Coordinate(firstX, firstY),
                        new Coordinate(secondX, secondY)))
                    {
                        this.Move(new Coordinate(firstX, firstY), new Coordinate(secondX, secondY));
                    }
                    else if (this.IsThereCastling(this.board.Table[firstX - 1, firstY - 1].Color))
                    {
                        if (firstX == secondX && (firstY - 2 == secondY || firstY + 2 == secondY))
                        {
                            this.Move(new Coordinate(firstX, secondY), new Coordinate(secondX, secondY));
                        }
                    }

                    if (this.whitePlayer.Win || this.blackPlayer.Win || IsGameEnd()) break;

                    Console.SetCursorPosition(xCoordinateToPrintMessage, 10);
                    Console.WriteLine(" ");
                    Console.BackgroundColor = ConsoleColor.White;

                    for (int i = 0; i < 10; i++)
                    {
                        Console.SetCursorPosition(xCoordinateToPrintMessage, 10);
                        Console.WriteLine(" ");
                        Console.BackgroundColor = ConsoleColor.White;
                        xCoordinateToPrintMessage++;
                    }

                    xCoordinateToPrintMessage = 141;
                }

                Console.SetCursorPosition(xCoordinateToPrintMessage, 10);
                Console.WriteLine(" ");
                Console.BackgroundColor = ConsoleColor.White;

                xCoordinateToPrintMessage = 141;

                for (int i = 0; i < 10; i++)
                {
                    Console.SetCursorPosition(xCoordinateToPrintMessage, 10);
                    Console.WriteLine(" ");
                    Console.BackgroundColor = ConsoleColor.White;
                    xCoordinateToPrintMessage++;
                }

                xCoordinateToPrintMessage = 127;

                for (int i = 0; i < 23; i++)
                {
                    Console.SetCursorPosition(xCoordinateToPrintMessage, 4);
                    Console.WriteLine(" ");
                    Console.BackgroundColor = ConsoleColor.White;
                    xCoordinateToPrintMessage++;
                }

                xCoordinateToPrintMessage = 135;

                for (int i = 0; i < 23; i++)
                {
                    Console.SetCursorPosition(xCoordinateToPrintMessage, 10);
                    Console.WriteLine(" ");
                    Console.BackgroundColor = ConsoleColor.White;
                    xCoordinateToPrintMessage++;
                }

                // win announcement
                if (this.whitePlayer.Win)
                {
                    Console.SetCursorPosition(140, 3);
                    Console.WriteLine("White player wins!!");
                }
                else if (this.blackPlayer.Win)
                {
                    Console.SetCursorPosition(140, 3);
                    Console.WriteLine("Black player wins!!");
                }
                else
                {
                    Console.SetCursorPosition(140, 3);
                    Console.WriteLine("Tie");
                }
            }
            catch (Exception)
            {
                xCoordinateToPrintMessage = 141;

                for (int i = 0; i < 10; i++)
                {
                    Console.SetCursorPosition(xCoordinateToPrintMessage, 10);
                    Console.WriteLine(" ");
                    Console.BackgroundColor = ConsoleColor.White;
                    xCoordinateToPrintMessage++;
                }

                xCoordinateToPrintMessage = 127;

                for (int i = 0; i < 23; i++)
                {
                    Console.SetCursorPosition(xCoordinateToPrintMessage, 4);
                    Console.WriteLine(" ");
                    Console.BackgroundColor = ConsoleColor.White;
                    xCoordinateToPrintMessage++;
                }

                xCoordinateToPrintMessage = 135;

                for (int i = 0; i < 23; i++)
                {
                    Console.SetCursorPosition(xCoordinateToPrintMessage, 10);
                    Console.WriteLine(" ");
                    Console.BackgroundColor = ConsoleColor.White;
                    xCoordinateToPrintMessage++;
                }

                this.Run();
            }

            Console.SetCursorPosition(0, 0);
        }

        private void InitilizeInitialBoard()
        {
            ConsoleWindow.CustomizeConsole();

            this.initializeObjects.InitializeBorders();

            this.initializeObjects.InitializeSqures();

            this.initializeObjects.InitializeCharazters();
        }

        //TODO twin figures
        private int[] FindFigures(string typeName)
        {
            var valueToReturn = new int[4];

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (this.board.Table[row, col] != null)
                    {
                        var findingBlackKing = this.board.Table[row, col].Color == ConsoleColor.Black && this.board.Table[row, col].GetType().Name == typeName;

                        if (findingBlackKing)
                        {
                            valueToReturn[0] = row;
                            valueToReturn[1] = col;
                            break;
                        }
                    }
                }
            }

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (this.board.Table[row, col] != null)
                    {
                        var findingBlackKing = this.board.Table[row, col].Color == ConsoleColor.White && this.board.Table[row, col].GetType().Name == typeName;

                        if (findingBlackKing)
                        {
                            valueToReturn[2] = row;
                            valueToReturn[3] = col;
                            break;
                        }
                    }
                }
            }

            return valueToReturn;
        }

        private bool CheckingDiagonalLinesForSameColoredFigures(int minX, int maxX, int minY, int maxY, Coordinate coordinate, Coordinate toCoordinate, IFigure figureToMove)
        {
            int col = minY;

            if (coordinate.X < toCoordinate.X && coordinate.Y < toCoordinate.Y)
            {
                col = minY;

                for (int row = minX; row < maxX; row++)
                {
                    if (this.board.Table[row, col] != null)
                    {
                        if (this.board.Table[row, col].Color == figureToMove.Color)
                        {
                            return false;
                        }
                    }

                    col++;
                }
            }
            else if (coordinate.X > toCoordinate.X && coordinate.Y < toCoordinate.Y)
            {
                col = maxY - 1;

                for (int row = minX; row < maxX; row++)
                {
                    if (this.board.Table[row - 1, col] != null)
                    {
                        if (this.board.Table[row - 1, col].Color == figureToMove.Color)
                        {
                            return false;
                        }
                    }

                    col--;
                }
            }
            else if (coordinate.X < toCoordinate.X && coordinate.Y > toCoordinate.Y)
            {
                col = maxY - 1;

                for (int row = minX; row < maxX; row++)
                {
                    if (this.board.Table[row, col - 1] != null)
                    {
                        if (this.board.Table[row, col - 1].Color == figureToMove.Color)
                        {
                            return false;
                        }
                    }

                    col--;
                }
            }
            else if (coordinate.X > toCoordinate.X && coordinate.Y > toCoordinate.Y)
            {
                for (int row = minX; row < maxX; row++)
                {
                    if (this.board.Table[row - 1, col - 1] != null)
                    {
                        if (this.board.Table[row - 1, col - 1].Color == figureToMove.Color)
                        {
                            return false;
                        }
                    }

                    col++;
                }
            }

            return true;
        }

        private bool CheckingHorizontalLinesForSameColoredFigures(int minY, int maxY, Coordinate coordinate, Coordinate toCoordinate, IFigure figureToMove)
        {
            if (coordinate.Y > toCoordinate.Y)
            {
                for (int col = minY; col < maxY; col++)
                {
                    if (this.board.Table[coordinate.X - 1, col - 1] != null)
                    {
                        if (this.board.Table[coordinate.X - 1, col - 1].Color == figureToMove.Color)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int col = minY; col < maxY; col++)
                {
                    if (this.board.Table[coordinate.X - 1, col] != null)
                    {
                        if (this.board.Table[coordinate.X - 1, col].Color == figureToMove.Color)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool CheckingVerticalLinesForSameColoredFigures(int minX, int maxX, Coordinate coordinate, Coordinate toCoordinate, IFigure figureToMove)
        {
            if (coordinate.X > toCoordinate.X)
            {
                if (figureToMove is Pawn && figureToMove.Color == ConsoleColor.White) return false;

                for (int row = minX; row < maxX; row++)
                {
                    if (this.board.Table[row - 1, coordinate.Y - 1] != null)
                    {
                        if (this.board.Table[row - 1, coordinate.Y - 1].Color == figureToMove.Color)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (figureToMove is Pawn && figureToMove.Color == ConsoleColor.Black) return false;

                for (int row = minX; row < maxX; row++)
                {
                    if (this.board.Table[row, coordinate.Y - 1] != null)
                    {
                        if (this.board.Table[row, coordinate.Y - 1].Color == figureToMove.Color)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool CanPawnBePromoted(Coordinate coordinate, Coordinate toCoordinate, int left, int top)
        {
            if (this.board.Table[coordinate.X - 1, coordinate.Y - 1] is Pawn)
            {
                if (this.board.Table[coordinate.X - 1, coordinate.Y - 1].Color == ConsoleColor.Black && toCoordinate.X - 1 == 0)
                {
                    this.drawManager.UndoCharacter(coordinate);

                    this.drawManager.UndoCharacter(coordinate);

                    this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1] = new Queen(new Coordinate(toCoordinate.X, toCoordinate.Y), ConsoleColor.Black);

                    this.board.Table[coordinate.X - 1, coordinate.Y - 1] = null;

                    this.drawManager.PrintCharacter(new Queen(new Coordinate(toCoordinate.X, toCoordinate.Y), ConsoleColor.Black), new Coordinate(left, top));

                    this.drawManager.PrintCharacter(new Queen(new Coordinate(toCoordinate.X, toCoordinate.Y), ConsoleColor.Black), new Coordinate(left, top));

                    return true;
                }
                else if (this.board.Table[coordinate.X - 1, coordinate.Y - 1].Color == ConsoleColor.White && toCoordinate.X - 1 == 7)
                {
                    this.drawManager.UndoCharacter(coordinate);

                    this.drawManager.UndoCharacter(coordinate);

                    this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1] = new Queen(new Coordinate(toCoordinate.X, toCoordinate.Y), ConsoleColor.White);

                    this.board.Table[coordinate.X - 1, coordinate.Y - 1] = null;

                    this.drawManager.PrintCharacter(new Queen(new Coordinate(toCoordinate.X, toCoordinate.Y), ConsoleColor.White), new Coordinate(left, top));

                    this.drawManager.PrintCharacter(new Queen(new Coordinate(toCoordinate.X, toCoordinate.Y), ConsoleColor.White), new Coordinate(left, top));

                    return true;
                }
            }

            return false;
        }

        private List<IFigure> FindingSameColoredFigures(ConsoleColor color)
        {
            var valueToReturn = new List<IFigure>();

            for (int row = 0; row < this.board.Table.GetLength(0); row++)
            {
                for (int col = 0; col < this.board.Table.GetLength(1); col++)
                {
                    if (this.board.Table[row, col] != null)
                    {
                        if (this.board.Table[row, col].Color == color) valueToReturn.Add(this.board.Table[row, col]);
                    }
                }
            }

            return valueToReturn;
        }

        private List<int> AvailableCoordinates(IFigure figure)
        {
            var coordinatesToReturn = new List<int>();

            int x = 0;

            int y = 0;

            Coordinate coordinate = figure.Coordinates;

            //x = coordinate.X + 1;

            //y = coordinate.Y;

            x = coordinate.X;

            y = coordinate.Y - 1;

            if (this.board.Table[x, y] == null || this.board.Table[x, y].Color != ConsoleColor.White)
            {
                coordinatesToReturn.Add(x);

                coordinatesToReturn.Add(y);
            }

            //

            //x = coordinate.X;

            //y = coordinate.Y + 1;

            x = coordinate.X - 1;

            y = coordinate.Y;

            if (this.board.Table[x, y] == null || this.board.Table[x, y].Color != ConsoleColor.White)
            {
                coordinatesToReturn.Add(x);

                coordinatesToReturn.Add(y);
            }

            //

            //x = coordinate.X - 1;

            //y = coordinate.Y;

            x = coordinate.X - 2;

            y = coordinate.Y - 1;

            if (x > 0)
            {
                if (this.board.Table[x, y] == null || this.board.Table[x, y].Color != ConsoleColor.White)
                {
                    coordinatesToReturn.Add(x);

                    coordinatesToReturn.Add(y);
                }
            }

            //

            //x = coordinate.X;

            //y = coordinate.Y - 1;

            x = coordinate.X - 1;

            y = coordinate.Y - 2;

            if (y > 0)
            {
                if (this.board.Table[x, y] == null || this.board.Table[x, y].Color != ConsoleColor.White)
                {
                    coordinatesToReturn.Add(x);

                    coordinatesToReturn.Add(y);
                }
            }

            //

            //x = coordinate.X - 1;

            //y = coordinate.Y - 1;

            x = coordinate.X - 2;

            y = coordinate.Y - 2;

            if (x > 0 && y > 0)
            {
                if (this.board.Table[x, y] == null || this.board.Table[x, y].Color != ConsoleColor.White)
                {
                    coordinatesToReturn.Add(x);

                    coordinatesToReturn.Add(y);
                }
            }

            //

            //x = coordinate.X + 1;

            //y = coordinate.Y - 1;

            x = coordinate.X;

            y = coordinate.Y - 2;

            if (y > 0)
            {
                if (this.board.Table[x, y] == null || this.board.Table[x, y].Color != ConsoleColor.White)
                {
                    coordinatesToReturn.Add(x);

                    coordinatesToReturn.Add(y);
                }
            }

            //

            //x = coordinate.X - 1;

            //y = coordinate.Y + 1;

            x = coordinate.X - 2;

            y = coordinate.Y;

            if (x > 0)
            {
                if (this.board.Table[x, y] == null || this.board.Table[x, y].Color != ConsoleColor.White)
                {
                    coordinatesToReturn.Add(x);

                    coordinatesToReturn.Add(y);
                }
            }

            //

            //x = coordinate.X + 1;

            //y = coordinate.Y + 1;

            x = coordinate.X;

            y = coordinate.Y;

            if (this.board.Table[x, y] == null || this.board.Table[x, y].Color != ConsoleColor.White)
            {
                coordinatesToReturn.Add(x);

                coordinatesToReturn.Add(y);
            }

            return coordinatesToReturn;
        }

        //TODO black king
        private bool IsGameEnd()
        {
            int[] possiblePositionsX = new int[9];

            int[] possiblePositionsY = new int[9];

            var coordinates = this.FindFigures("King");

            var blackKing = this.board.Table[coordinates[0], coordinates[1]];

            var whiteKing = this.board.Table[coordinates[2], coordinates[3]];

            //int whiteKingX = whiteKing.Coordinates.X;

            //int whiteKingY = whiteKing.Coordinates.Y;

            //checking white king

            var available = this.AvailableCoordinates(whiteKing);

            var blackFigures = new List<IFigure>();

            var figuresAvailableToKill = new List<IFigure>();

            blackFigures = this.FindingSameColoredFigures(ConsoleColor.Black);

            for (int i = 0; i < blackFigures.Count; i++)
            {
                for (int j = 0; j < available.Count - 1; j++)
                {
                    if (this.CheckingPosition(blackFigures[i].Coordinates, new Coordinate(available[j], available[j + 1])))
                    {
                        figuresAvailableToKill.Add(blackFigures[i]);
                    }
                }
            }

            for (int i = 0; i < figuresAvailableToKill.Count; i++)
            {
                for (int j = 0; j < available.Count - 1; j++)
                {
                    if (!this.CheckingPosition(figuresAvailableToKill[j].Coordinates, new Coordinate(available[j], available[j + 1])))
                    {
                        return false;
                    }
                }
            }

            if (whiteKing.Coordinates.X == 1 && whiteKing.Coordinates.Y == 5) return false;

            return true;
        }

        //TODO EnPassant

        //TODO pin

        //TODO fork

        //TODO trifor repetition

        private bool IsThereCastling(ConsoleColor color)
        {
            int row = 0;

            if (color == ConsoleColor.Black)
            {
                var coordinates = this.FindFigures("King");

                row = 7;

                for (int col = 1; col < 4; col++)
                {
                    if (this.board.Table[row, col] != null)
                    {
                        return false;
                    }
                }

                for (int col = 5; col < 7; col++)
                {
                    if (this.board.Table[row, col] != null)
                    {
                        return false;
                    }
                }

                if (!this.hasTheBlackKingMoved
                    && !this.hasTheBlackRookMoved)
                {
                    return true;
                }

                //

                this.FindFigures("Rook");

                if (!this.hasTheBlackKingMoved
                    && !this.hasTheBlackKingMoved)

                {
                    return true;
                }
            }
            else
            {
                var coordinates = this.FindFigures("King");

                row = 0;

                for (int col = 1; col < 4; col++)
                {
                    if (this.board.Table[row, col] != null)
                    {
                        return false;
                    }
                }

                for (int col = 5; col < 7; col++)
                {
                    if (this.board.Table[row, col] != null)
                    {
                        return false;
                    }
                }

                if (!this.hasTheWhiteRookMoved
                    && !this.hasTheWhiteKingMoved)
                {
                    return true;
                }

                //

                this.FindFigures("Rook");

                if (!this.hasTheWhiteKingMoved
                    && !this.hasTheWhiteRookMoved)

                {
                    return true;
                }
            }

            return false;
        }

        private void Move(Coordinate coordinate, Coordinate toCoordinate)
        {
            int top = 3; // col

            int left = 32; // row

            var figureToMove = this.board.Table[coordinate.X - 1, coordinate.Y - 1];

            if (toCoordinate.X == 1)
            {
                top += 28;
            }
            else if (toCoordinate.X == 2)
            {
                top += 24;
            }
            else if (toCoordinate.X == 3)
            {
                top += 20;
            }
            else if (toCoordinate.X == 4)
            {
                top += 16;
            }
            else if (toCoordinate.X == 5)
            {
                top += 12;
            }
            else if (toCoordinate.X == 6)
            {
                top += 8;
            }
            else if (toCoordinate.X == 7)
            {
                top += 4;
            }

            if (toCoordinate.Y == 2)
            {
                left += 11;
            }
            else if (toCoordinate.Y == 3)
            {
                left += 22;
            }
            else if (toCoordinate.Y == 4)
            {
                left += 33;
            }
            else if (toCoordinate.Y == 5)
            {
                left += 44;
            }
            else if (toCoordinate.Y == 6)
            {
                left += 55;
            }
            else if (toCoordinate.Y == 7)
            {
                left += 66;
            }
            else if (toCoordinate.Y == 8)
            {
                left += 77;
            }

            if (figureToMove is King && figureToMove.Color == ConsoleColor.Black) this.hasTheBlackKingMoved = true;

            if (figureToMove is King && figureToMove.Color == ConsoleColor.White) this.hasTheWhiteKingMoved = true;

            if (figureToMove is Rook && figureToMove.Color == ConsoleColor.Black) this.hasTheBlackRookMoved = true;

            if (figureToMove is Rook && figureToMove.Color == ConsoleColor.White) this.hasTheWhiteRookMoved = true;

            if (this.CanPawnBePromoted(coordinate, toCoordinate, left, top)) return;

            if (this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1] != null) this.KillFigure(coordinate, toCoordinate);

            this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1] = figureToMove;

            figureToMove.HasMoved = true;

            figureToMove.Coordinates.X = toCoordinate.X;

            figureToMove.Coordinates.Y = toCoordinate.Y;

            this.board.Table[coordinate.X - 1, coordinate.Y - 1] = null;

            this.drawManager.UndoCharacter(toCoordinate);

            this.drawManager.UndoCharacter(toCoordinate);

            this.drawManager.UndoCharacter(coordinate);

            this.drawManager.UndoCharacter(coordinate);

            this.drawManager.PrintCharacter(figureToMove, new Coordinate(left, top));

            this.drawManager.PrintCharacter(figureToMove, new Coordinate(left, top));
        }

        private void KillFigure(Coordinate coordinate, Coordinate toCoordinate)
        {
            var figureToKill = this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1];

            if (figureToKill is King && figureToKill.Color == ConsoleColor.Black)
            {
                this.whitePlayer.Win = true;
            }
            else if (figureToKill is King && figureToKill.Color == ConsoleColor.White)
            {
                this.blackPlayer.Win = true;
            }

            if (figureToKill.Color == ConsoleColor.Black)
            {
                this.whitePlayer.OccupiedFigures.Add(this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1]);
            }
            else
            {
                this.blackPlayer.OccupiedFigures.Add(this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1]);
            }
        }

        private bool CheckingPosition(Coordinate coordinate, Coordinate toCoordinate)
        {
            var figureToMove = this.board.Table[coordinate.X - 1, coordinate.Y - 1];

            bool isItFitting = false;

            int x = 0;
            int y = 0;

            int maxX = Math.Max(coordinate.X, toCoordinate.X);

            int minX = Math.Min(coordinate.X, toCoordinate.X);

            int maxY = Math.Max(coordinate.Y, toCoordinate.Y);

            int minY = Math.Min(coordinate.Y, toCoordinate.Y);

            if (toCoordinate.Y > 8 || toCoordinate.X > 8)
            {
                isItFitting = false;
            }
            else if (figureToMove is King)
            {
                x = coordinate.X + 1;

                y = coordinate.Y;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X;

                y = coordinate.Y + 1;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X - 1;

                y = coordinate.Y;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X;

                y = coordinate.Y - 1;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X - 1;

                y = coordinate.Y - 1;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X + 1;

                y = coordinate.Y - 1;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X - 1;

                y = coordinate.Y + 1;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X + 1;

                y = coordinate.Y + 1;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;
            }
            else if (figureToMove is Bishop)
            {
                if (!this.CheckingDiagonalLinesForSameColoredFigures(minX, maxX, minY, maxY, coordinate, toCoordinate, figureToMove)) return false;

                isItFitting = this.boardColors.ColorBoard[coordinate.X - 1, coordinate.Y - 1] == this.boardColors.ColorBoard[toCoordinate.X - 1, toCoordinate.Y - 1]
                    && (coordinate.X < toCoordinate.X && coordinate.Y < toCoordinate.Y // + +
                    || coordinate.X < toCoordinate.X && coordinate.Y > toCoordinate.Y // + - 
                    || coordinate.X > toCoordinate.X && coordinate.Y > toCoordinate.Y // - -
                    || coordinate.X > toCoordinate.X && coordinate.Y < toCoordinate.Y); // - +
            }
            else if (figureToMove is Knight)
            {
                x = coordinate.X + 2;

                y = coordinate.Y + 1;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X + 2;

                y = coordinate.Y - 1;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X - 2;

                y = coordinate.Y + 1;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X - 2;

                y = coordinate.Y - 1;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X - 1;

                y = coordinate.Y + 2;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X - 1;

                y = coordinate.Y - 2;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X + 1;

                y = coordinate.Y - 2;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                //

                x = coordinate.X + 1;

                y = coordinate.Y + 2;

                isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                if (isItFitting) return isItFitting;
            }
            else if (figureToMove is Pawn)
            {
                if (!this.CheckingVerticalLinesForSameColoredFigures(minX, maxX, coordinate, toCoordinate, figureToMove)) return false;

                if (this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1] == null)
                {
                    x = coordinate.X + 1;

                    y = coordinate.Y;

                    isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                    if (isItFitting) return isItFitting;

                    x = coordinate.X - 1;

                    y = coordinate.Y;

                    isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                    if (isItFitting) return isItFitting;

                    //

                    if (!figureToMove.HasMoved)
                    {
                        x = coordinate.X + 2;

                        y = coordinate.Y;

                        isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                        if (isItFitting) return isItFitting;

                        x = coordinate.X - 2;

                        y = coordinate.Y;

                        isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                        if (isItFitting) return isItFitting;
                    }
                }

                if (this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1] != null)
                {
                    x = coordinate.X + 1;

                    y = coordinate.Y + 1;

                    isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                    if (isItFitting) return isItFitting;

                    //

                    x = coordinate.X + 1;

                    y = coordinate.Y - 1;

                    isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                    if (isItFitting) return isItFitting;

                    //

                    x = coordinate.X - 1;

                    y = coordinate.Y - 1;

                    isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                    if (isItFitting) return isItFitting;

                    //

                    x = coordinate.X - 1;

                    y = coordinate.Y + 1;

                    isItFitting = toCoordinate.X == x && toCoordinate.Y == y;

                    if (isItFitting) return isItFitting;
                }
            }
            else if (figureToMove is Queen)
            {
                if (coordinate.X - toCoordinate.X != 0 && coordinate.Y - toCoordinate.Y != 0)
                {
                    if (!this.CheckingDiagonalLinesForSameColoredFigures(minX, maxX, minY, maxY, coordinate, toCoordinate, figureToMove)) return false;
                }
                else if (coordinate.Y - toCoordinate.Y != 0)
                {
                    if (!this.CheckingHorizontalLinesForSameColoredFigures(minY, maxY, coordinate, toCoordinate, figureToMove)) return false;
                }
                else if (coordinate.X - toCoordinate.X != 0)
                {
                    if (!this.CheckingVerticalLinesForSameColoredFigures(minX, maxX, coordinate, toCoordinate, figureToMove)) return false;
                }

                x = coordinate.X;

                y = coordinate.Y;

                isItFitting = toCoordinate.X == x;

                if (isItFitting) return isItFitting;

                isItFitting = toCoordinate.Y == y;

                if (isItFitting) return isItFitting;

                isItFitting = this.boardColors.ColorBoard[coordinate.X - 1, coordinate.Y - 1] == this.boardColors.ColorBoard[toCoordinate.X - 1, toCoordinate.Y - 1]
                    && (coordinate.X < toCoordinate.X && coordinate.Y < toCoordinate.Y // + +
                    || coordinate.X < toCoordinate.X && coordinate.Y > toCoordinate.Y // + - 
                    || coordinate.X > toCoordinate.X && coordinate.Y > toCoordinate.Y // - -
                    || coordinate.X > toCoordinate.X && coordinate.Y < toCoordinate.Y); // - +

                if (isItFitting) return isItFitting;
            }
            else if (figureToMove is Rook)
            {
                if (coordinate.X - toCoordinate.X != 0)
                {
                    if (!this.CheckingVerticalLinesForSameColoredFigures(minX, maxX, coordinate, toCoordinate, figureToMove)) return false;
                }
                else if (coordinate.Y - toCoordinate.Y != 0)
                {
                    if (!this.CheckingHorizontalLinesForSameColoredFigures(minY, maxY, coordinate, toCoordinate, figureToMove)) return false;
                }

                x = coordinate.X;

                y = coordinate.Y;

                isItFitting = toCoordinate.X == x;

                if (isItFitting) return isItFitting;

                isItFitting = toCoordinate.Y == y;

                if (isItFitting) return isItFitting;
            }

            return isItFitting;
        }

        private bool IsThereSameColorFigureInFront(Coordinate coordinate, Coordinate toCoordinate)
        {
            bool isThereASameColorFigure = true;

            if (this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1] != null)
            {
                isThereASameColorFigure = this.board.Table[coordinate.X - 1, coordinate.Y - 1].Color != this.board.Table[toCoordinate.X - 1, toCoordinate.Y - 1].Color;
            }

            return isThereASameColorFigure;
        }
    }
}