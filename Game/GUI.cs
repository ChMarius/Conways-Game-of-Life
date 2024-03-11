using System.Text;
using GameOfLife;

namespace Conways_Game_of_Life
{
    public static class GUI
    {
        public static string DisplayGrid(Grid Grid)
        {
            StringBuilder output = new StringBuilder();
            for (int Y = 0; Y < Grid.Columns; Y++) 
            {
                for(int X = 0; X < Grid.Rows; X++)
                {
                    if (Grid.Cells[X,Y].CellState)
                    {
                        output.Append("██");
                    }
                    else
                    {
                        output.Append("░░");
                    }
                }
                output.Append('\n');
            }

            return output.ToString();
        }

        //This function Updates the whole scene and outputs the result to the screen
        //In addition it also returns any input the user might have provided
        public static string UpdateScene(ref Menu[] Scene, ref int CurrentMenu)
        {
            string Output = "";
            //We are making a copy of all of the provided variables so that we can adjust them as needed and
            //apply the differences to the original variables only once we are finished
            int NextMenu = CurrentMenu;
            
            //We check if the current menu we are looking at is of type "Selectable" or "Prompt"
            if (Scene[CurrentMenu].GetType().Name == "Selectable")
            {
                //We copy the current menu and update it based on user input
                Selectable TempMenu = (Selectable)Scene[CurrentMenu];
                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.UpArrow:
                        TempMenu.UpdateMenuSelection(GUI.Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        TempMenu.UpdateMenuSelection(GUI.Direction.Down);
                        break;
                    case ConsoleKey.Enter:
                        //If the Enter key is pressed the currently selected option is chosen
                        //We store the index of the next menu that is to be displayed
                        NextMenu = TempMenu.Links[TempMenu.CurrentSelection - 1];
                        //Here we check for special conditions
                        if (NextMenu == -1)
                        {
                            //We Quit the program
                            Quit();
                            Environment.Exit(0);
                        }
                        if(CurrentMenu == 4 && TempMenu.CurrentSelection == 1)
                        {
                            TempMenu.PrintMenu();
                            return "Advance";
                        }
                        if (CurrentMenu == 4 && TempMenu.CurrentSelection == 2)
                        {
                            TempMenu.PrintMenu();
                            return "Save";
                        }
                        break;
                    default:
                        break;
                }
                //We apply our chages to the original scene and current menu variables
                Scene[CurrentMenu] = TempMenu;
                CurrentMenu = NextMenu;
            }
            else if (Scene[CurrentMenu].GetType().Name == "Prompt")
            {
                //We copy the menu and get the validated input from the user
                //We do this because the scene stores the menus as the abstract type "Menu"
                //As such we can not be sure if the current menu is of type "Prompt" unless we try parse it
                Prompt TempMenu = (Prompt)Scene[CurrentMenu];
                
                //We save the output
                Output = TempMenu.ReadInput();
                CurrentMenu = TempMenu.Links[0];
            }

            //We print the current menu to the screen and return the player input if applicable
            Scene[CurrentMenu].PrintMenu();
            return Output;
        }

        private static void Quit()
        {
            Console.Clear();
            Console.WriteLine("Thank you for playing");
            Console.ReadKey(true);
        }

        public enum Direction
        {
            Up,
            Down
        }
    }
}