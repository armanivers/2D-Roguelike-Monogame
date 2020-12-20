#region File Description

//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace _2DRoguelike.Content.Core.Screens
{
    internal class GameoverScreen : MenuScreen
    {
        #region Initialization
        MenuEntry score;
        MenuEntry newGame;
        MenuEntry mainMenu;

        private int scoreCounter;
        private int scoreMax;
        private int incrementSpeed;

        public GameoverScreen()  : base("Game Over",true,1,true)
        {
            notEscapable = true;
            scoreCounter = 0;

            scoreMax = StatisticsManager.currentScore.Score;

            DetermineIncrementSpeed();
            // Create our menu entries.
            score = new MenuEntry("Score: " + scoreCounter, false, Color.Yellow) ;
            newGame = new MenuEntry("Start New Game");
            mainMenu = new MenuEntry("Return To Menu");

            // Hook up menu event handlers.
            score.Selectable = false;
            newGame.Selected += StartNewGame;
            mainMenu.Selected += ReturnToMainMenu;

            // Add entries to the menu.
            MenuEntries.Add(score);
            MenuEntries.Add(newGame);
            MenuEntries.Add(mainMenu);
        }

        #endregion Initialization

        #region Handle Input

        public void DetermineIncrementSpeed()
        {
            if(scoreMax < 100)
            {
                incrementSpeed = 1;
            }
            else if(scoreMax < 200)
            {
                incrementSpeed = 2;
            }else
            {
                incrementSpeed = 3;
            }
        }

        public override void CustomUpdate()
        {
            if(scoreCounter < scoreMax)
            {
                score.Text = "Score: " + scoreCounter;
                scoreCounter+=incrementSpeed;
            }
            else
            {
                if(scoreCounter >= Game1.gameStats.scores[0])
                {
                    score.Text = "New Highscore Achieved: " + scoreCounter +" !";
                }
            }
        }

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