﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        public static Location valueOf(Vector2 position){
            return valueOf(position.X, position.Y);
        }

        public static Location valueOf(Rectangle rect) {
            return valueOf(rect.X, rect.Y);
        }

        public static Location valueOf(float x1, float y1) {
            double x = (x1 - SideCraft.player.ScreenPosition.X) / Settings.BLOCK_SIZE + SideCraft.player.coordinates.getX();
            double y = (y1 - SideCraft.player.ScreenPosition.Y) / -Settings.BLOCK_SIZE + SideCraft.player.coordinates.getY();

            return new Location(x, y);
        }

        public Vector2 toVector2() {
            double x = (this.x - SideCraft.player.coordinates.getX()) * Settings.BLOCK_SIZE + SideCraft.player.ScreenPosition.X;
            double y = SideCraft.player.ScreenPosition.Y - ((this.y - SideCraft.player.coordinates.getY()) * Settings.BLOCK_SIZE);

            return new Vector2((float)x, (float)y);
        }

        public Rectangle toRectangle(int width, int height) {
            Vector2 v = toVector2();

            return new Rectangle((int)v.X, (int)v.Y, width, height);
        }

        public String toString() {
            return "{X:" + x + String.Empty + " Y:" + y + "}";
        }

        public Location modify(double x, double y) {
            return new Location(this.x + x, this.y + y, this.world);
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