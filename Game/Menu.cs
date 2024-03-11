using System.Text;
using Conways_Game_of_Life;
using GameOfLife;

namespace Conways_Game_of_Life
{
    //We start by creating an abstract Menu
    public abstract class Menu
    {
        //The value used to store the different elements the menu has. The first one is always the prompt/title
        protected string[] options;

        public string[] Options
        {
            get { return options; }
            set { options = value; }
        }

        //The values used to determine what the next menu will be
        protected int[] links;

        public int[] Links
        {
            get { return links; }
            set { links = value; }
        }

        //The whole menu condensed into a single string with new lines
        protected string text;
        //The width of the menu
        protected int width;

        //The function that "renders" the menu
        //It is abstract because Prompt menus and Selectable menus look different
        protected abstract string FillText();

        //A function that is used to find the width of the menu based on the longest provided option
        protected int FindMenuWidth()
        {
            width = 0;
            foreach (string Option in Options)
            {
                if (width < Option.Trim().Length + 3)
                {
                    width = Option.Trim().Length + 3;
                }
            }
            return width;
        }

        //A function that prints the menu to the screen
        public virtual void PrintMenu()
        {
            Console.Clear();
            text = FillText();
            Console.WriteLine(@text);
        }
    }


    public class Prompt : Menu
    {

        public Prompt(string Prompt, int Link)
        {
            this.Options = new string[] {Prompt};
            this.Links = new int[] { Link };
            this.width = FindMenuWidth();
            this.text = FillText();
        }

        //The prompt menu has very simple rendering and skipps the box that is usually rendered in the Selectable menus
        protected override string FillText()
        {
            return Options[0];
        }

        //This function retuns user input only if it is not null or a whitespace character
        public string ReadInput()
        {
            string input = Console.ReadLine();

            while (input.Trim() == "" || input.Trim() == null)
            {
                PrintMenu();
                Console.WriteLine("the text your inputed was not valid. Plese try again:");
                input = Console.ReadLine();
            }

            return input;
        }
    }


    public class Selectable : Menu
    {
        //the curren selection variable is used in order for the program to remember what the current selected option is
        private int currentSelection;

        public int CurrentSelection
        {
            get { return currentSelection; }
            set { currentSelection = value; }
        }

        public Selectable(string[] Options, int[] Links)
        {
            this.Options = Options;
            this.Links = Links;
            this.width = FindMenuWidth();
            this.CurrentSelection = 1;
            this.text = FillText();
        }

        //The function that renders the menu to a string
        protected override string FillText()
        {
            //We start by intializing a string builder
            StringBuilder TempText = new StringBuilder();

            //We append the first option of the menu which in this case is the title
            TempText.Append(Options[0]);
            TempText.Append('\n');

            //We render the top line of the box
            TempText.Append('┌');
            TempText.Append(new string('─', width - 2));
            TempText.Append('┐');
            TempText.Append('\n');

            //This loop renders the bulk of the menu line by line starting from the *second line because the first one has already been rendered
            for (int Y = 1; Y < (Options.Length - 1) * 2; Y++)
            {
                //We calculate the index of the option that needs to be added to the current line based on the Y coordinate
                //The one is added because we are interested in the options after the first one since it is the title and can not be selected
                int CurrentLineWithOption = Y / 2 + 1;
                //The render from left to right so we start with the left wall of the menu
                TempText.Append('│');

                //We want to render an option on every second line, since we want some space between the options
                if (Y % 2 != 0)
                {
                    //We check if the current line should displayed as selected
                    //If it is we add the '>', otherwise we just add a space
                    if (CurrentSelection == CurrentLineWithOption)
                    {
                        TempText.Append('>');
                    }
                    else
                    {
                        TempText.Append(' ');
                    }

                    //after that we add the text for the option along side the appropriate amount of spaces so that the size of the menu is constant
                    TempText.Append(Options[CurrentLineWithOption]
                        + new string(' ', width - 3 - Options[CurrentLineWithOption].Length));

                }
                else
                {
                    //If no option is to be displayed we just add the appropriate amount of spaces so that the size of the menu is constant
                    TempText.Append(new string(' ', width - 2));
                }

                //The line is finished by adding the right side of menu
                TempText.Append('│');
                TempText.Append('\n');
            }

            //Last but not least we add the last line of the menu which is the bottom one
            TempText.Append('└');
            TempText.Append(new string('─', width - 2));
            TempText.Append('┘');
            TempText.Append('\n');

            //We build the string from the string builder and return the rendered menu
            return TempText.ToString();
        }

        public void UpdateMenuSelection(GUI.Direction MoveDirection)
        {
            //The current option selection gets adjusted based on how the user has decided to "move" the '>' symbol
            if (MoveDirection == GUI.Direction.Up && CurrentSelection - 1 >= 1)
            {
                CurrentSelection--;
            }
            else if (MoveDirection == GUI.Direction.Down && CurrentSelection < Options.Length - 1)
            {
                CurrentSelection++;
            }
        }
    }
}
