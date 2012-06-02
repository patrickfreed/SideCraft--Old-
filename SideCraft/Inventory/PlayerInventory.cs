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
            contents[0, 0] = new MaterialStack(Material.STONE, 5);
        }

        public void add(MaterialStack stack) {
            for(int x = 0; x < contents.GetLength(0); x++){
                for (int y = 0; y < contents.GetLength(1); y++) {
                    if (contents[x, y] != null) {
                        if (contents[x, y].getType() == stack.getType()) {
                            if (contents[x, y].getAmount() < contents[x, y].getType().getMaxStackSize()) {
                                int amount = contents[x, y].getType().getMaxStackSize() - contents[x, y].getAmount();

                                if (amount > stack.getAmount()) {
                                    contents[x, y].modifyAmount(stack.getAmount());
                                    stack.modifyAmount(-1 * stack.getAmount());
                                }
                                else {
                                    contents[x, y].modifyAmount(amount);
                                    stack.modifyAmount(-1 * amount);
                                }

                                if (stack.getAmount() == 0) {
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            if (stack.getAmount() > 0) {
                for (int y = 0; y < contents.GetLength(0); y++) {
                    for (int x = 0; x < contents.GetLength(1); x++) {
                        if (contents[x, y]  == null) {
                            setAt(x, y, stack);
                            return;
                        }
                    }
                }
            }
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
                    if (contents[x, y] != null) {
                        if (contents[x, y].getType().getId() == stack.getType().getId()) {
                            return new int[] { x, y };
                        }
                    }
                }
            }
            return new int[]{-1, -1};
        }

        public void draw(SpriteBatch batch) {

        }
    }
}
