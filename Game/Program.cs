using Conways_Game_of_Life;
using System;

namespace GameOfLife
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] MenuOptions = new string[]
            {
                "Conway's Game of Life",
                "1. Create a new random grid",
                "2. Load grid from JSON file",
                "3. Exit"
            };

            Menu MainMenu = new Menu(MenuOptions);

            MainMenu.PrintMenu();

            bool continueDisplay = true;

            while (continueDisplay)
            {
                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        MainMenu.MoveCursor(Menu.Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        MainMenu.MoveCursor(Menu.Direction.Down);
                        break;
                    default:
                        Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
                        Console.Write(' ');
                        break;
                }
            }
        }
    }
}