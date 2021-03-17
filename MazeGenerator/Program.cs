using System;

namespace MazeGenerator
{
    class Program
    {
        static int MaxRows = 5;
        static int MaxCols = 5;

        public static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                if (!int.TryParse(args[0], out int numRows))
                {
                    Console.WriteLine($"Error {args[0]} is not a valid number of rows");
                    return;
                }

                if (!int.TryParse(args[1], out int numCols))
                {
                    Console.WriteLine($"Error {args[1]} is not a valid number of columns");
                    return;
                }

                MaxRows = numRows;
                MaxCols = numCols;
            }

            Grid grid = new Grid(MaxCols, MaxRows);

            Random rnd = new Random();

            Console.Clear();
            grid.RecursiveBacktrackingGenerator(grid.Cells[0, 0]);
            Console.SetCursorPosition(0, (MaxRows * 2) + 2);
        }
    }
}
