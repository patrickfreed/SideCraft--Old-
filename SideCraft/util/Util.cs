using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SideCraft;

namespace SideCraft {
    public class Util {

        const int GRASS = 0;
        const int DIRT = 1;
        const int STONE = 2;
        const int IRON_ORE = 3;
        const int AIR = 4;

        public Vector2 getPosition(Coordinates coordinates) {
            float x = (float)((coordinates.getX()  - Game1.player.coordinates.getX()) * 32) + Game1.player.startMapPosition.X;
            float y = (float)(((coordinates.getY() - Game1.player.coordinates.getY()) * 32) - Game1.player.startMapPosition.Y) * -1;

            return new Vector2(x, y);
        }

        public Coordinates getCoordinates(Vector2 position) {

            double x = (double)(position.X /*((Game1.player.ScreenPosition.X - Game1.player.MapPosition.X) % 32)*/ - Game1.player.startMapPosition.X) / 32 + Game1.player.coordinates.getX();
            //int y = (int)Math.Floor((double)(Game1.player.startMapPosition.Y - position.Y) / 32 + Game1.player.coordinates.Y);
            //double y = (double)(Game1.player.startMapPosition.Y - position.Y) / 32 + Game1.player.coordinates.getY();
            double y = (((position.Y - Game1.player.startMapPosition.Y) / 32) * -1) + Game1.player.coordinates.getY() + 1;
            //position.X = (((int)Math.Floor(position.X - Game1.player.startMapPosition.X) / 32 + Game1.player.coordinates.X));
            //position.Y = (((int)Math.Floor(Game1.player.startMapPosition.Y - (int)position.Y) / 32 + Game1.player.coordinates.Y));

            return new Coordinates(x,y);
        }

        public float negativeSign(float number) {
            if (number <= 0) {
                return -1;
            }
            else {
                return 1;
            }
        }

        public float getFractionOfBlock() {
            //if (Game1.player.coordinates.X > 0) {
                return (Game1.player.ScreenPosition.X - Game1.player.MapPosition.X) % 32;
            //}
            //else {
               // return (Game1.player.MapPosition.X - Game1.player.ScreenPosition.X) % 32;
            //}
        }

        public float getInteger(double number) {
            if (number < 0) {
                return (float)Math.Ceiling(number);
            }
            else {
                return (float)Math.Floor(number);
            }
        }
    }
}