using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.Terrain.MaterialData;

namespace SideCraft {
    public class Block {

        private Coordinates coordinates;
 
        private MaterialData data;
        
        public Vector2 location;

        public Block(MaterialData d) {
            this.data = d;
        }

        public int getTypeId() {
            return this.data.getId();
        }

        public MaterialData getType() {
            return this.data;
        }

        public Coordinates getCoordinates() {
            return this.coordinates;
        }

        public void setCoordinates(Coordinates newc) {
            this.coordinates = newc;
        }

        public void setType(MaterialData dataToSet) {
            this.data = dataToSet;
        }

        public void draw(SpriteBatch spriteBatch, Texture2D tile, Vector2 position) {
            this.location = position;
            spriteBatch.Draw(data.getTexture(), position, Color.White);
        }
    }
}
