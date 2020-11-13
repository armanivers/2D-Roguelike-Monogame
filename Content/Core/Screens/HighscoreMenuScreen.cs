#region File Description

//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using System.Diagnostics;
using System.Net;

namespace _2DRoguelike.Content.Core.Screens
{
    internal class HighscoreMenuScreen : MenuScreen
    {
        #region Initialization

        public HighscoreMenuScreen()  : base("Global Leaderboard",true)
        {
            MenuEntry mainMenu = new MenuEntry("Return To Menu");
            GetHighscores();
            mainMenu.Selected += ReturnToMainMenu;
            MenuEntries.Add(mainMenu);
        }

        #endregion Initialization

        #region Handle Input

        private void GetHighscores()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://latenitearii.ddns.net/2dmonogame/highscore");
                dataString = json;
            }
        }

        private void ReturnToMainMenu(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }

        #endregion Handle Input
    }
}