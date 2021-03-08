using System;

namespace MazeGenerator
{
    class MainClass
    {
        static int MaxRows = 5;
        static int MaxCols = 5;

        public static void Main(string[] args)
        {
            Grid grid = new Grid(MaxRows, MaxCols);

            Random rnd = new Random();

            Console.Clear();
            grid.RecursiveBacktrackingGenerator(grid.Cells[0, 0]);
            Console.SetCursorPosition(0, MaxRows + 2);
        }
    }
}
