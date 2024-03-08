using System.Text.Json;

namespace GameOfLife
{
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
            int rows=5;
            int columns=4;
            Grid grid = new(rows,columns);
            grid.RandomizeGrid();
            string fileName = "Grids\\grid2.json";
            string jsonString = JsonSerializer.Serialize(grid);
            File.WriteAllText(fileName,jsonString);
        }


    }
}