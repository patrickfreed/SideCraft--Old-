using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.material;

namespace SideCraft.material {
    public class Dirt:Material {

        const int DURABILITY = 15;

        public override int getId() {
            return SideCraft.DIRT;
        }

        public override int getMaxStackSize(){
            return MaterialStack.DEFAULT_STACK_SIZE;
        }

        public override Texture2D getTexture() {
            return SideCraft.dirtTile;
        }

        public override int getDamage() {
            return 1;
        }

        public override int getDurability() {
            return DURABILITY;
        }

        public override Material getDropType() {
            return Material.DIRT;
        }

        public override bool isSolid() {
            return true;
        }

        public override int getDropAmount() {
            return 1;
        }

    }
}
