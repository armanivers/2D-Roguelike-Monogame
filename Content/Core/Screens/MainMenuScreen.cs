#region File Description

//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;

#endregion Using Statements

namespace _2DRoguelike.Content.Core.Screens
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    internal class MainMenuScreen : MenuScreen
    {
        #region Initialization

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("Main Menu")
        {

            TransitionOnTime = TimeSpan.FromSeconds(0.8);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);
            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry highscoreMenuEntry = new MenuEntry("Highscores");
            MenuEntry playerStats = new MenuEntry("Player Statistics");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            highscoreMenuEntry.Selected += HighscoresMenuEntrySelected;
            playerStats.Selected += PlayerStatsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(highscoreMenuEntry);
            MenuEntries.Add(playerStats);
            MenuEntries.Add(exitMenuEntry);
        }

        #endregion Initialization

        #region Handle Input

        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,new GameplayScreen());
        }

        private void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        private void HighscoresMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new LocalScoreboardScreen(), e.PlayerIndex);
        }

        private void PlayerStatsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.LoadCustom(ScreenManager, true, e.PlayerIndex, new BackgroundsStatisticsScreen(), new GamestatsScreen());
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit the game?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {

            Game1.gameSettings.SaveSettings();
            Game1.gameStats.SaveStatistics();

            ScreenManager.Game.Exit();
        }

        #endregion Handle Input
    }
}