using System.Collections.Generic;

namespace RealChess.Contracts
{
    public interface IPlayer
    {
        bool Win { get; set; }

        List<IFigure> OccupiedFigures { get; set; }
    }
}