using System.Collections.Generic;
using RealChess.Contracts;

namespace RealChess.Objects.Players
{
    public class Player : IPlayer
    {
        public Player()
        {
            this.OccupiedFigures = new List<IFigure>();
        }

        public bool Win { get; set; }

        public List<IFigure> OccupiedFigures { get; set; }
    }
}
