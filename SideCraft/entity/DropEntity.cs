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

        readonly int WIDTH = 15, HEIGHT = 15;

        public DropEntity(Material t, Location loc) {
            HEIGHT = Settings.BLOCK_SIZE / 2;
            WIDTH = Settings.BLOCK_SIZE / 2;
            spawn(loc);
            type = t;
        }

        public DropEntity(Material t, double x, double y, World world) {
            location = new Location(x, y, world.getName());
            type = t; HEIGHT = Settings.BLOCK_SIZE / 2;
            WIDTH = Settings.BLOCK_SIZE / 2;


            spawn(location);
        }

        public DropEntity(Material t, double x, double y) {
            HEIGHT = Settings.BLOCK_SIZE / 2;
            WIDTH = Settings.BLOCK_SIZE / 2;
            type = t;
            Location location = new Location(x, y);
            spawn(location);
        }

        public void spawn(Location loc) {
            while (loc.getWorld().getBlockAt(loc).getType().isSolid()) {
                loc.modifyY(1);
            }

            this.location = loc;
            loc.getWorld().registerEntity(this);
        }

        public void update(GameTime time) {
            
            Block thisBlock = this.getLocation().getWorld().getBlockAt(this.getLocation());
            if (!thisBlock.getType().isSolid()) {
                Block nextBlock = this.getLocation().getWorld().getBlockAt(new Location(location.getX(), location.getY() - 1));

                if (nextBlock.getType().isSolid()) {
                    if(!nextBlock.getBounds().Intersects(getBounds())){
                        location.setY(location.getY() - 0.2);
                    }
                }else{
                    location.setY(location.getY() - 0.2);
                }
            }

            if (Math.Abs(SideCraft.player.coordinates.getX() - getLocation().getX()) <= 1.5 && Math.Abs(SideCraft.player.coordinates.getY() - getLocation().getY()) <= 1.5) {
                if (SideCraft.player.coordinates.getX() > getLocation().getX()) {
                    location.modifyX(0.2);
                }
                else {
                    location.modifyX(-0.2);
                }
            }

            if(getBounds().Intersects(SideCraft.player.getBounds())){
                destroy();
            }
        }

        public void destroy() {
            SideCraft.player.getInventory().add(new MaterialStack(this.type, 1));
            getLocation().getWorld().unregisterEntity(this);
        }

        public void draw() {
            Screen.render(location, type.getTexture(), WIDTH, HEIGHT, false);
        }

        public Location getLocation() {
            return this.location;
        }

        public Rectangle getBounds() {
            return location.toRectangle(WIDTH, HEIGHT);
        }
    }
}
