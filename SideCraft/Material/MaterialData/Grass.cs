using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.Material;

namespace SideCraft.Terrain.MaterialData {
    class Grass:MaterialData {

        private int durability;
        private int maxDurability;

        public Grass() {
            this.maxDurability = 15;
            durability = getMaxDurability();
        }
        
        public int getId() {
            return Game1.GRASS;
        }

        public int getMaxStackSize() {
            return MaterialStack.DEFAULT_STACK_SIZE;
        }

        public Texture2D getTexture() {
            return Game1.grassTile;
        }


        public int getDamage() {
            return 1;
        }


        public int getMaxDurability() {
            return this.maxDurability;
        }

        public int getCurrentDurability() {
            return this.durability;
        }


        public void damage(int d) {
            this.durability -= d;
        }
    }
}
