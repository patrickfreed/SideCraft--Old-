using System;
using SideCraft.UI;
using System.Windows.Forms;

namespace SideCraft {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            SideCraftMainMenu menu = new SideCraftMainMenu();
            if (menu.ShowDialog() == DialogResult.OK) {
                using (Game1 game = new Game1()) {
                    game.Run();
                }
            }
        }
    }
#endif
}

