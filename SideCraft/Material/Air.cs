using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.material;

namespace SideCraft.material {
    public class Air:Material{

        const int DURABILITY = 1;

        public override int getId() {
            return SideCraft.AIR;
        }

        public override int getMaxStackSize() {
            return MaterialStack.DEFAULT_STACK_SIZE;
        }

        public override Texture2D getTexture() {
            return SideCraft.airTile;
        }

        public override int getDurability() {
            return DURABILITY;
        }

        public override int getDropAmount() {
            return 0;
        }

        public override Material getDropType() {
            throw new NotImplementedException();
        }

        public override bool isSolid() {
            return false;
        }

        public override int getDamage() {
            return 1;
        }

    }
}
