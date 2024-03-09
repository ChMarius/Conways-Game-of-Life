using System;

namespace GameOfLife
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool continueDisplay = true;

            while (continueDisplay)
            {
                Console.WriteLine("\nConway's Game of Life");
                Console.WriteLine("1. Create a new random grid");
                Console.WriteLine("2. Load grid from JSON file");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                var option = Console.ReadLine();
                JsonStorage json=new();
                switch (option)
                {
                    case "1":
                        Console.WriteLine("\nNo");
                        int rows = 100;
                        int columns = 100;
                        int generation = 0;

                        Grid grid = new(rows,columns);
                        grid.RandomizeGrid();
                        grid.SetGeneration(generation);
                        //json.StoreGrid(grid);
                        // ask user for rows (4-100)
                        // ask user for columns (4-100)
                        // init grid

                        // temp code for testing automaton sim, ctrl+c to quit
                        while (true)
                        {
                            for (int i = 0; i < grid.Columns; i++)
                            {
                                for (int j = 0; j < grid.Rows; j++)
                                    Console.Write(grid.Cells[i,j].GetCellState() ? "1" : "0");
                                Console.Write('\n');
                            }
                            Thread.Sleep(1000);
                            Console.Clear();
                            grid = AutomatonSimulator.ApplyRules(grid);
                        }

                        break;
                    case "2":
                        //JsonStorage.filePath ="Grids\\grid.json";
                        string filePath ="Grids\\grid.json";
                        json.LoadGrid(filePath);
                        break;
                    case "3":
                        Console.WriteLine("\nExiting...");
                        continueDisplay = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }
            }
        }
    }
}