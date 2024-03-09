using System.Data;

namespace GameOfLife
{

    public static class AutomatonSimulator
    {
        
        static void Rules(Grid grid)
        {
            Grid newGrid = grid;

            for(int i = 0; i < grid.Columns; i++)
            {
                for(int j = 0; j < grid.Rows; j++)
                {
                    int neighbors = grid.GetLiveCellNeighborCount(i,j);
                    bool cellState = grid.Cells[i, j].GetCellState();

                    if(cellState)
                    {
                        if(neighbors != 2 && neighbors != 3)
                            newGrid.Cells[i, j].SetCellState(false);
                    }
                    else
                    {
                        if(neighbors == 3)
                            newGrid.Cells[i, j].SetCellState(true);
                    }
                }

                grid.UpdateGrid(newGrid.Cells);
            }
             
        }
    }
}