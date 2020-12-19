#region File Description

//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using _2DRoguelike.Content.Core.Statistics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Linq;

namespace _2DRoguelike.Content.Core.Screens
{
    internal class GlobalScoreboardScreen : MenuScreen
    {

        List<KeyValuePair<string, int>> scores;

        public GlobalScoreboardScreen() : base("Global Highscore", true, 5)
        {
            notEscapable = true;

            // Create our menu entries.
            MenuEntry switchHighscore = new MenuEntry("Local Highscores");
            MenuEntry mainMenu = new MenuEntry("Main Menu");

            scores = GlobalHighscoreManager.FetchGlobalHighscoreData();

            Color color;

            for (int i = 0; i < 5; i++)
            {
                if (i >= scores.Count)
                {
                    // in case deserialized xml throws an exception, then adjust custom menu entry to i
                    customSelectEntry = i;
                    break;
                }

                //bool even = (i % 2 == 0);
                //(i % 2 == 0) ? (color = Color.Yellow) : (color = Color.Orange);

                if (i % 2 == 0)
                {
                    color = Color.Yellow;
                }
                else
                {
                    color = Color.Orange;
                }
                MenuEntry me = new MenuEntry("", false, color);

                me.Text = i + ". " + scores[i].Key + ": " + scores[i].Value;

                MenuEntries.Add(me);
            }

            // Hook up menu event handlers.
            switchHighscore.Selected += SwitchHighscoreRegion;
            mainMenu.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(switchHighscore);
            MenuEntries.Add(mainMenu);
        }

        private void SwitchHighscoreRegion(object sender, PlayerIndexEventArgs e)
        {
            ExitScreen();
            ScreenManager.AddScreen(new LocalScoreboardScreen(), e.PlayerIndex);
            //LoadingScreen.Load(ScreenManager, false, null, new LocalScoreboardScreen(), new MainMenuScreen());
        }


        private void ReturnToMainMenu(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }

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
    }
}