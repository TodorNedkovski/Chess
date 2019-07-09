using RealChess.Coordinates;
using System;

namespace RealChess.Contracts
{
    public interface IFigure
    {
        Coordinate Coordinates { get; }

        ConsoleColor Color { get; }

        bool HasMoved { get; set; }

        bool[,] Design { get; }
    }
}