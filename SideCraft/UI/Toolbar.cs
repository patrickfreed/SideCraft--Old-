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
       
        private Rectangle BOX_ONE = new Rectangle(322, 432, 30, 30);
        private Rectangle BOX_TWO = new Rectangle(327,432, 30, 30);
        private Rectangle BOX_THREE = new Rectangle(332, 432, 30, 30);
        private Rectangle BOX_FOUR = new Rectangle(337, 432, 30, 30);
        private Rectangle BOX_FIVE = new Rectangle(342, 432, 30, 30);

        private Rectangle[] boxes;
    
        public Toolbar() {
            this.currentIndex = 0;
            boxes = new Rectangle[]{BOX_ONE, BOX_TWO, BOX_THREE, BOX_FOUR, BOX_FIVE};
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

            for (int x = 0; x < boxes.Length; x++) {
                spriteBatch.Draw(Game1.player.getInventory().getAt(0, x).getType().getTexture(), boxes[x], Color.White);
            }
        }
    
    }
}
