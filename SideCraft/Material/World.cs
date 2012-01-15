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

       private Util util;

       public World(Dictionary<String, Block> bl, String n) {
           this.name = n;
           this.blocks = bl;
           this.util = new Util();
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
           Coordinates upperExtreme = new Coordinates(Math.Floor(Game1.player.coordinates.getX()) + 13, Math.Floor(Game1.player.coordinates.getY() + 9));
           Coordinates lowerExtreme = new Coordinates(Math.Floor(Game1.player.coordinates.getX()) - 13, Math.Floor(Game1.player.coordinates.getY() - 9));

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
