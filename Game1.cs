using _2DRoguelike.Content.Core;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Player;
using _2DRoguelike.Content.Core.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _2DRoguelike
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
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

            _graphics.PreferredBackBufferWidth = 853;
            _graphics.PreferredBackBufferHeight = 480;
            
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
            base.Draw(gameTime);
        }
    }
}
