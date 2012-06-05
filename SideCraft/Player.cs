using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SideCraft;
using SideCraft.menu;
using SideCraft.inventory;
using SideCraft.material;

namespace SideCraft {
    public class Player:entity.Entity {

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

        const double JUMP_HEIGHT = 1.3;
        const int LEFT = -1, UP = 1, RIGHT = 1, DOWN = -1, STABLE = 0, ARM_LENGTH = 4;

        private int MOVEMENT_SPEED;
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
            MOVEMENT_SPEED = Settings.BLOCK_SIZE * 10;
            ScreenPosition = new Rectangle(388, 224, Settings.BLOCK_SIZE, Settings.BLOCK_SIZE);

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

        public void update(GameTime gameTime) {
            updateStates();
            updateMovement(gameTime);
            updateCollision();
            updateInteraction(gameTime);
            updateToolbar();
            updatePosition();
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
                Location mouseCoords = Location.valueOf(Mouse.GetState().X, Mouse.GetState().Y);

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
                Location mouseCoords = Location.valueOf(Mouse.GetState().X, Mouse.GetState().Y);
                Block block = getWorld().getBlockAt(mouseCoords);

                if (canPlaceBlock(block)) {
                    block.setType(getToolbar().getSelectedObj().getType());
                    getToolbar().getSelectedObj().modifyAmount(-1);

                    if (getToolbar().getSelectedObj().getAmount() <= 0) {
                        getInventory().setAt(getToolbar().getCurrentIndex(), 0, null);
                    }
                }
            }
        }

        /*
         * To do: Clean up all of this to match new use of rectangles
         */

        private void updateMovement(GameTime gameTime) {
            if (kbState.IsKeyDown(Keys.A) || kbState.IsKeyDown(Keys.S)) {
                speed.X = MOVEMENT_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                DIRECTION.X = LEFT;
            }
            else if (kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.D)) {
                speed.X = MOVEMENT_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                DIRECTION.X = RIGHT;
            }
            else {
                DIRECTION.X = STABLE;
                speed.X = 0;
            }

            float left = ScreenPosition.Left;
            float right = ScreenPosition.Right;

            Location leftFoot = Location.valueOf(left + Settings.BLOCK_SIZE/8, ScreenPosition.Bottom);
            Location rightFoot = Location.valueOf(right - Settings.BLOCK_SIZE / 8, ScreenPosition.Bottom);
            Location rightEar = Location.valueOf(right - Settings.BLOCK_SIZE / 8, ScreenPosition.Top);
            Location leftEar = Location.valueOf(left + Settings.BLOCK_SIZE / 8, ScreenPosition.Top);
            Location above = Location.valueOf(ScreenPosition.Center.X, ScreenPosition.Top + Settings.BLOCK_SIZE / 8);

            if (moveState == MovementState.WALKING) {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && (getWorld().getBlockAt(leftFoot).getType().isSolid()) && (getWorld().getBlockAt(rightFoot).getType().isSolid()) && !getWorld().getBlockAt(above).getType().isSolid()) {
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
                moveState = MovementState.FALLING;
                speed.Y = -4;
                originalY = 0;
            }
        }

        private void updateCollision() {
            Location leftFoot = Location.valueOf(ScreenPosition.Left + Settings.BLOCK_SIZE / 16, ScreenPosition.Bottom);
            Location rightFoot = Location.valueOf(ScreenPosition.Right - Settings.BLOCK_SIZE / 16, ScreenPosition.Bottom);

            Block left = getWorld().getBlockAt(leftFoot);
            Block right = getWorld().getBlockAt(rightFoot);

            if (moveState == MovementState.WALKING) {
                if ((!left.getType().isSolid() && !right.getType().isSolid()) /*|| coordinates.getY() % 1 != 0*/) {
                    speed.Y = -4;
                    moveState = MovementState.FALLING;
                }
            }
            else if (moveState == MovementState.FALLING) {
                if (left.getType().isSolid() || right.getType().isSolid()) {
                    speed.Y = 0;
                    moveState = MovementState.WALKING;
                }
            }

            Block sideBlock, topBlock, botBlock;
            Location top, side, bottom; 

            if (DIRECTION.X != STABLE) {
                if (DIRECTION.X == RIGHT) {
                    side = Location.valueOf(getBounds().Right + Settings.BLOCK_SIZE / 8, getBounds().Center.Y);
                    top = Location.valueOf(getBounds().Right + Settings.BLOCK_SIZE / 8, getBounds().Top);
                    bottom = Location.valueOf(getBounds().Right + Settings.BLOCK_SIZE / 8, getBounds().Bottom);
                }
                else { //left
                    side = Location.valueOf(getBounds().Left - Settings.BLOCK_SIZE / 8, getBounds().Center.Y);
                    top = Location.valueOf(getBounds().Left - Settings.BLOCK_SIZE / 8, getBounds().Top);
                    bottom = Location.valueOf(getBounds().Left - Settings.BLOCK_SIZE / 8, getBounds().Bottom);
                }

                sideBlock = getWorld().getBlockAt(side);
                topBlock = getWorld().getBlockAt(top);
                botBlock = getWorld().getBlockAt(bottom);

                if (sideBlock.getType().isSolid() || topBlock.getType().isSolid() || (botBlock.getType().isSolid() && moveState == MovementState.FALLING)) {
                    DIRECTION.X = 0;
                }
            }
        }

        private void updatePosition() {
            oldX = currentX;
            oldY = currentY;

            currentX += speed.X * DIRECTION.X;
            currentY += speed.Y * DIRECTION.Y;

            coordinates.modifyX((currentX - oldX) / 32);
            coordinates.modifyY((currentY - oldY) / 32);
        }

        public void Draw(SpriteBatch spriteBatch) {
            Screen.render(coordinates, texture, Settings.BLOCK_SIZE, Settings.BLOCK_SIZE, false);
            Screen.render(new Rectangle(ScreenPosition.Right, ScreenPosition.Top, 16, 16),getInventory().getAt(getToolbar().getCurrentIndex(), 0).getType().getTexture());
            toolbar.Draw();
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

        public Rectangle getBounds() {
            return ScreenPosition;
        }

        public Location getLocation() {
            return this.coordinates;
        }

        public void draw() {
            Draw(SideCraft.spriteBatch);
        }

        public void destroy() {
            //Todo
        }
    }
}
