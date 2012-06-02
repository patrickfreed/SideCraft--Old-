using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SideCraft.material {
    public abstract class Material {
        public static readonly Obsidian OBSIDIAN = new Obsidian();
        public static readonly Dirt DIRT = new Dirt();
        public static readonly Air AIR = new Air();
        public static readonly Grass GRASS = new Grass();
        public static readonly Stone STONE = new Stone();

        public abstract int getId();

        public abstract int getMaxStackSize();

        public abstract int getDamage();

        public abstract int getDurability();

        public abstract int getDropAmount();

        public abstract Material getDropType();

        public abstract Texture2D getTexture();

        public abstract bool isSolid();
    }
}
