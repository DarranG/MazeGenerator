using System;
namespace MazeGenerator
{
    public class Cell
    {
        public int Row { get; }
        public int Col { get; }

        public bool[] Walls = { true, true, true, true };
        public bool Visited { get; set; }

        public Cell(int row, int col)
        {
            this.Row = row;
            this.Col = col;
            this.Visited = false;
        }
    }
}
