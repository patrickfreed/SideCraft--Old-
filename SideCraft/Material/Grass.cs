using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.material;

namespace SideCraft.material {
    public class Grass : Material {

        const int DURABILITY = 15;

        public override int getId() {
            return SideCraft.GRASS;
        }

        public override int getMaxStackSize() {
            return MaterialStack.DEFAULT_STACK_SIZE;
        }

        public override Texture2D getTexture() {
            return SideCraft.grassTile;
        }

        public override int getDamage() {
            return 1;
        }

        public override int getDropAmount() {
            return 4;
        }

        public override Material getDropType() {
            return Material.DIRT;
        }

        public override bool isSolid() {
            return true;
        }

        public override int getDurability() {
            return DURABILITY;
        }
    }
}
