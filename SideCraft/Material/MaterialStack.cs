using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideCraft.material {
    public class MaterialStack {

        public const int DEFAULT_STACK_SIZE = 64;

        private Material type;

        private int amount;

        public MaterialStack(Material t, int am) {
            this.type = t;
            this.amount = am;
        }

        public Material getType() {
            return this.type;
        }

        public int getAmount() {
            return this.amount;
        }

        public void modifyAmount(int newAmount) {
            this.amount += newAmount;
        }
    }
}
