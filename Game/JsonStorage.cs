using System.Text.Json;

namespace GameOfLife
{
    public struct SerializableGrid // move this somewhere else or refactor, please - Dan
    {
        public int generation { get; private set; }
        public int rows { get; private set; }
        public int columns { get; private set; }
        public bool[][] grid { get; private set; }
        public SerializableGrid(int inputRows, int inputColumns, bool[][] inputGrid, int inputGen)
        {
            columns = inputColumns;
            rows = inputRows;
            grid = inputGrid;
            generation = inputGen;
        }
    }
    public class JsonStorage : IStorage
    {
        public static string filePath;
        public void LoadGrid()
        {
            var gridJson = File.ReadAllText(filePath);
            Grid grid = JsonSerializer.Deserialize<Grid>(gridJson);
            
        }
        public void StoreGrid()
        {
            int rows = 5;
            int columns = 4;
            int generation = 5;

            Grid grid = new(rows,columns);
            grid.RandomizeGrid();

            // Convert 2D array of Cell objects into serializable jagged array of booleans.
            bool[][] gridCells = new bool[columns][];
            for (int i = 0; i < columns; i++)
            {
                bool[] gridCellRow = new bool[rows];
                for (int j = 0; j < rows; j++)
                    gridCellRow[j] = grid.Cells[i,j].GetCellState();
                gridCells[i] = gridCellRow;
            }

            SerializableGrid serializableGrid = new(rows,columns,gridCells,generation);

            string fileName = "Grids\\grid.json";

            string jsonString = JsonSerializer.Serialize(serializableGrid);

            File.WriteAllText(fileName,jsonString);
        }
    }
}