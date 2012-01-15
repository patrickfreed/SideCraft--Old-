using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.Material;

namespace SideCraft.Terrain.MaterialData {
    class Stone:MaterialData {

        private int durability;

        public Stone() {
            this.durability = getCurrentDurability();
        }
        
        public int getId() {
            return Game1.STONE;
        }

        public int getMaxStackSize() {
            return MaterialStack.DEFAULT_STACK_SIZE;;
        }

        public Texture2D getTexture() {
            return Game1.stoneTile;
        }

        public int getDamage() {
            return 1;
        }


        public int getMaxDurability() {
            return 45;
        }

        public int getCurrentDurability() {
            return this.durability;
        }


        public void damage(int d) {
            this.durability -= d;
        }
    }
}
