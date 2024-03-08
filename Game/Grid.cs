namespace GameOfLife
{
    public class Grid : IGrid
    {
        public List<List<Cell>> Cells { get; private set; } = new();
        public int Generation { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public void SetGrid()
        {
            for (int i = 0; i < Columns; i++)
                for (int j = 0; j < Rows; j++)
                   if(Cells[i][j].CellState) { Console.Write("O"); }
                   else { Console.Write("."); }
        } 
        
        public void RandomizeGrid()
        {
            Random rand = new();
            bool randomState = rand.NextDouble() >= 0.5;
            for (int i = 0; i < Columns; i++)
                for (int j = 0; j < Rows; j++)
                    Cells[i][j].SetCellState(randomState);
        }
        public void SetGeneration(int generation)
        {
            Generation = generation;
        }
        public void UpdateGrid(List<List<Cell>> newGridState)
        {
            SetGeneration(Generation + 1);
            Cells = newGridState;
        }
        public int GetLiveCellNeighborCount(int x, int y)
        {
            if (x < 0)
                throw new ArgumentOutOfRangeException("x value cannot be less than 0.");
            else if (y < 0)
                throw new ArgumentOutOfRangeException("y value cannot be less than 0.");
            else if (x >= Columns)
                throw new ArgumentOutOfRangeException("x value cannot be equal to or more than total amount of columns in the grid.");
            else if (y >= Rows)
                throw new ArgumentOutOfRangeException("y value cannot be equal to or more than total amount of rows in the grid.");

            int liveNeighbors = 0;

            bool topEdge = (y == 0);
            bool leftEdge = (x == 0);
            bool rightEdge = (x >= Columns);
            bool bottomEdge = (y >= Rows);

            // Start at east, then iterate over all neighbors clockwise.
            if (!rightEdge && Cells[x+1][y].GetCellState()) // east
                liveNeighbors++;
            if (!rightEdge && !bottomEdge && Cells[x+1][y+1].GetCellState()) // southeast
                liveNeighbors++;
            if (!bottomEdge && Cells[x][y+1].GetCellState()) // south
                liveNeighbors++;
            if (!bottomEdge && !leftEdge && Cells[x-1][y+1].GetCellState()) // southwest
                liveNeighbors++;
            if (!leftEdge && Cells[x-1][y].GetCellState()) // west
                liveNeighbors++;
            if (!leftEdge && !topEdge && Cells[x-1][y-1].GetCellState()) // northwest
                liveNeighbors++;
            if (!topEdge && Cells[x][y-1].GetCellState()) // north
                liveNeighbors++;
            if (!topEdge && !rightEdge && Cells[x+1][y-1].GetCellState()) // northeast
                liveNeighbors++;
            
            return liveNeighbors;
        }
    }
}