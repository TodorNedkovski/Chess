using System;

namespace RealChess.Table
{
    public class BoardColors
    {
        public BoardColors()
        {
            ColorBoard = new ConsoleColor[,]
                {
                    {ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray},

                    {ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray},

                    {ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray},

                    {ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray},

                    {ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray},

                    {ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray},

                    {ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray},

                    {ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.DarkGray},
                };
        }

        public ConsoleColor[,] ColorBoard { get; set; }
    }
}