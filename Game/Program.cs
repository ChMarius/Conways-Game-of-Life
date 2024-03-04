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

                switch (option)
                {
                    case "1":
                        Console.WriteLine("\nNo");
                        break;
                    case "2":
                        Console.WriteLine("\nHell nah");
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