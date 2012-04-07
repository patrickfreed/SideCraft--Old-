using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SideCraft.material;
using Microsoft.Xna.Framework;

namespace SideCraft.entity {
    class DropEntity:Entity {

        private readonly Material type;
        private Location location;

        const int WIDTH = 15, HEIGHT = 15;

        public DropEntity(Material t, Location loc) {
            location = loc;
            type = t;
            loc.getWorld().registerEntity(this);
        }

        public DropEntity(Material t, double x, double y, World world) {
            location = new Location(x, y, world.getName());
            type = t;
            location.getWorld().registerEntity(this);
        }

        public DropEntity(Material t, double x, double y) {
            type = t;
            location = new Location(x, y);
            location.getWorld().registerEntity(this);
        }

        public void update() {
            
            Block thisBlock = this.getLocation().getWorld().getBlockAt(this.getLocation());
            if (thisBlock.getType() is Air) {
                Block nextBlock = this.getLocation().getWorld().getBlockAt(new Location(location.getX(), location.getY() - 1));

                if (!(nextBlock.getType() is Air)) {
                    if(!nextBlock.getBounds().Intersects(getBounds())){
                        location.setY(location.getY() - 0.2);
                    }
                }else{
                    location.setY(location.getY() - 0.2);
                }
            }
            draw();
        }

        public void draw() {

            Screen.render(location, type.getTexture(), WIDTH, HEIGHT, false);
        }

        public Location getLocation() {
            return this.location;
        }

        public Rectangle getBounds() {
            return Util.getRectangle(location, WIDTH, HEIGHT, false);
        }
    }
}
