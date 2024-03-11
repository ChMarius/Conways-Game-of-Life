using Conways_Game_of_Life;
using System;
using System.Transactions;

namespace GameOfLife
{
    public class Program
    {
        public static void Main()
        {
            //Local variables that store information that will be used later to generate the grid
            int Rows = 0;
            int Columns = 0;
            string filePath = "";
            JsonStorage json = new();
            Grid grid = new(Rows, Columns);

            //The "Menu System" stores all of the menus in a so called "Scene" which is just an array of the type Menu. Here we are storing which menu we are currently on
            int CurrentMenu = 0;

            //Here we initilaize all of the different Menus

            //There are two menu types "Selectable" and "Prompt".
            //They both inherit the "Menu" abstract class and are differentianted by the Selectable class' ability to select from a range of different options

            //Here we define the different menu options, the first one of which being the title of the menu
            string[] MainMenuOptions = new string[]
            {
                "Conway's Game of Life",
                "1. Create a new random grid",
                "2. Load grid from JSON file",
                "3. Exit"
            };

            //Here we define where each menu option is going to lead to after being selected
            int[] MainMenuLinks = new int[]
            {
                1, 3, -1
            };

            Menu MainMenu = new Selectable(MainMenuOptions, MainMenuLinks);

            //The Prompt type requires a lot less information so they can be intialized in a single line with an (Prompt, Next menu location in scene)
            Menu NewGridRowsPrompt = new Prompt("Enter the number of rows for the grid (4-100): ", 2);
            Menu NewGridColumnsPrompt = new Prompt("Enter the number of columns for the grid (4-100): ", 4);
            Menu JSONFilePrompt = new Prompt("Please provide a path for a JSON file to load the grid from: ", 4);

            //This is where we create the "Scene" and add all of the Menus to it
            Menu[] Scene = new Menu[] {MainMenu, NewGridRowsPrompt, NewGridColumnsPrompt, JSONFilePrompt};

            //We start by printing the first menu from the scene to the screen
            Scene[0].PrintMenu();

            bool continueDisplay = true;

            while (continueDisplay)
            {
                //This code gets and varifies information from the interface/user
                switch (CurrentMenu)
                {
                    case 1:
                        while (!int.TryParse(GUI.UpdateScene(ref Scene, ref CurrentMenu), out Rows) || (Rows < 4 || Rows > 100))
                        {
                            Console.WriteLine("The text you inputed was not a valid number. Please try again:");
                        }

                        //we need to update the Current menu variable here
                        //because we only want to transition to the next menu after we have varified that the information provided by the user is correct
                        grid = new(Rows, Columns);
                        grid.Randomize();
                        CurrentMenu = Scene[1].Links[0];
                        break;
                    case 2:
                        while (!int.TryParse(GUI.UpdateScene(ref Scene, ref CurrentMenu), out Columns) || (Columns < 4 || Columns > 100))
                        {
                            Console.WriteLine("The text you inputed was not a valid number. Please try again:");
                        }
                        grid = new(Rows, Columns);
                        grid.Randomize();
                        CurrentMenu = Scene[2].Links[0];
                        break;
                    case 3:
                        filePath = GUI.UpdateScene(ref Scene, ref CurrentMenu);
                        grid = json.LoadGrid(filePath); // TODO: check if filePath is valid
                        CurrentMenu = Scene[3].Links[0];
                        break;
                    case 4:
                        //Here we trigger the actual simulation to start by checking the last menu has been passed
                        StartSim(grid);
                        break;
                    default:
                        //We want to check if the current menu variable is less than the amount of menus in the scene,
                        //because since we are using it as a trigger it can become larger or smaller than the amount of menus in the scene
                        if (CurrentMenu < Scene.Length)
                        {
                            GUI.UpdateScene(ref Scene, ref CurrentMenu);
                        }
                        break;
                }

                if (CurrentMenu < Scene.Length)
                {
                    //The menu needs to be updated again since the current menu value has been changed
                    Scene[CurrentMenu].PrintMenu();
                }
            }
        }

        private static void StartSim(Grid grid)
        {
            JsonStorage json = new();
            bool advance = false;
            bool continueDisplay = true;
            while (continueDisplay) 
            {
                Console.Clear();

                Console.WriteLine("Iteration " + grid.Generation + '\n');
                Console.WriteLine("Press 'N' to advance to the next generation.");
                Console.WriteLine("Press 'S' to save the current grid state to a file.");
                Console.WriteLine("Press 'X' to exit the simulation.\n");
                
                for (int i = 0; i < grid.Columns; i++)
                {
                    for (int j = 0; j < grid.Rows; j++)
                        Console.Write(grid.Cells[i,j].GetCellState() ? "O" : ".");
                    Console.Write('\n');
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.N:
                        advance = true;
                        break;
                    case ConsoleKey.S:
                        json.StoreGrid(grid);
                        break;
                    case ConsoleKey.X:
                        continueDisplay = false;
                        break;
                    default:
                        break;
                }

                if (advance)
                    grid = AutomatonSimulator.ApplyRules(grid);
                advance = false;
            }

            // TODO: go back to main menu
            Console.Clear();
            Console.WriteLine("Thanks for playing!");
            Environment.Exit(0);
        }
    }
}