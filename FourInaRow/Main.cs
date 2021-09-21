using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C21_Ex05_Shai313189490_David_208430165
{
    public class program
    {
        public static void Main(string[] args)
        {
            GameSettings settings = new GameSettings();

            settings.ShowDialog();

            if (settings.isGameContinue)
            {
                Game g = new Game(settings);
                g.ShowDialog();
            }
        }
    }
}
