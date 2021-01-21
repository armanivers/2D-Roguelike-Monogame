using _2DRoguelike.Content.Core;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Screens;
using _2DRoguelike.Content.Core.Statistics;
using _2DRoguelike.Content.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace _2DRoguelike
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static readonly Random rand = new Random();
        public static readonly String gameName = "Dungeon Explorer";
        // the 3 "../" used to save the settings file in the main direcotry of the game, instead of putting it in bin/Debug/netcoreapp3.1
        //public static string projectPath = "../../../";
        // should be saved in appdata/Roaming of user folder otherwise
        public static string projectPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static GameSettings gameSettings;
        public static GameStatistics gameStats;

        private ScreenManager screenManager;

        private static readonly string[] preloadAssets =
        {
            "gradient",
        };

        private SpriteBatch _spriteBatch;
        public static Game1 Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            gameSettings = new GameSettings();
            gameStats = new GameStatistics();

            _graphics.PreferredBackBufferWidth = gameSettings.screenWidth;
            _graphics.PreferredBackBufferHeight = gameSettings.screenHeight;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Instance = this;

            // Create the screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }

        protected override void Initialize()
        {
            Window.Title = gameName;
            gameSettings.LoadSettings();
            gameStats.LoadStatistics();

            _graphics.PreferredBackBufferWidth = gameSettings.screenWidth;
            _graphics.PreferredBackBufferHeight = gameSettings.screenHeight;
            _graphics.IsFullScreen = gameSettings.fullScreen;
            _graphics.ApplyChanges();

            TextureManager.Load(Content);
            TileTextureManager.Load(Content);
            SoundManager.Load(Content);

            MediaPlayer.Play(SoundManager.MenuMusic);
            MediaPlayer.Volume = Game1.gameSettings.backgroundMusicLevel;
            MediaPlayer.IsRepeating = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
