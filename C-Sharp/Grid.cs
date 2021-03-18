using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MazeGenerator
{
    public class Grid
    {
        int width;
        int height;

        public Cell[,] Cells;

        public Grid(int w, int h)
        {
            this.width = w;
            this.height = h;
            this.Cells = new Cell[h, w];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    this.Cells[row, col] = new Cell(row, col);
                }
            }
        }

        private Cell GetCell(int col, int row)
        {
            if (col < 0 || row < 0 || col > (this.width - 1) || row > (this.height - 1))
            {
                return null;
            }

            return this.Cells[row, col];
        }

        private List<Cell> GetUnvisitedNeighbors(int col, int row)
        {
            List<Cell> neighbors = new List<Cell>();

            Cell top = this.GetCell(col, row - 1);
            Cell right = this.GetCell(col + 1, row);
            Cell bottom = this.GetCell(col, row + 1);
            Cell left = this.GetCell(col - 1, row);

            if ((top != null) && !top.Visited)
            {
                neighbors.Add(top);
            }

            if ((right != null) && !right.Visited)
            {
                neighbors.Add(right);
            }

            if ((bottom != null) && !bottom.Visited)
            {
                neighbors.Add(bottom);
            }

            if ((left != null) && !left.Visited)
            {
                neighbors.Add(left);
            }

            return neighbors;
        }

        public void RecursiveBacktrackingGenerator(Cell current)
        {
            Thread.Sleep(100);

            current.Visited = true;
            this.Draw(current);

            List<Cell> neighbors = this.GetUnvisitedNeighbors(current.Col, current.Row);

            neighbors.Shuffle();
            foreach (Cell next in neighbors)
            {
                if (!next.Visited)
                {
                    this.RemoveWalls(current, next);

                    // Recurse with next as the new current cell.
                    this.RecursiveBacktrackingGenerator(next);
                    this.Draw(current);
                }
            }

            Thread.Sleep(100);
        }

        private void RemoveWalls(Cell current, Cell next)
        {
            int x = current.Col - next.Col;
            int y = current.Row - next.Row;

            if (x == 1)
            {
                // Current cell is to the right of the next cell,
                current.Walls[Direction.Left] = false;
                next.Walls[Direction.Right] = false;
            }
            else if (x == -1)
            {
                // Current cell is to the left of the next cell,
                current.Walls[Direction.Right] = false;
                next.Walls[Direction.Left] = false;
            }

            if (y == 1)
            {
                // Current cell is below of the next cell,
                current.Walls[Direction.Above] = false;
                next.Walls[Direction.Below] = false;
            }
            else if (y == -1)
            {
                // Current cell is above of the next cell,
                current.Walls[Direction.Below] = false;
                next.Walls[Direction.Above] = false;
            }
        }

        public void Draw(Cell currentCell)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(RenderHorizontalWalls(0, true));

            for (int row = 0; row < height; row++)
            {
                Console.WriteLine(RenderRow(row, currentCell));
                Console.WriteLine(RenderHorizontalWalls(row, false));
            }
        }

        private string RenderRow(int row, Cell currentCell)
        {
            StringBuilder sb = new StringBuilder(Cells[row, 0].Walls[(int)Direction.Left] ? "|" : " ");

            for (int col = 0; col < width; col++)
            {
                char currCell = (row == currentCell.Row && (col == currentCell.Col)) ? '@' : (this.Cells[row, col].Visited ? '.' : ' ');
                char rightWall = Cells[row, col].Walls[(int)Direction.Right] ? '|' : ' ';
                sb.Append($"{currCell}{rightWall}");
            }

            return sb.ToString();
        }

        private string RenderHorizontalWalls(int row, bool upperWall)
        {
            StringBuilder sb = new StringBuilder("+");

            for (int col = 0; col < width; col++)
            {
                char wall;

                if (upperWall)
                {
                    wall = Cells[row, col].Walls[(int)Direction.Above] ? '-' : ' ';
                }
                else
                {
                    wall = Cells[row, col].Walls[(int)Direction.Below] ? '-' : ' ';
                }

                sb.Append($"{wall}+");
            }

            return sb.ToString();
        }
    }
}
