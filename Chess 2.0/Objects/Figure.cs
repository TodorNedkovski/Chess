using System;
using RealChess.Contracts;
using RealChess.Coordinates;

namespace RealChess.Objects
{
    public abstract class Figure : IFigure
    {
        public Figure(Coordinate coordinates, ConsoleColor color)
        {
            this.Coordinates = coordinates;
            this.Color = color;
        }

        public Coordinate Coordinates { get; }

        public bool HasMoved { get; set; }

        public virtual bool[,] Design { get; }

        public ConsoleColor Color { get; }
    }
}