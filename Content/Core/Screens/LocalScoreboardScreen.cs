#region File Description

//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace _2DRoguelike.Content.Core.Screens
{
    internal class LocalScoreboardScreen : MenuScreen
    {
        #region Initialization

        public LocalScoreboardScreen() : base("Local Highscore", true, 5)
        {
            notEscapable = true;

            // Create our menu entries.
            MenuEntry switchHighscore = new MenuEntry("Global Highscores");
            MenuEntry mainMenu = new MenuEntry("Main Menu");
            string playername = Game1.gameSettings.playerName;
            Color color;

            for(int i = 0; i < 5; i++)
            {
                //bool even = (i % 2 == 0);
                //(i % 2 == 0) ? (color = Color.Yellow) : (color = Color.Orange);

                if(i%2==0)
                {
                    color = Color.Yellow;
                }
                else
                {
                    color = Color.Orange;
                }
                MenuEntry me = new MenuEntry("", false, color);

                string score = (Game1.gameStats.scores.Count > i) ? ""+Game1.gameStats.scores[i] : "---";
                me.Text = i+". "+playername+": "+score;
                MenuEntries.Add(me);
            }

            // Hook up menu event handlers.
            switchHighscore.Selected += SwitchHighscoreRegion; 
            mainMenu.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(switchHighscore);
            MenuEntries.Add(mainMenu);
        }

        #endregion Initialization

        #region Handle Input

        private void SwitchHighscoreRegion(object sender, PlayerIndexEventArgs e)
        {
            ExitScreen();
            ScreenManager.AddScreen(new GlobalScoreboardScreen(), e.PlayerIndex);
        }


        private void ReturnToMainMenu(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }

        #endregion Handle Input
    }
}