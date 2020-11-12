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

        private MenuEntry highscoreData;
        private string data;

        public HighscoreMenuScreen()  : base("Game Over")
        {
            MenuEntry mainMenu = new MenuEntry("Return To Menu");
            GetHighscores();
            highscoreData = new MenuEntry(data, true);
            mainMenu.Selected += ReturnToMainMenu;
            MenuEntries.Add(highscoreData);
            MenuEntries.Add(mainMenu);
        }

        #endregion Initialization

        #region Handle Input

        private void GetHighscores()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://latenitearii.ddns.net/dashboard/data");
                data = json;
                Debug.Print(json.ToString());
            }
        }

        private void StartNewGame(object sender, PlayerIndexEventArgs e)
        {
            //ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
        }

        private void ReturnToMainMenu(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }

        #endregion Handle Input
    }
}