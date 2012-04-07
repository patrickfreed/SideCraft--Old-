using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SideCraft.material;
using Microsoft.Xna.Framework.Graphics;

namespace SideCraft.inventory {
    public class PlayerInventory {

        private MaterialStack[,] contents;

        const int COLUMNS = 5;
        const int ROWS = 4;

        public PlayerInventory() {
            contents = new MaterialStack[COLUMNS, ROWS];
            contents[4, 0] = new MaterialStack(Material.GRASS, 5);
        }

        public MaterialStack getAt(int column, int row) {    
            if (contents[column, row] != null) {
                return contents[column, row];
            }
            else {
                return new MaterialStack(Material.AIR, 0);
            }
        }

        public void setAt(int column, int row, MaterialStack stack) {
            if (column < contents.GetLength(0) && row < contents.GetLength(1) && row > -1 && column > -1) {
                contents[column, row] = stack;
            }
        }

        public MaterialStack[,] getContents() {
            return this.contents;
        }

        public void setContents(MaterialStack[,] newc) {
            if (newc.GetLength(0) == contents.GetLength(0) && newc.GetLength(1) == contents.GetLength(1)) {
                this.contents = newc;
            }
        }

        public int[] getIndex(MaterialStack stack) {
            for(int x = 0; x < contents.GetLength(0); x++){
                for (int y = 0; y < contents.GetLength(1); y++) {
                    if(contents[x,y].getType().getId() == stack.getType().getId()){
                        return new int[] {x,y};
                    }
                }
            }
            return new int[]{-1, -1};
        }

        public void draw(SpriteBatch batch) {

        }
    }
}
