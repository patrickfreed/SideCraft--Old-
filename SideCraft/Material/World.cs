using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.Terrain.MaterialData;

namespace SideCraft {
   public class World {
   
       private Dictionary<String, Block> blocks;
       private String name;

       private int xLength;
       private int yLength;

       private Util util;

       public World(Dictionary<String, Block> bl, String n) {
           this.name = n;
           this.blocks = bl;
           this.util = new Util();
           this.xLength = (int)(Game1.graphics.GraphicsDevice.Viewport.Bounds.Width / 32);
           this.yLength = (int)(Game1.graphics.GraphicsDevice.Viewport.Bounds.Height / 32);
       }

       public String getName() {
           return name;
       }

       public Dictionary<String, Block> getBlocks() {
           return blocks;
       }

       public Block getBlockAt(Coordinates coordinates) {
           int x = (int)Math.Floor(coordinates.getX());
           int y = (int)Math.Floor(coordinates.getY());
           
           if(!blocks.ContainsKey(x.ToString() + "," + y.ToString())){   
               if (coordinates.getY() > -1) {
                   blocks.Add(x.ToString() + "," + y.ToString(), new Block(new Air()));
               }
               else if (coordinates.getY() == -1) {
                   blocks.Add(x.ToString() + "," + y.ToString(), new Block(new Grass()));
               }
               else if (coordinates.getY() == -2) {
                   Random rnd = new Random();

                   if (rnd.Next(0, 2) == 0) {
                       blocks.Add(x.ToString() + "," + y.ToString(), new Block(new Dirt()));
                   }
                   else {
                       blocks.Add(x.ToString() + "," + y.ToString(), new Block(new Stone()));
                   }
               }
               else if (coordinates.getY() < -2) {
                   blocks.Add(x.ToString() + "," + y.ToString(), new Block(new Stone()));
               }
               blocks[x.ToString() + "," + y.ToString()].setCoordinates(coordinates);
           }
           
           return blocks[x.ToString() + "," + y.ToString()];
       }

       public void update(SpriteBatch spriteBatch) {
           updateSize();
           
           Coordinates upperExtreme = new Coordinates(Math.Floor(Game1.player.coordinates.getX()) + xLength, Math.Floor(Game1.player.coordinates.getY() + yLength));
           Coordinates lowerExtreme = new Coordinates(Math.Floor(Game1.player.coordinates.getX()) - xLength, Math.Floor(Game1.player.coordinates.getY() - yLength));

           int xDistance = (int)Math.Abs(upperExtreme.getX() - lowerExtreme.getX());
           int yDistance = (int)Math.Abs(upperExtreme.getY() - lowerExtreme.getY());

           for (int x = 0; x <= xDistance; x++) {
               for (int y = 0; y <= yDistance; y++) {
                   double xCoord = upperExtreme.getX() + increment(x, (int)upperExtreme.getX(), (int)lowerExtreme.getX());
                   double yCoord = upperExtreme.getY() + increment(y, (int)upperExtreme.getY(), (int)lowerExtreme.getY());

                   Block block = getBlockAt(new Coordinates(xCoord,yCoord));
                   block.draw(spriteBatch, null, util.getPosition(block.getCoordinates()));
               }
           }
       }

       public void updateSize() {        
           Rectangle window = Game1.graphics.GraphicsDevice.Viewport.Bounds;

           int height = (int)window.Height / 32;
           int width = (int)window.Width / 32;

           Vector2 newScreenPos = new Vector2((width / 2) * 32, (height/2) * 32);

           int xDiff = (int)newScreenPos.X - (int)Game1.player.ScreenPosition.X;
           int yDiff = ((int)newScreenPos.Y - (int)Game1.player.ScreenPosition.Y);

           Game1.player.startScreenPosition = Game1.player.ScreenPosition;
           Game1.player.startMapPosition.X += xDiff;
           Game1.player.startMapPosition.Y += yDiff;
           Game1.player.ScreenPosition = newScreenPos;
           Game1.player.recPos.X += xDiff;
           Game1.player.recPos.Y += yDiff;

           this.xLength = width;
           this.yLength = height;
       }

       private int increment(int x, int compare1, int compare2) {
           if (compare1 > compare2) {
               return -1 * x;
           }
           else {
               return x;
           }
       }
   }
}
