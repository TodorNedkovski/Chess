using RealChess.Contracts;
using RealChess.Coordinates;
using RealChess.Objects;
using System;

namespace RealChess.Table
{
    public class Board
    {
        public Board()
        {
            this.Table = new IFigure[,]
            {
                {new Rook(new Coordinate(1, 1), ConsoleColor.White), new Knight(new Coordinate(1, 2), ConsoleColor.White), new Bishop(new Coordinate(1, 3), ConsoleColor.White), new Queen(new Coordinate(1, 4)
                , ConsoleColor.White), new King(new Coordinate(1, 5), ConsoleColor.White), new Bishop(new Coordinate(1, 6), ConsoleColor.White), new Knight(new Coordinate(1, 7), ConsoleColor.White), new Rook(new Coordinate(1, 8), ConsoleColor.White)},
                {new Pawn(new Coordinate(2, 1), ConsoleColor.White), new Pawn(new Coordinate(2, 2), ConsoleColor.White), new Pawn(new Coordinate(2, 3), ConsoleColor.White), new Pawn(new Coordinate(2, 4)
                , ConsoleColor.White), new Pawn(new Coordinate(2, 5), ConsoleColor.White), new Pawn(new Coordinate(2, 6), ConsoleColor.White), new Pawn(new Coordinate(2, 7), ConsoleColor.White), new Pawn(new Coordinate(2, 8), ConsoleColor.White)},
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                {new Pawn(new Coordinate(7, 1), ConsoleColor.Black), new Pawn(new Coordinate(7, 2), ConsoleColor.Black), new Pawn(new Coordinate(7, 3), ConsoleColor.Black), new Pawn(new Coordinate(7, 4)
                , ConsoleColor.Black), new Pawn(new Coordinate(7, 5), ConsoleColor.Black), new Pawn(new Coordinate(7, 6), ConsoleColor.Black), new Pawn(new Coordinate(7, 7), ConsoleColor.Black), new Pawn(new Coordinate(7, 8), ConsoleColor.Black)},
                {new Rook(new Coordinate(8, 1), ConsoleColor.Black), new Knight(new Coordinate(8, 2), ConsoleColor.Black), new Bishop(new Coordinate(8, 3), ConsoleColor.Black), new Queen(new Coordinate(8, 4)
                , ConsoleColor.Black), new King(new Coordinate(8, 5), ConsoleColor.Black), new Bishop(new Coordinate(8, 6), ConsoleColor.Black), new Knight(new Coordinate(8, 7), ConsoleColor.Black), new Rook(new Coordinate(8, 8), ConsoleColor.Black)},
            };
        }

        public IFigure[,] Table { get; set; }
    }
}
