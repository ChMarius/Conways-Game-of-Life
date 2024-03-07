namespace GameOfLife
{
    public class Grid : IGrid
    {
        public List<List<Cell>> Cells { get; private set; } = new();
        public int Generation { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Grid(int rows, int columns, bool randomize)
        {
            Rows = rows;
            Columns = columns;
            if (randomize)
                RandomizeGrid();
        }
        private void RandomizeGrid()
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

        // TODO: logic for checking neighbors of a cell that accounts for cells at the edge of a grid
    }
}