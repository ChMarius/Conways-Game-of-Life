using System.Text.Json;

namespace GameOfLife
{
    public class JsonStorage : IStorage
    {
        // The Cells array in Grid cannot be serialized to JSON directly, so instead we use the struct below.
        public struct JsonGrid(Grid inputGrid)
        {
            public int Generation { get; set; } = inputGrid.Generation;
            public int Rows { get; set; } = inputGrid.Rows;
            public int Columns { get; set; } = inputGrid.Columns;
            public bool[][] Grid { get; set; }
        }
        public Grid LoadGrid(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            JsonGrid jsonGrid = JsonSerializer.Deserialize<JsonGrid>(jsonString)!;
            Grid grid = new(jsonGrid.Rows,jsonGrid.Columns);

            for (int i = 0; i < jsonGrid.Columns; i++)
                for (int j = 0; j < jsonGrid.Rows; j++)
                    grid.Cells[i,j].SetCellState(jsonGrid.Grid[i][j]);
            
            return grid;
        }
        public void StoreGrid(Grid grid)
        {
            // Convert 2D array of Cell objects into serializable array of arrays of booleans.
            bool[][] jsonCells = new bool[grid.Columns][];
            for (int i = 0; i < grid.Columns; i++)
            {
                bool[] jsonGridRow = new bool[grid.Rows];
                for (int j = 0; j < grid.Rows; j++)
                    jsonGridRow[j] = grid.Cells[i,j].GetCellState();
                jsonCells[i] = jsonGridRow;
            }

            JsonGrid jsonGrid = new(grid)
            {
                Grid = jsonCells
            };

            // TODO: generate file names dynamically
            string fileName = "Grids\\grid.json";
            string jsonString = JsonSerializer.Serialize(jsonGrid);
            File.WriteAllText(fileName,jsonString);
        }
    }
}