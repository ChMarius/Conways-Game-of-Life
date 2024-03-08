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
            Console.WriteLine(grid.Cells);
            grid.UpdateGrid(grid.Cells);
            grid.SetGrid();
            grid.SetGeneration(grid.Generation);
        }
        public void StoreGrid()
        {

        }


    }
}