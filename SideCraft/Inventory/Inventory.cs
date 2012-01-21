using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SideCraft.Terrain.MaterialData;
using SideCraft.Material;

namespace SideCraft.Inventory {
    public class PlayerInventory {

        private MaterialStack[,] contents;

        const int COLUMNS = 5;
        const int ROWS = 4;

        public PlayerInventory() {
            contents = new MaterialStack[COLUMNS, ROWS];
            contents[0, 0] = new MaterialStack(new Stone(), 1);
        }

        public MaterialStack getAt(int row, int column) {    
            if (contents[column, row] != null) {
                return contents[column, row];
            }
            else {
                return new MaterialStack(new Air(), 0);
            }
        }

        public void setAt(int column, int row, MaterialStack stack) {
            if (column <= 3 && column >= 0 && row >= 0 && row <= 4) {
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
    }
}
