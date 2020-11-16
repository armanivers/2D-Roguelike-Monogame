#region File Description

//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using _2DRoguelike.Content.Core.GameDebug;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Player;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Threading;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using System.Diagnostics;
using _2DRoguelike.Content.Core.UI;

#endregion Using Statements

namespace _2DRoguelike.Content.Core.Screens
{
    internal class GameplayScreen : GameScreen
    {
        #region Fields

        private ContentManager content;

        private Random random = new Random();

        private float pauseAlpha;

    
        #endregion Fields

        #region Initialization

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            // EntityBasis Konstruktor fügt automatisch zur EntityManager.entities hinzu
            new GreenZombie(/*WorldGenerator.spawn*/LevelManager.maps.getSpawnpoint(), 10, 2, 3);
            new Player(LevelManager.maps.getSpawnpoint(), 100, 0.4f, 5);

            UIManager.healthBar = new HealthBar(Player.Instance);


            Thread.Sleep(500);

            ScreenManager.Game.ResetElapsedTime();
        }

        public override void UnloadContent()
        {
            content.Unload();
            // Unload all entities + delete current Player Intance
            EntityManager.unloadEntities();
            Player.Instance.DeleteInstance();
            Camera.Unload();
        }

        #endregion Initialization

       #region Update and Draw

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                // Game 
                Camera.Update(Player.Instance);
                InputController.Update();
                EntityManager.Update(gameTime);
                UIManager.Update(gameTime);
                if (Player.Instance.IsDead())
                {
                    //LoadingScreen.Load(ScreenManager, false, null,new GameoverScreen());
                    ScreenManager.AddScreen(new GameoverScreen(), ControllingPlayer);
                }
            }
        }

        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
               
            }
            else
            {
                

            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,Color.CornflowerBlue, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,Camera.transform);

            LevelManager.Draw(spriteBatch);
            EntityManager.Draw(spriteBatch);
            // GameDebug.GameDebug.hitboxDebug.Draw(spriteBatch);

            spriteBatch.End();

            // seperate sprite batch begin+end which isn't attached to an active 2D Camera
            spriteBatch.Begin();

            // UI Elements
            UIManager.Draw(spriteBatch);
            // GameDebug.GameDebug.playerDebug.Draw(spriteBatch);

            spriteBatch.End();


            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        #endregion Update and Draw
    }
}