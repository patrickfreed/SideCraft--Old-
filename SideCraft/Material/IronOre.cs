using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SideCraft.material {
    class IronOre:Material {

        const int DURABILITY = 45;
        
        public override int getId() {
            return Settings.IRON_ORE;
        }

        public override int getMaxStackSize() {
            return MaterialStack.DEFAULT_STACK_SIZE;
        }

        public override Texture2D getTexture() {
            return SideCraft.iron_oreTile;
        }

        public override int getDamage() {
            return 1;
        }

        public override Material getDropType() {
            return Material.STONE;
        }

        public override int getDropAmount() {
            return 1;
        }

        public override bool isSolid() {
            return false;
        }

        public override int getDurability() {
            return DURABILITY;
        }
    }
}
