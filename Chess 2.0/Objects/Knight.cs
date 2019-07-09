using RealChess.Coordinates;
using System;

namespace RealChess.Objects
{
    public class Knight : Figure
    {
        public Knight(Coordinate coordinates, ConsoleColor color)
            : base(coordinates, color)
        {
            this.Design = new bool[,]
            {
                {false, false, false, false, false, false, false, false, false, false, false },

                {false, false, true, true, true, true, false, false, false, false, false },

                {false, false, false, false, true, true, true, false, false, false, false  },

                {false, false, false, true, true, true, true, true, false, false, false  },
            };
        }

        public override bool[,] Design { get; }
    }
}