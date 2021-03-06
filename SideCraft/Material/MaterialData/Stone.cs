﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.Material;

namespace SideCraft.Terrain.MaterialData {
    class Stone:MaterialData {

        private int durability;
        private int maxDurability;

        public Stone() {
            this.maxDurability = 45;
            this.durability = getMaxDurability();
        }
        
        public int getId() {
            return Game1.STONE;
        }

        public int getMaxStackSize() {
            return MaterialStack.DEFAULT_STACK_SIZE;
        }

        public Texture2D getTexture() {
            return Game1.stoneTile;
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
