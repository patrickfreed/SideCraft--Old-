using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SideCraft {
    class Screen {
        public static void render(Location location, Texture2D texture, int width, int height, bool center) {
            Rectangle position = Util.getRectangle(location, width, height, false);

            if (center) {
                position = new Rectangle(position.Center.X, position.Bottom, width, height);
            }

            SideCraft.spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
