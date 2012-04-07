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

        public static Vector2 getPosition(Location coordinates) {
            float x = (float)((coordinates.getX()  - SideCraft.player.coordinates.getX()) * 32) + SideCraft.player.ScreenPosition.X;
            //float y = (float)(SideCraft.player.startMapPosition.Y - (coordinates.getY() - SideCraft.player.coordinates.getY()) * 32);
            double y = SideCraft.player.ScreenPosition.Y - ((coordinates.getY() - SideCraft.player.coordinates.getY()) * 32);

            return new Vector2(x, (float)y);
        }

        public static Rectangle getRectangle(Location coordinates, int width, int height, bool updateSize) {
            Vector2 pos = getPosition(coordinates);
            
            int x = (int)pos.X;
            int y = (int)pos.Y;

           // int x = (int)(((coordinates.getX() - SideCraft.player.coordinates.getX()) * 32) + SideCraft.player.startMapPosition.X);
           // int y = (int)(((coordinates.getY() - SideCraft.player.coordinates.getY()) * 32) - SideCraft.player.startMapPosition.Y) * -1;

            if (updateSize) {
                return new Rectangle(x, y, width * (800 / SideCraft.graphics.GraphicsDevice.Viewport.Width), width * (480 / SideCraft.graphics.GraphicsDevice.Viewport.Height));
            }
            else {
                return new Rectangle(x, y, width, height);
            }
        }

        public static Location getCoordinates(Vector2 position) {
            double x = (double)(position.X - SideCraft.player.ScreenPosition.X) / 32 + SideCraft.player.coordinates.getX();
            double y = (((position.Y - SideCraft.player.ScreenPosition.Y) / 32) * -1) + SideCraft.player.coordinates.getY();

            return new Location(x,y);
        }
    }
}