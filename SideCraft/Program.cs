using System;
using SideCraft.menu;
using System.Windows.Forms;

namespace SideCraft {
#if WINDOWS || XBOX
    static class Program{

        public static SideCraftMainMenu menu;
        
        static void Main(string[] args){
            Application.EnableVisualStyles();
            menu = new SideCraftMainMenu();
            if (menu.ShowDialog() == DialogResult.OK) {
                menu = null;
                using (SideCraft game = new SideCraft()) {
                    game.Run();
                }
            }
        }
    }
#endif
}

