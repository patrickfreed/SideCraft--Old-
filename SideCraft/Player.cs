using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SideCraft;
using SideCraft.UI;
using SideCraft.inventory;
using SideCraft.material;

namespace SideCraft {
    public class Player {

        public Rectangle ScreenPosition;
        private Vector2 speed;

        private Toolbar toolbar;
        private PlayerInventory inventory;

        private Texture2D texture;

        private static Int64 milliseconds = 0;
        private float interval = 250f;

        private Vector2 DIRECTION;

        private double originalY;

        private MovementState moveState;
   
        const double JUMP_HEIGHT = 1.0;
        const int LEFT = -1, UP = 1, RIGHT = 1, DOWN = -1, MOVEMENT_SPEED = 300, STABLE = 0;

        float startX, startY, currentX, currentY, oldX, oldY;

        private static MouseState oldMouseState, mouseState;
        private static KeyboardState oldKbState, kbState;

        public String world;

        public Location coordinates;

        enum MovementState {
            WALKING,
            JUMPING,
            FALLING
        }

        enum actionState {
            IDLE,
            BREAKING
        }

        public Player() {
           
            ScreenPosition = new Rectangle(388, 224, 30, 32);
            
            startX = 388f;
            startY = 224f;
            currentX = startX;
            currentY = startY;

            DIRECTION = new Vector2(RIGHT, UP);
            coordinates = new Location(0, 0, "world");

            oldMouseState = Mouse.GetState();
            mouseState = oldMouseState;

            kbState = Keyboard.GetState();
            oldKbState = Keyboard.GetState();
            
            toolbar = new Toolbar();
            inventory = new PlayerInventory();
            
            moveState = MovementState.WALKING;
        }

        public void LoadContent(Texture2D t) {
            texture = t;
        }

        public void Update(GameTime gameTime) {
            updateStates();
            
            updateMovement(gameTime);
           // updateCollision();
            updateInteraction(gameTime);
            updateToolbar();
        }

        private void updateStates() {
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            
            oldKbState = kbState;
            kbState = Keyboard.GetState();
        }

        private void updateToolbar() {
            if (mouseState.ScrollWheelValue > oldMouseState.ScrollWheelValue || (!oldKbState.IsKeyDown(Keys.B) && kbState.IsKeyDown(Keys.B))) {
                getToolbar().setCurrentIndex(1);
            }
            else if (mouseState.ScrollWheelValue < oldMouseState.ScrollWheelValue) {
                getToolbar().setCurrentIndex(-1);
            }
        }

        private void updateInteraction(GameTime gameTime) {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                Location mouseCoords = Util.getCoordinates(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

                milliseconds += (long)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (milliseconds >= interval) {
                    milliseconds = 0;

                    if (Math.Abs(mouseCoords.getX() - coordinates.getX()) <= 4 && Math.Abs(mouseCoords.getY() - coordinates.getY()) <= 4) {
                        Block block = getWorld().getBlockAt(mouseCoords);

                        if (block.getType() is Air) return;

                        getWorld().getBlockAt(mouseCoords).damage(5);
                    }
                }
            }
            else if (Mouse.GetState().RightButton == ButtonState.Pressed) {
                Location mouseCoords = Util.getCoordinates(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                Block block = getWorld().getBlockAt(mouseCoords);

                if (canPlaceBlock(block)) {
                    block.setType(getToolbar().getSelectedObj().getType());
                    getToolbar().getSelectedObj().modifyAmount(-1);

                    if (getToolbar().getSelectedObj().getAmount() <= 0) {
                        getInventory().setAt(getToolbar().getCurrentIndex(), 0, new MaterialStack(Material.AIR, 0));
                    }
                }
            }
        }

        /*
         * To do: Clean up all of this to match new use of rectangles
         */

        private void updateMovement(GameTime gameTime) {
            Util util = new Util();
            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.A) || kbState.IsKeyDown(Keys.S)) {
                
                //Block b = getWorld().getBlockAt(util.getCoordinates(new Vector2(this.ScreenPosition.Left, this.ScreenPosition.Bottom)));
                Block t = getWorld().getBlockAt(Util.getCoordinates(new Vector2(this.ScreenPosition.Left, this.ScreenPosition.Center.Y)));

                if (!t.getType().isSolid()) {
                    speed.X = MOVEMENT_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    DIRECTION.X = LEFT;
                }
                else {
                    speed.X = 0;
                }
            }
            else if (kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.D)) {

                //Block b = getWorld().getBlockAt(Util.getCoordinates(new Vector2(this.ScreenPosition.Right, this.ScreenPosition.Bottom)));
                //Block t = getWorld().getBlockAt(Util.getCoordinates(new Vector2(this.ScreenPosition.Right, this.ScreenPosition.Center.Y)));

                Block nextBlock = getWorld().getBlockAt(Util.getCoordinates(new Vector2(this.ScreenPosition.Right, this.ScreenPosition.Center.Y)));

                if (!nextBlock.getType().isSolid()) {
                    speed.X = 300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    DIRECTION.X = RIGHT;
                }
                else {
                    speed.X = 0;
                    DIRECTION.X = STABLE;
                }
            }
            else {
                DIRECTION.X = STABLE;
                speed.X = 0;
            }

