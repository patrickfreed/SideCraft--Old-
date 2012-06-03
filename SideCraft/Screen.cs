using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SideCraft {
    class Screen {
        public static void renderString(SpriteFont font, String text, Vector2 v, Color color) {
            SideCraft.spriteBatch.Begin();
            SideCraft.spriteBatch.DrawString(font, text, v, color);
            SideCraft.spriteBatch.End();
        }


        public static void render(Vector2 v, Texture2D texture) {
            SideCraft.spriteBatch.Begin();
            SideCraft.spriteBatch.Draw(texture, v, Color.White);
            SideCraft.spriteBatch.End();
        }
        
        public static void render(Rectangle rec, Texture2D texture) {
            SideCraft.spriteBatch.Begin();
            SideCraft.spriteBatch.Draw(texture, rec, Color.White);
            SideCraft.spriteBatch.End();
        }
        
        public static void render(Location location, Texture2D texture, int width, int height, bool center) {
            SideCraft.spriteBatch.Begin();
            Rectangle position = location.toRectangle(width, height);

            if (center) {
                position = new Rectangle(position.Center.X, position.Bottom, width, height);
            }

            SideCraft.spriteBatch.Draw(texture, position, Color.White);
            SideCraft.spriteBatch.End();
        }
    }
}
