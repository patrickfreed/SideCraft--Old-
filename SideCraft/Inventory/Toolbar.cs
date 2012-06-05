using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SideCraft.material;

namespace SideCraft.menu {
    public class Toolbar {
        private int currentIndex;

        private Vector2 location = new Vector2(315, 400);

        private Texture2D selectionBox;

        const int Y = 407, WIDTH = 29, HEIGHT = 29;

        private Rectangle BOX_ONE = new Rectangle(322, Y, WIDTH, HEIGHT);
        private Rectangle BOX_TWO = new Rectangle(360,Y, WIDTH, HEIGHT);
        private Rectangle BOX_THREE = new Rectangle(398, Y, WIDTH, HEIGHT);
        private Rectangle BOX_FOUR = new Rectangle(436, Y, WIDTH, HEIGHT);
        private Rectangle BOX_FIVE = new Rectangle(474, Y, WIDTH, HEIGHT);

        private Rectangle[] boxes;
    
        public Toolbar() {
            selectionBox = SideCraft.content.Load<Texture2D>("UIContent/toolbar_selection");
            this.currentIndex = 0;
            boxes = new Rectangle[]{BOX_ONE, BOX_TWO, BOX_THREE, BOX_FOUR, BOX_FIVE};
        }

        public MaterialStack getSelectedObj() {
            return SideCraft.player.getInventory().getAt(currentIndex, 0);
        }

        public int getCurrentIndex() {
            return currentIndex;
        }

        public void setCurrentIndex(int index) {
            if (index == 1) {
                if (currentIndex <= 3) {
                    currentIndex += 1;
                }
                else {
                    currentIndex = 0;
                }
            }
            else {
                if (currentIndex > 0) {
                    currentIndex -= 1;
                }else{
                    currentIndex = 4;
                }
            }
        }

        public void Draw() {
            Screen.render(location, SideCraft.toolbarTile);

            for (int x = 0; x < boxes.Length; x++) {
                MaterialStack item = SideCraft.player.getInventory().getAt(x, 0);
                
                Screen.render(boxes[x], item.getType().getTexture());

                if (x == currentIndex) {
                    Screen.render(new Rectangle(boxes[x].X - 3, Y - 3, 35, 34), this.selectionBox);
                }
                if (item.getAmount() > 0)
                    Screen.renderString(SideCraft.font, SideCraft.player.getInventory().getAt(x, 0).getAmount().ToString(), new Vector2(boxes[x].Center.X + 5, boxes[x].Center.Y - 1), Color.White);
            }
        }
    
    }
}
