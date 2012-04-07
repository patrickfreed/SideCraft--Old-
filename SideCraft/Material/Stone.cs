using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SideCraft.material {
    public class Stone:Material {

        const int DURABILITY = 45;
        
        public override int getId() {
            return SideCraft.STONE;
        }

        public override int getMaxStackSize() {
            return MaterialStack.DEFAULT_STACK_SIZE;
        }

        public override Texture2D getTexture() {
            return SideCraft.stoneTile;
        }

        public override int getDamage() {
            return 1;
        }

        public override int getDropAmount() {
            return 1;
        }

        public override Material getDropType() {
            return Material.STONE;
        }

        public override bool isSolid() {
            return true;
        }

        public override int getDurability() {
            return DURABILITY;
        }
    }
}
