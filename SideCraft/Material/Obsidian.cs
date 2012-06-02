using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SideCraft.material {
    public class Obsidian:Material {

        const int DURABILITY = 1;

        public override int getId() {
            return SideCraft.AIR;
        }

        public override int getMaxStackSize() {
            return MaterialStack.DEFAULT_STACK_SIZE;
        }

        public override Texture2D getTexture() {
            return SideCraft.obsidianTile;
        }

        public override int getDurability() {
            return DURABILITY;
        }

        public override int getDropAmount() {
            return 0;
        }

        public override Material getDropType() {
            return Material.DIRT;
        }

        public override bool isSolid() {
            return true;
        }

        public override int getDamage() {
            return 1;
        }

    }
}
