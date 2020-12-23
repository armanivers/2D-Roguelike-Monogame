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
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
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
using _2DRoguelike.Content.Core.Statistics;

#endregion Using Statements

namespace _2DRoguelike.Content.Core.Screens
{
    internal class GameplayScreen : GameScreen
    {
        #region Fields

        private ContentManager content;

        private Random random = new Random();

        private float pauseAlpha;

        private Gameplay gameplay;

    
        #endregion Fields

        #region Initialization

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(2.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);
            gameplay = new Gameplay();
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameplay.LoadContent();

            Thread.Sleep(500);

            ScreenManager.Game.ResetElapsedTime();
        }

        public override void UnloadContent()
        {
            content.Unload();

            gameplay.UnloadContent();
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

                gameplay.Update(gameTime);

                if (gameplay.gameOver)
                {
                    StatisticsManager.currentScore.ForceCounterUpdate();
                    Game1.gameStats.AddHighscore(StatisticsManager.currentScore);
                    GlobalHighscoreManager.SendHighscoreToServer(Game1.gameSettings.playerName, StatisticsManager.currentScore.Score);
                    //ScreenManager.AddScreen(new GameoverScreen(), ControllingPlayer);
                    LoadingScreen.LoadCustom(ScreenManager, true, null, new BackgroundHighscoreScreen(), new GameoverScreen());
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
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,Color.Black, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            gameplay.Draw(spriteBatch);

            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        #endregion Update and Draw
    }
}