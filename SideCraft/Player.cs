using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SideCraft;
using SideCraft.Terrain.MaterialData;
using SideCraft.UI;
using SideCraft.Inventory;
using SideCraft.Material;

namespace SideCraft {
    public class Player {
        public Vector2 MapPosition;
        public Vector2 startMapPosition;
        public Vector2 ScreenPosition;

        private Toolbar toolbar;
        private PlayerInventory inventory;

        private Texture2D texture;
        private Vector2 speed;

        private static Int64 milliseconds = 0;
        private float interval = 250f;

        private double originalY;

        private Rectangle recPos;

        private MovementState moveState;
   

        const double JUMP_HEIGHT = 1;

        private static MouseState oldState;

        public World world;

        public Coordinates coordinates = new Coordinates(0, 0);

        enum MovementState {
            Walking,
            Jumping,
            Falling
        }

        enum actionState {
            Idle,
            Breaking
        }

        public Player(Vector2 p) {
            MapPosition = p;
            startMapPosition = p;
            ScreenPosition = new Vector2(384, 160);
            oldState = Mouse.GetState();
            toolbar = new Toolbar();
            inventory = new PlayerInventory();
            moveState = MovementState.Walking;

            recPos = new Rectangle((int)startMapPosition.X, (int)startMapPosition.Y, 32, 64);
        }

        public void LoadContent(Texture2D t) {
            texture = t;
        }

        public void Update(GameTime gameTime) {
            updateMovement(gameTime);
            updateInteraction(gameTime);
            updateToolbar();
        }

        private void updateToolbar() {
            if (Mouse.GetState().ScrollWheelValue > oldState.ScrollWheelValue) {
                getToolbar().setCurrentIndex(getToolbar().getCurrentIndex() + 1);
            }
        }

        private void updateInteraction(GameTime gameTime) {
            Util util = new Util();
            KeyboardState kbState = Keyboard.GetState();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                Coordinates mouseCoords = util.getCoordinates(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

                milliseconds += (long)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (milliseconds >= interval) {
                    milliseconds = 0;

                    if (Math.Abs(mouseCoords.getX() - coordinates.getX()) <= 4 && Math.Abs(mouseCoords.getY() - coordinates.getY()) <= 4) {
                        Block block = world.getBlockAt(mouseCoords);

                        if (block.getType() is Air) return;

                        world.getBlockAt(mouseCoords).getType().damage(5);

                        if (block.getType().getCurrentDurability() <= 0) {
                            block.setType(new Air());
                        }
                    }
                }
            }
            else if (Mouse.GetState().RightButton == ButtonState.Pressed) {
                Coordinates mouseCoords = util.getCoordinates(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                Block block = world.getBlockAt(mouseCoords);

                if (block.getType() is Air && block != world.getBlockAt(this.coordinates) && block != world.getBlockAt(new Coordinates(coordinates.getX(), coordinates.getY() + 1))) {
                    block.setType(new Stone());
                }
            }
        }

        private void updateMovement(GameTime gameTime) {
            Util util = new Util();
            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.A) || kbState.IsKeyDown(Keys.S)) {
                speed.X = -300 * (float)gameTime.ElapsedGameTime.TotalSeconds;

                Block feet = world.getBlockAt(new Coordinates(Math.Ceiling(coordinates.getX() - 1.15), coordinates.getY()));
                Block head = world.getBlockAt(new Coordinates(Math.Ceiling(coordinates.getX() - 1.15), coordinates.getY() + 1));

                if (feet.getTypeId() != Game1.AIR || head.getTypeId() != Game1.AIR) {
                    speed.X = 0;
                }
            }
            else if (kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.D)) {
                speed.X = 300 * (float)gameTime.ElapsedGameTime.TotalSeconds;

                Block feet = world.getBlockAt(new Coordinates(Math.Floor(coordinates.getX() + 0.7), coordinates.getY()));
                Block head = world.getBlockAt(new Coordinates(Math.Floor(coordinates.getX() + 0.7), Math.Floor(coordinates.getY() + 1)));
       
                if (feet.getTypeId() != Game1.AIR || head.getTypeId() != Game1.AIR) {
                    speed.X = 0;
                }
            }
            else
                speed.X = 0;

            Coordinates leftFoot = new Coordinates(coordinates.getX(), Math.Ceiling(coordinates.getY() - 1));
            Coordinates rightFoot = new Coordinates(coordinates.getX() + 0.5, Math.Ceiling(coordinates.getY() - 1));
            Coordinates rightEar = new Coordinates(coordinates.getX() + 0.5, Math.Floor(coordinates.getY() + 2));
            Coordinates leftEar = new Coordinates(coordinates.getX(), Math.Floor(coordinates.getY() + 2));

            if (moveState != MovementState.Jumping) {

                //if (world.getBlockAt(new Coordinates(coordinates.getX(), Math.Ceiling(coordinates.getY()) - 1)).getType() is Air) {
                if((world.getBlockAt(leftFoot).getType() is Air && world.getBlockAt(rightFoot).getType() is Air) || coordinates.getY() % 1 != 0){ 
                    speed.Y = -4;
                }
                else if (moveState == MovementState.Walking && Keyboard.GetState().IsKeyDown(Keys.Space) && !(world.getBlockAt(leftFoot).getType() is Air) && !(world.getBlockAt(rightFoot).getType() is Air)) {
                    speed.Y = 4;
                    moveState = MovementState.Jumping;
                    originalY = coordinates.getY();
                }
                else {
                    speed.Y = 0;
                }

            }

            if (moveState == MovementState.Jumping && ((coordinates.getY() - this.originalY >= JUMP_HEIGHT) || !(world.getBlockAt(rightEar).getType() is Air) || !(world.getBlockAt(leftEar).getType() is Air))) {
                moveState = MovementState.Walking;
                originalY = 0;
            }

               // MapPosition += speed;
            recPos.X += (int)speed.X;
            recPos.Y += (int)speed.Y;

            //coordinates.setX((MapPosition.X - startMapPosition.X) / 32);
            //coordinates.setY((MapPosition.Y - startMapPosition.Y) / 32);
            coordinates.setX((recPos.X - startMapPosition.X) / 32);
            coordinates.setY((recPos.Y - startMapPosition.Y) / 32);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, ScreenPosition, Color.White);
            spriteBatch.Draw(getInventory().getAt(0, getToolbar().getCurrentIndex()).getType().getTexture(), new Vector2(ScreenPosition.X + 20, ScreenPosition.Y + 10), Color.White);
            //spriteBatch.Draw(Game1.selectionTile, recPos, Color.Red);
            toolbar.Draw(spriteBatch);
        }

        public PlayerInventory getInventory() {
            return this.inventory;
        }

        public Toolbar getToolbar() {
            return this.toolbar;
        }
    }
}
