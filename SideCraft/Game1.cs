using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SideCraft;
using SideCraft.UI;

namespace SideCraft {
    public class Game1 : Microsoft.Xna.Framework.Game {

        public const int GRASS = 0;
        public const int DIRT = 1;
        public const int STONE = 2;
        public const int IRON_ORE = 3;
        public const int AIR = 4;

        public static Player player;

        private SpriteFont font;

        public static Texture2D grassTile, stoneTile, dirtTile, iron_oreTile, airTile;
        public static Texture2D selectionTile, toolbarTile;

        public static Dictionary<String, World> worlds = new Dictionary<String, World>();

        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
       
        protected override void Initialize() {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("CourierNew");

            player = new Player();
            player.LoadContent(Content.Load<Texture2D>("steve"));

            World world = new World(new Dictionary<string,Block>(), "world");
            worlds.Add(world.getName(), world);
            player.world = world;

            stoneTile = Content.Load<Texture2D>("stone");
            grassTile = Content.Load<Texture2D>("grass");
            dirtTile = Content.Load<Texture2D>("dirt");
            iron_oreTile = Content.Load<Texture2D>("iron_ore");
            airTile = Content.Load<Texture2D>("air");

            toolbarTile = Content.Load<Texture2D>("UIContent/toolbar");
            selectionTile = Content.Load<Texture2D>("Mouse/selection");
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    this.Exit();

                player.Update(gameTime);

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                Util util = new Util();

                spriteBatch.Begin();

                player.world.update(spriteBatch);

                Coordinates mouseCoords = util.getCoordinates(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                Coordinates mouseroofCoords = new Coordinates(mouseCoords.getX(), Math.Ceiling(mouseCoords.getY()));
                if (Math.Abs(mouseCoords.getX() - player.coordinates.getX()) <= 4 && Math.Abs(mouseCoords.getY() - player.coordinates.getY()) <= 4)
                    spriteBatch.Draw(selectionTile, player.world.getBlockAt(mouseCoords).location, Color.White);

                //spriteBatch.DrawString(font, "X: " + ((player.coordinates.getX())).ToString() + Environment.NewLine + "Y: " + player.coordinates.getY().ToString() + Environment.NewLine + "Mouse X: " + mouseCoords.getX() + Environment.NewLine + "Mouse Y: " + mouseCoords.getY() + Environment.NewLine + "MousePos.X: " + Mouse.GetState().X + Environment.NewLine + Mouse.GetState().Y, new Vector2(10, 10), Color.Black);
                spriteBatch.DrawString(font, "0", util.getPosition(new Coordinates(0,0)), Color.Black);
                
                player.Draw(spriteBatch);
                spriteBatch.End();
                base.Draw(gameTime);
        }
    }
}
