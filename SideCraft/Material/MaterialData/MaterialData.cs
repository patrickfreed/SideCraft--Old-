using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SideCraft.Terrain.MaterialData {
    public interface MaterialData {
        
        int getId();

        int getMaxStackSize();

        int getDamage();

        int getMaxDurability();

        int getCurrentDurability();

        void damage(int d);

        Texture2D getTexture();
    }
}
