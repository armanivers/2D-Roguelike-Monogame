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

        /* JSON FORMAT array with objects in the form of "name"="name1","score"="1230"  OPTIONAL: "date"=17122020"
         * {
    "1": {
      "name": "Ari",
      "score": "1230"
    },
    "2": {
      "name": "Klaus",
      "score": "1200"
    },
    "3": {
      "name": "Jurgen",
      "score": "800"
    },
    "4": {
      "name": "Kevin",
      "score": "300"
    },
    "5": {
      "name": "player32",
      "score": "100"
    }
  }
        */

        private void ReturnToMainMenu(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }

        #endregion Handle Input
    }
}