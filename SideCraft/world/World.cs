using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.material;
using SideCraft.entity;

namespace SideCraft {
    public class World {

        private Dictionary<String, Block> blocks;

        private List<Entity> entities;

        private String name;

        private int xLength;
        private int yLength;

        private Util util;

        public World(String n) {
            this.name = n;
            this.blocks = new Dictionary<string, Block>();
            this.util = new Util();
            this.xLength = (int)(SideCraft.graphics.GraphicsDevice.Viewport.Bounds.Width / 32);
            this.yLength = (int)(SideCraft.graphics.GraphicsDevice.Viewport.Bounds.Height / 32);

            entities = new List<Entity>();
        }

        public String getName() {
            return name;
        }

        public Dictionary<String, Block> getBlocks() {
            return blocks;
        }

        public Block getBlockAt(Location coordinates) {
            int x = (int)Math.Floor(coordinates.getX());
            int y = (int)Math.Ceiling(coordinates.getY());

            if (!blocks.ContainsKey(x.ToString() + "," + y.ToString())) {
                if (coordinates.getY() > -1) {
                    blocks.Add(x.ToString() + "," + y.ToString(), new Block(Material.AIR));
                }
                else if (coordinates.getY() == -1) {
                    blocks.Add(x.ToString() + "," + y.ToString(), new Block(Material.GRASS));
                }
                else if (coordinates.getY() == -2) {
                    Random rnd = new Random();

                    if (rnd.Next(0, 2) == 0) {
                        blocks.Add(x.ToString() + "," + y.ToString(), new Block(Material.DIRT));
                    }
                    else {
                        blocks.Add(x.ToString() + "," + y.ToString(), new Block(Material.STONE));
                    }
                }
                else if (coordinates.getY() < -2) {
                    blocks.Add(x.ToString() + "," + y.ToString(), new Block(Material.STONE));
                }
                blocks[x.ToString() + "," + y.ToString()].setLocation(coordinates);
            }

            return blocks[x.ToString() + "," + y.ToString()];
        }

        public void update(SpriteBatch spriteBatch) {
            //updateSize();

            Location upperExtreme = new Location(Math.Floor(SideCraft.player.coordinates.getX()) + xLength, Math.Floor(SideCraft.player.coordinates.getY() + yLength));
            Location lowerExtreme = new Location(Math.Floor(SideCraft.player.coordinates.getX()) - xLength, Math.Floor(SideCraft.player.coordinates.getY() - yLength));

            int xDistance = (int)Math.Abs(upperExtreme.getX() - lowerExtreme.getX());
            int yDistance = (int)Math.Abs(upperExtreme.getY() - lowerExtreme.getY());

            for (int x = 0; x <= xDistance; x++) {
                for (int y = 0; y <= yDistance; y++) {
                    double xCoord = upperExtreme.getX() + increment(x, (int)upperExtreme.getX(), (int)lowerExtreme.getX());
                    double yCoord = upperExtreme.getY() + increment(y, (int)upperExtreme.getY(), (int)lowerExtreme.getY());

                    getBlockAt(new Location(xCoord, yCoord)).draw();
                }
            }

            updateEntities();

        }

        public void updateEntities() {
            for (int x = 0; x < entities.Count; x++) {
                entities[x].update();
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

        public void registerEntity(Entity e) {
            entities.Add(e);
        }
    }
}
