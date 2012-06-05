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
using SideCraft.menu;

namespace SideCraft {
    public class SideCraft : Microsoft.Xna.Framework.Game {

        public const int GRASS = 0;
        public const int DIRT = 1;
        public const int STONE = 2;
        public const int IRON_ORE = 3;
        public const int AIR = 4;

        public static Player player;

        public static KeyboardState kbState, oldKbState;

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
            oldKbState = kbState;
            kbState = Keyboard.GetState();
            
            if (kbState.IsKeyDown(Keys.Escape))
                this.Exit();

            if (kbState.IsKeyDown(Keys.F5) && !oldKbState.IsKeyDown(Keys.F5)) {
                if (Settings.DEBUG) {
                    Settings.DEBUG = false;
                }
                else {
                    Settings.DEBUG = true;
                }
            }
            player.getWorld().update(gameTime);
            player.update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CadetBlue);

            player.getWorld().draw();
            player.Draw(spriteBatch);

            Location mouseCoords = Location.valueOf(Mouse.GetState().X, Mouse.GetState().Y);
            Location mouseroofCoords = new Location(mouseCoords.getX(), Math.Ceiling(mouseCoords.getY()));
            
            if (Math.Abs(mouseCoords.getX() - player.coordinates.getX()) <= 4 && Math.Abs(mouseCoords.getY() - player.coordinates.getY()) <= 4)
                Screen.render(player.getWorld().getBlockAt(mouseCoords).getLocation(), selectionTile, Settings.BLOCK_SIZE, Settings.BLOCK_SIZE, false);

            if (Settings.DEBUG) {
                Screen.renderString(font, SideCraft.player.getLocation().toString() + Environment.NewLine + mouseCoords.toString() + Environment.NewLine + "Mouse position" + mouseCoords.toVector2().ToString(), new Vector2(10, 10), Color.Black);
                Screen.renderString(font, "0", new Location(0, 0).toVector2(), Color.White);
                
            }
            base.Draw(gameTime);
        }
    }
}
