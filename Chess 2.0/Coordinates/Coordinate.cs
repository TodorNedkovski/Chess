namespace RealChess.Coordinates
{
    public class Coordinate
    {
        public Coordinate(int x ,int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}