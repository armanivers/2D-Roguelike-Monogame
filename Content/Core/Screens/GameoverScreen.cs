#region File Description

//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

namespace _2DRoguelike.Content.Core.Screens
{
    internal class GameoverScreen : MenuScreen
    {
        #region Initialization

        public GameoverScreen()  : base("Game Over")
        {
            notEscapable = true;
            // Create our menu entries.
            MenuEntry newGame = new MenuEntry("Start New Game");
            MenuEntry mainMenu = new MenuEntry("Return To Menu");

            // Hook up menu event handlers.
            newGame.Selected += StartNewGame;
            mainMenu.Selected += ReturnToMainMenu;

            // Add entries to the menu.
            MenuEntries.Add(newGame);
            MenuEntries.Add(mainMenu);
        }

        #endregion Initialization

        #region Handle Input

        private void StartNewGame(object sender, PlayerIndexEventArgs e)
        {
            //ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
            LoadingScreen.Load(ScreenManager, false, e.PlayerIndex, new GameplayScreen());
        }

        private void ReturnToMainMenu(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }

        #endregion Handle Input
    }
}