using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.Material;

namespace SideCraft.Terrain.MaterialData {
    class Dirt:MaterialData {

        private int durability;
        private int maxDurability;

        public Dirt() {
            this.maxDurability = 15;
            this.durability = getMaxDurability();
        }

        public int getId() {
            return Game1.DIRT;
        }

        public int getMaxStackSize(){
            return MaterialStack.DEFAULT_STACK_SIZE;
        }

        public Texture2D getTexture() {
            return Game1.dirtTile;
        }


        public int getDamage() {
            return 1;
        }


        public int getMaxDurability() {
            return this.maxDurability;
        }

        public int getCurrentDurability() {
            return durability;
        }

        public void damage(int d) {
            this.durability -= d;
        }
    }
}
