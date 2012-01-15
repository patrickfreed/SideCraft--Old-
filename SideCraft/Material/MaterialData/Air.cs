using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.Material;

namespace SideCraft.Terrain.MaterialData {
    class Air:MaterialData {
        public int getId() {
            return Game1.AIR;
        }

        public int getMaxStackSize() {
            return MaterialStack.DEFAULT_STACK_SIZE;;
        }

        public Texture2D getTexture() {
            return Game1.airTile;
        }


        public int getDamage() {
            return 1;
        }


        public int getMaxDurability() {
            throw new NotImplementedException();
        }

        public int getCurrentDurability() {
            throw new NotImplementedException();
        }


        public void damage(int d) {
            throw new NotImplementedException();
        }
    }
}
