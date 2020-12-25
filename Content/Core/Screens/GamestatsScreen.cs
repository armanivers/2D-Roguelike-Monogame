#region File Description

//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _2DRoguelike.Content.Core.Screens
{
    internal class GamestatsScreen : MenuScreen
    {
        #region Initialization
        MenuEntry StatisticsLine1n2;
        MenuEntry StatisticsLine2n3;
        MenuEntry StatisticsLine4;
        MenuEntry mainMenu;
        Color color;
        public GamestatsScreen() : base("Player Statistics", true, 3, true)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);

            StatisticsLine1n2 = new MenuEntry("Items Recieved: " +Game1.gameStats.itemsRecieved + "                          Monsters Killed: " +Game1.gameStats.monstersKilled, false, Color.White);
            StatisticsLine2n3 = new MenuEntry("Times leveled up: " +Game1.gameStats.timesLeveledUp + "                          Loots Opened: " + Game1.gameStats.lootsOpened, false, Color.White);
            StatisticsLine4 = new MenuEntry("Levels Reached: "+ Game1.gameStats.levelsReached, false, Color.White);

            mainMenu = new MenuEntry("Return To Menu");

            mainMenu.Selected += ReturnToMainMenu;

            MenuEntries.Add(StatisticsLine1n2);
            MenuEntries.Add(StatisticsLine2n3);
            MenuEntries.Add(StatisticsLine4);
            MenuEntries.Add(mainMenu);
        }

        #endregion Initialization

        #region Handle Input

      

        public override void CustomUpdate()
        {
            
        }

        private void ReturnToMainMenu(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.LoadCustom(ScreenManager, true, null, new BackgroundScreen(), new MainMenuScreen());
        }

        #endregion Handle Input
    }
}