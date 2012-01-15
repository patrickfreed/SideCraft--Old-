using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SideCraft.Terrain.MaterialData;
using SideCraft.Material;

namespace SideCraft.UI {
    public class Toolbar {
        private MaterialData[] objects = new MaterialData[4];
        private int currentIndex;

        private Vector2 location = new Vector2(315, 425);

        public Toolbar() {
            this.currentIndex = 0;
        }

        public MaterialStack getSelectedObj() {
            return Game1.player.getInventory().getContents()[currentIndex, 0];
        }

        public int getCurrentIndex() {
            return currentIndex;
        }

        public void setCurrentIndex(int index) {
            currentIndex = index;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Game1.toolbarTile, location, Color.White);
        }
    
    }
}
