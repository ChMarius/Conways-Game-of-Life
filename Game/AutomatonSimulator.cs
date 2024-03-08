using System.Data;

namespace GameOfLife
{

    public class AutomatonSimulator
    {
        public Grid CurrentGrid;
    


        static void Updategrid()
        {


            Grid grid = new(Rows, Columns, true);
            Grid newGrid = grid;

            for(int i = 0; i < grid.Columns; i++)
            {
                for(int j = 0; j < grid.Rows; j++)
                {
                    int neighbors = grid.GetLiveCellNeighborCount(i,j);
                    bool cellState = grid.Cells[i][j].GetCellState();
                }
            }
        }
    }
}