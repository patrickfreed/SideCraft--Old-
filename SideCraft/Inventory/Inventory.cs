using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SideCraft.Terrain.MaterialData;
using SideCraft.Material;

namespace SideCraft.Inventory {
    public class PlayerInventory {

        private MaterialStack[,] contents;

        public PlayerInventory() {
            contents = new MaterialStack[3, 4];
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
    }
}
