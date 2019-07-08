using RealChess.Coordinates;
using System;

namespace RealChess.Objects
{
    public class Queen : Figure
    {
        public Queen(Coordinate coordinates, ConsoleColor color)
            : base(coordinates, color)
        {
            this.Design = new bool[,]
            {
                {false, true, false, true, false, true, false, true, false, true, false },

                {false, false, true, false, true, false, true, false, true, false, false },

                {false, true, false, true, false, true, false, true, false, true, false  },

                {false, false, true, false, true, false, true, false, true, false, false  },
            };
        }

        public override bool[,] Design { get; }
    }
}