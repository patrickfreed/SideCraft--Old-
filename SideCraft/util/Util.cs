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
            double x = (double)(position.X - Game1.player.startMapPosition.X) / 32 + Game1.player.coordinates.getX();
            double y = (((position.Y - Game1.player.startMapPosition.Y) / 32) * -1) + Game1.player.coordinates.getY() + 1;

            return new Coordinates(x,y);
        }

        public static Vector2 getUpdatedPosition(Vector2 oldPosition) {
            Rectangle window = Game1.graphics.GraphicsDevice.Viewport.Bounds;
            
            int height = (int)window.Height / 32;
            int width = (int)window.Width / 32;
            
            Vector2 newScreenPos = new Vector2((width / 2) * 32, (height / 2) * 32);
            
            int xDiff = (int)newScreenPos.X - (int)Game1.player.startScreenPosition.X;
            int yDiff = ((int)newScreenPos.Y - (int)Game1.player.startScreenPosition.Y);

            return new Vector2(oldPosition.X + xDiff, oldPosition.Y + yDiff);
        }

        public static Rectangle getUpdatedRectangle(Rectangle oldPosition) {
            Rectangle window = Game1.graphics.GraphicsDevice.Viewport.Bounds;
            
            int width = (int)window.Width / 32;
            int height = (int)window.Height / 32;
            
            Vector2 newScreenPos = new Vector2((width / 2) * 32, (height/2) * 32);
            
            int xDiff = (int)newScreenPos.X - (int)Game1.player.startScreenPosition.X;
            int yDiff = (int)newScreenPos.Y - (int)Game1.player.startScreenPosition.Y * -1;

            return new Rectangle(oldPosition.X + xDiff, oldPosition.Y + yDiff, oldPosition.Width, oldPosition.Height);
        }

    }
}