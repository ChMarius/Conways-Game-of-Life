using System.Text;

namespace Conways_Game_of_Life
{
    internal static class GUI
    {

    }

    public class Menu
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private string[] options;

        public string[] Options
        {
            get { return options; }
            set { options = value; }
        }

        public enum Direction
        {
            Up,
            Down
        }

        public Menu(string[] Options) 
        {
            this.Options = Options;
            this.Width = FindMenuWidth();
            this.Text = FillText();
        }
        public Menu(int Width, string[] Options)
        {
            this.Width = Width;
            this.Options = Options;
            this.Text = FillText();
        }

        private int FindMenuWidth()
        {
            Width = 0;
            foreach (string Option in Options)
            {
                if (Width < Option.Trim().Length + 3)
                {
                    Width = Option.Trim().Length + 3;
                }
            }
            return Width;
        }

        private string FillText()
        {
            StringBuilder TempText = new StringBuilder();

            TempText.Append(Options[0]);
            TempText.Append('\n');

            TempText.Append('┌');
            TempText.Append(new string('─', Width - 2));
            TempText.Append('┐');
            TempText.Append('\n');

            int OptionCounter = 1;

            for (int Y = 0; Y < (Options.Length - 1) * 2; Y++)
            {
                TempText.Append('│');

                if (Y % 2 != 0)
                {
                    TempText.Append(' ' + Options[OptionCounter]
                        + new string(' ', Width - Options[OptionCounter].Length));
                    OptionCounter++;
                }
                else
                {
                    TempText.Append(new string(' ', Width - 2));
                }

                TempText.Append('│');
                TempText.Append('\n');
            }

            TempText.Append('└');
            TempText.Append(new string('─', Width - 2));
            TempText.Append('┘');
            TempText.Append('\n');

            return TempText.ToString();
        }

        public void PrintMenu()
        {
            Console.WriteLine(@text);
        }
    }
}
