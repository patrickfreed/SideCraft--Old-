using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SideCraft.Terrain.MaterialData;

namespace SideCraft.Material {
    public class MaterialStack {

        public const int DEFAULT_STACK_SIZE = 64;

        private MaterialData type;

        private int amount;

        public MaterialStack(MaterialData t, int am) {
            this.type = t;
            this.amount = am;
        }

        public MaterialData getType() {
            return this.type;
        }

        public int getAmount() {
            return this.amount;
        }

        public void setAmount(int newAmount) {
            this.amount = newAmount;
        }
    }
}
