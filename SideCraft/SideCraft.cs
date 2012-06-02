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
    public class SideCraft : Microsoft.Xna.Framework.Game {

        public const int GRASS = 0;
        public const int DIRT = 1;
        public const int STONE = 2;
        public const int IRON_ORE = 3;
        public const int AIR = 4;

        public static Player player;

        public static SpriteFont font;

        public static ContentManager content;

        public static Texture2D grassTile, stoneTile, dirtTile, iron_oreTile, airTile, obsidianTile;
        public static Texture2D selectionTile, toolbarTile;

        public static Dictionary<String, World> worlds = new Dictionary<String, World>();

        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public SideCraft() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            base.Initialize();
        }

        protected override void LoadContent() {
            content = Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            

            font = Content.Load<SpriteFont>("CourierNew");

            stoneTile = Content.Load<Texture2D>("stone");
            grassTile = Content.Load<Texture2D>("grass");
            dirtTile = Content.Load<Texture2D>("dirt");
            iron_oreTile = Content.Load<Texture2D>("iron_ore");
            airTile = Content.Load<Texture2D>("air");
            obsidianTile = Content.Load<Texture2D>("obsidian");

            player = new Player();
            player.LoadContent(Content.Load<Texture2D>("steve"));

            World world = new World("world");
            worlds.Add(world.getName(), world);
            player.world = world.getName();

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
            Location loc = new Location(0, 1);
            float t = Util.getPosition(loc).Y;


            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.getWorld().update();

            Location mouseCoords = Util.getCoordinates(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            Location mouseroofCoords = new Location(mouseCoords.getX(), Math.Ceiling(mouseCoords.getY()));
            if (Math.Abs(mouseCoords.getX() - player.coordinates.getX()) <= 4 && Math.Abs(mouseCoords.getY() - player.coordinates.getY()) <= 4)
                Screen.render(player.getWorld().getBlockAt(mouseCoords).getLocation(), selectionTile, 32, 32, false);

            spriteBatch.DrawString(font, "X: " + ((player.coordinates.getX())).ToString() + Environment.NewLine + "Y: " + player.coordinates.getY().ToString() + Environment.NewLine + "Mouse X: " + mouseCoords.getX() + Environment.NewLine + "Mouse Y: " + mouseCoords.getY() + Environment.NewLine + "MousePos.X: " + Mouse.GetState().X + Environment.NewLine + Mouse.GetState().Y + Environment.NewLine + "Y conv" + Util.getPosition(mouseCoords).ToString(), new Vector2(10, 10), Color.Black) ;
            //spriteBatch.Draw(dirtTile, player.ScreenPosition, Color.Black);

            spriteBatch.DrawString(font, player.getWorld().getBlockAt(mouseCoords).getTypeId().ToString(), Util.getPosition(mouseCoords), Color.White);
            spriteBatch.DrawString(font, "0", Util.getPosition(new Location(0,0)), Color.White);

            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