            Location leftFoot = Util.getCoordinates(new Vector2(ScreenPosition.Left, ScreenPosition.Bottom));
            Location rightFoot = Util.getCoordinates(new Vector2(ScreenPosition.Right, ScreenPosition.Bottom));
            Location rightEar = Util.getCoordinates(new Vector2(ScreenPosition.Right, ScreenPosition.Top));
            Location leftEar = Util.getCoordinates(new Vector2(ScreenPosition.Left, ScreenPosition.Top));
            Location above = Util.getCoordinates(new Vector2(ScreenPosition.Center.X, ScreenPosition.Top));

            if (moveState != MovementState.JUMPING) {
                if((getWorld().getBlockAt(leftFoot).getType() is Air && getWorld().getBlockAt(rightFoot).getType() is Air) /*|| coordinates.getY() % 1 != 0*/){ 
                    speed.Y = -4;
                }
                else if (moveState == MovementState.WALKING && Keyboard.GetState().IsKeyDown(Keys.Space) && !(getWorld().getBlockAt(leftFoot).getType() is Air) && !(getWorld().getBlockAt(rightFoot).getType() is Air) && !getWorld().getBlockAt(above).getType().isSolid()) {
                    speed.Y = 4;
                    DIRECTION.Y = UP;
                    moveState = MovementState.JUMPING;
                    originalY = coordinates.getY();
                }
                else {
                    speed.Y = 0;
                }

            }

            if (moveState == MovementState.JUMPING && ((coordinates.getY() - this.originalY >= JUMP_HEIGHT) || !(getWorld().getBlockAt(above).getType() is Air))) {
                moveState = MovementState.WALKING;
                originalY = 0;
            }

            oldX = currentX;
            oldY = currentY;

            currentX += speed.X * DIRECTION.X;
            currentY += speed.Y * DIRECTION.Y;

            coordinates.modifyX((currentX - oldX) / 32);
            coordinates.modifyY((currentY - oldY) / 32);
        }

        private void updateCollision() {
            if (DIRECTION.X == RIGHT) {
                if (speed.Y > 0) {
                    if (DIRECTION.Y == UP) {
                        Block above = getWorld().getBlockAt(new Location(coordinates.getX(), coordinates.getY() + 1));
                        Block front = getWorld().getBlockAt(Util.getCoordinates(new Vector2(this.ScreenPosition.Right, this.ScreenPosition.Center.Y)));
                        Block topright = getWorld().getBlockAt(new Location(coordinates.getX() + 1, coordinates.getY() + 1));

                        if((getBounds().Intersects(above.getBounds()) && above.getType().isSolid()) || (getBounds().Intersects(front.getBounds()) && front.getType().isSolid()) || (topright.getBounds().Intersects(getBounds()) && topright.getType().isSolid())){ 
                            moveState = MovementState.WALKING;
                            speed.X = 0;
                            coordinates.modifyX(-0.5);
                        }
                    }
                    else if(DIRECTION.Y == DOWN){
                        Block below = getWorld().getBlockAt(new Location(coordinates.getX(), coordinates.getY() - 1));
                        Block front = getWorld().getBlockAt(Util.getCoordinates(new Vector2(this.ScreenPosition.Right, this.ScreenPosition.Center.Y)));
                        Block bottomright = getWorld().getBlockAt(new Location(coordinates.getX() + 1, coordinates.getY() - 1));

                        if(getBounds().Intersects(below.getBounds()) || getBounds().Intersects(front.getBounds()) || bottomright.getBounds().Intersects(getBounds())){ 
                            speed = Vector2.Zero;
                        }
                    }
                }else{
                    Block front = getWorld().getBlockAt(new Location(coordinates.getX() + 1, coordinates.getY()));

                    if(getBounds().Intersects(front.getBounds())){
                        speed.X = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            //spriteBatch.Draw(texture, ScreenPosition, Color.White);
            Screen.render(coordinates, texture, 32, 32, false);
            spriteBatch.Draw(getInventory().getAt(getToolbar().getCurrentIndex(), 0).getType().getTexture(), new Vector2(ScreenPosition.X + 20, ScreenPosition.Y + 10), Color.White);
            toolbar.Draw(spriteBatch);
        }

        public PlayerInventory getInventory() {
            return this.inventory;
        }

        public Toolbar getToolbar() {
            return this.toolbar;
        }

        public World getWorld() {
            return SideCraft.worlds[world];
        }

        private bool canPlaceBlock(Block block) {
            return !(getToolbar().getSelectedObj().getType() is Air) && block.getType() is Air;
        }

        private Rectangle getBounds() {
            return ScreenPosition;
        }
    }
}
