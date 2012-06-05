using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideCraft {
    class Settings {
        //Modify-able settings
        public static int BLOCK_SIZE = 24;
        public static bool DEBUG = false;
        
        //Block ids
        public const int AIR = 0;
        public const int GRASS = 1;
        public const int DIRT = 2;
        public const int STONE = 3;
        public const int IRON_ORE = 4;
        public const int OBSIDIAN = 5;
    }
}
