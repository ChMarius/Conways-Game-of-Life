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
            Grid MainGrid = new Grid(4,4);
            string JSONPath = "";

            //The "Menu System" stores all of the menus in a so called "Scene" which is just an array of the type Menu. Here we are storing which menu we are currently on
            int CurrentMenu = 0;

            //Here we initilaize all of the different Menus

            //There are two menu types "Selectable" and "Prompt".
            //They both inherit the "Menu" abstract class and are differentianted by the Selectable class' ability to select from a range of different options

            //Here we define the different menu options, the first one of which being the title of the menu
            string[] MainMenuOptions = new string[]
            {
                "Conway's Game of Life",
                "Create a new random grid",
                "Load grid from JSON file",
                "Exit"
            };

            //Here we define where each menu option is going to lead to after being selected
            int[] MainMenuLinks = new int[]
            {
                1, 3, -1
            };

            Menu MainMenu = new Selectable(MainMenuOptions, MainMenuLinks);

            string[] SimulationMenuOptions = new string[]
            {
                "Options",
                "Advance simulation",
                "Save",
                "Exit"
            };

            //Here we define where each menu option is going to lead to after being selected
            int[] SimulationMenuLinks = new int[]
            {
                4, 4, -1
            };

            Menu SimulationMenu = new Selectable(SimulationMenuOptions, SimulationMenuLinks);

            //The Prompt type requires a lot less information so they can be intialized in a single line with an (Prompt, Next menu location in scene)
            Menu NewGridRowsPrompt = new Prompt("Enter the number of rows for the grid (4-100): ", 2);
            Menu NewGridColumnsPrompt = new Prompt("Enter the number of columns for the grid (4-100): ", 4);
            Menu JSONLoadFilePrompt = new Prompt("Please provide a path for a JSON file to load the grid from: ", 4);

            //This is where we create the "Scene" and add all of the Menus to it
            Menu[] Scene = new Menu[] {MainMenu, NewGridRowsPrompt, NewGridColumnsPrompt, JSONLoadFilePrompt, SimulationMenu};

            //We start by printing the first menu from the scene to the screen
            Scene[0].PrintMenu();

            bool IsRunning = true;

            while (IsRunning)
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
                        CurrentMenu = Scene[1].Links[0];
                        break;
                    case 2:
                        while (!int.TryParse(GUI.UpdateScene(ref Scene, ref CurrentMenu), out Columns) || (Columns < 4 || Columns > 100))
                        {
                            Console.WriteLine("The text you inputed was not a valid number. Please try again:");
                        }
                        CurrentMenu = Scene[2].Links[0];

                        //We update the grid and randomize it
                        MainGrid.InitGrid(Rows, Columns);
                        MainGrid.Randomize();

                        break;
                    case 3:
                        //We get a file name from the user
                        //And test if the file can be found
                        //If not prompts the user again
                        while (!JsonStorage.LoadGrid(JSONPath, out MainGrid))
                        {
                            int temp = 3;
                            JSONPath = GUI.UpdateScene(ref Scene, ref temp);
                            Console.WriteLine("The file name you entered could not be found. Please try again:");
                            CurrentMenu = 3;
                        }
                        CurrentMenu = Scene[3].Links[0];
                        break;

                    case 4:

                        Scene[CurrentMenu].Options[0] = GUI.DisplayGrid(MainGrid);
                        Scene[CurrentMenu].PrintMenu();
                        string debug = GUI.UpdateScene(ref Scene, ref CurrentMenu);
                        switch (debug)
                        {
                            case "Advance":
                                MainGrid = AutomatonSimulator.ApplyRules(MainGrid);
                                break;
                            case "Save":
                                JsonStorage.StoreGrid(MainGrid);
                                break;
                        }
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
            }
        }
    }
}