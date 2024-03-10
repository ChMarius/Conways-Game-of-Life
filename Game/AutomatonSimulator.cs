namespace GameOfLife
{
    public static class AutomatonSimulator
    {
        public static Grid ApplyRules(Grid grid)
        {
            Grid newGrid = grid;

            for(int i = 0; i < grid.Columns; i++)
            {
                for(int j = 0; j < grid.Rows; j++)
                {
                    int neighbors = grid.GetLiveCellNeighborCount(i,j);
                    bool cellState = grid.Cells[i, j].GetCellState();

                    bool newState = neighbors == 3 || neighbors == 2 && cellState;
                    newGrid.Cells[i, j].SetCellState(newState);
                }  
            }

            grid.Update(newGrid.Cells);
            return grid;
        }
    }
}