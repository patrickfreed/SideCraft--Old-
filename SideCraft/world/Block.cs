using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SideCraft.material;
using SideCraft.entity;

namespace SideCraft {
    public class Block {

        private Location location;

        private int health;

        private Material data;

        public Block(Material d) {
            this.data = d;
            this.health = data.getDurability();
        }

        public int getTypeId() {
            return this.data.getId();
        }

        public Material getType() {
            return this.data;
        }

        public Location getLocation() {
            return this.location;
        }

        public void setLocation(Location newc) {
            this.location = newc;
        }

        public void setType(Material dataToSet) {
            this.data = dataToSet;
            this.health = dataToSet.getDurability();
        }

        public void draw() {
            Screen.render(getLocation(), getType().getTexture(), Settings.BLOCK_SIZE, Settings.BLOCK_SIZE, false);
        }

        public int getHealth() {
            return health;
        }

        public void damage(int d) {
            this.health -= d;

            if (health <= 0)
                destroy();
        }

        public Rectangle getBounds() {
            return location.toRectangle(32, 32);
        }

        public void destroy(){
            Material material = this.getType();
            setType(Material.AIR);

            for (int x = 0; x < material.getDropAmount(); x++) {
                DropEntity droppedBlock = new DropEntity(material.getDropType(), new Location(location.getX() + 0.6 * x, location.getY()));
            }
        }

    }
}
