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
    internal class GameoverScreen : MenuScreen
    {
        #region Initialization
        MenuEntry score;
        MenuEntry newGame;
        MenuEntry mainMenu;
        MenuEntry gameoverText;

        private Dictionary<int, string> quotes;

        private int scoreCounter;
        private int scoreMax;
        private int incrementSpeed;

        public GameoverScreen()  : base("Game Over",true,2,true)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            notEscapable = true;
            scoreCounter = 0;

            scoreMax = StatisticsManager.currentScore.Score;

            quotes = new Dictionary<int, string>()
        {
            { 0,"This was the most amazing fight in history!"},
            { 1,"Aww man sucks, lets try a new game?"},
            { 2,"You died for a good cause? I guess..."},
            { 3,"You were a great warrior!"},
            { 4,"Look at your score!"}
        };

            DetermineIncrementSpeed();
            // Create our menu entries.
            score = new MenuEntry("Score: " + scoreCounter, false, Color.Yellow);
            gameoverText = new MenuEntry("", false, Color.Aquamarine);
            gameoverText.Text = SelectGameoverText();
            newGame = new MenuEntry("Start New Game");
            mainMenu = new MenuEntry("Return To Menu");

            // Hook up menu event handlers.
            newGame.Selected += StartNewGame;
            mainMenu.Selected += ReturnToMainMenu;

            // Add entries to the menu.
            MenuEntries.Add(score);
            MenuEntries.Add(gameoverText);
            MenuEntries.Add(newGame);
            MenuEntries.Add(mainMenu);
        }

        #endregion Initialization

        #region Handle Input

        public string SelectGameoverText()
        {
            Random r = new Random();

            int randomQuoteIndex = r.Next(0, quotes.Count);
            Debug.Print("Selected n = " + randomQuoteIndex + " count is = " + quotes.Count);
            return quotes[randomQuoteIndex];
        }


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
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,new GameplayScreen());
        }

        private void ReturnToMainMenu(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.LoadCustom(ScreenManager, true, null, new BackgroundScreen(), new MainMenuScreen());
        }

        #endregion Handle Input
    }
}