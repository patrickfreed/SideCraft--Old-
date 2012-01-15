using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideCraft{
    public class Coordinates{
        private double x, y;

        public Coordinates(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public double getX() {
            return this.x;
        }

        public double getY() {
            return this.y;
        }

        public void setX(double x) {
            this.x = x;
        }

        public void setY(double y) {
            this.y = y;
        }

        public void setCoordinates(double x, double y) {
            this.x = x;
            this.y = y;
        }
    }
}