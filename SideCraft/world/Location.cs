using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideCraft{
    public class Location{
        private double x, y;
        private String world;

        public Location(double x, double y) {
            this.x = x;
            this.y = y;
            this.world = SideCraft.player.getWorld().getName();
        }

        public Location(double x, double y, string world) {
            this.x = x;
            this.y = y;
            this.world = world;
        }

        public double getX() {
            return this.x;
        }

        public double getY() {
            return this.y;
        }

        public void modifyX(double amount) {
            this.x =  x + amount;
        }

        public void modifyY(double amount) {
            this.y += amount;
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

        public bool intersects(Location otherLocation) {
            if(otherLocation.getWorld().getName().Equals(world)){
                return Math.Floor(this.getX()) - Math.Floor(otherLocation.getX()) == 0 && Math.Floor(this.getY()) - Math.Floor(otherLocation.getY()) == 0;
            }
            return false;
        }

        public World getWorld() {
            return SideCraft.worlds[world];
        }
    }
}