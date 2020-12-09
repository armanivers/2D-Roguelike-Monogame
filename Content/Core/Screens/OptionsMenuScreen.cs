#region File Description

//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using System;
using System.Diagnostics;

namespace _2DRoguelike.Content.Core.Screens
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    internal class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        private MenuEntry ungulateMenuEntry;
        private MenuEntry languageMenuEntry;
        private MenuEntry elfMenuEntry;

        private MenuEntry fullscreenMenuEntry;
        
        //bg music
        private MenuEntry backgroundMusicLevel;
        private MenuEntry bgDecrease;
        private MenuEntry bgIncrease;

        //sfx 
        private MenuEntry soundeffectsLevel;
        private MenuEntry sfxDecrease;
        private MenuEntry sfxIncrease;

        private enum Ungulate
        {
            BactrianCamel,
            Dromedary,
            Llama,
        }

        private static Ungulate currentUngulate = Ungulate.Dromedary;

        private static string[] languages = { "C#", "French", "Deoxyribonucleic acid" };
        private static int currentLanguage = 0;
        
        private static int elf = 23;

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            ungulateMenuEntry = new MenuEntry(string.Empty);
            languageMenuEntry = new MenuEntry(string.Empty);
            elfMenuEntry = new MenuEntry(string.Empty);

            fullscreenMenuEntry = new MenuEntry(string.Empty);

            //bg music
            backgroundMusicLevel = new MenuEntry(string.Empty);
            bgIncrease = new MenuEntry(string.Empty);
            bgDecrease = new MenuEntry(string.Empty);

            //sfx
            soundeffectsLevel = new MenuEntry(string.Empty);
            sfxIncrease = new MenuEntry(string.Empty);
            sfxDecrease = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            ungulateMenuEntry.Selected += UngulateMenuEntrySelected;
            languageMenuEntry.Selected += LanguageMenuEntrySelected;
            elfMenuEntry.Selected += ElfMenuEntrySelected;

            fullscreenMenuEntry.Selected += FrobnicateMenuEntrySelected;

            //bg music
            bgDecrease.Selected += DecreaseBackgroundMusicLevel;
            bgIncrease.Selected += IncreaseBackgroundMusicLevel;

            //sfx
            sfxDecrease.Selected += DecreaseSoundEffectsLevel;
            sfxIncrease.Selected += IncreaseSoundEffectsLevel;

            back.Selected += OnCancel;

            // Add entries to the menu.
            //MenuEntries.Add(ungulateMenuEntry);
            //MenuEntries.Add(languageMenuEntry);
            //MenuEntries.Add(elfMenuEntry);

            MenuEntries.Add(fullscreenMenuEntry);

            //bg music
            MenuEntries.Add(backgroundMusicLevel);
            MenuEntries.Add(bgDecrease);
            MenuEntries.Add(bgIncrease);

            //sfx
            MenuEntries.Add(soundeffectsLevel);
            MenuEntries.Add(sfxDecrease);
            MenuEntries.Add(sfxIncrease);
        }

        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        private void SetMenuEntryText()
        {
            ungulateMenuEntry.Text = "Preferred ungulate: " + currentUngulate;
            languageMenuEntry.Text = "Language: " + languages[currentLanguage];
            fullscreenMenuEntry.Text = "Fullscreen: " + (GameSettings.fullScreen ? "on" : "off");
            elfMenuEntry.Text = "elf: " + elf;
            backgroundMusicLevel.Text = String.Format("Background Music: {0:0} %", GameSettings.backgroundMusicLevel*100);
            soundeffectsLevel.Text = String.Format("Soundeffects: {0:0} %", GameSettings.soundeffectsLevel*100);
            bgDecrease.Text = "-";
            bgIncrease.Text = "+";
            sfxDecrease.Text = "-";
            sfxIncrease.Text = "+";
        }

        #endregion Initialization

        #region Handle Input

        /// <summary>
        /// Event handler for when the Ungulate menu entry is selected.
        /// </summary>
        private void UngulateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentUngulate++;

            if (currentUngulate > Ungulate.Llama)
                currentUngulate = 0;

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        private void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentLanguage = (currentLanguage + 1) % languages.Length;

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        private void FrobnicateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (GameSettings.fullScreen)
            {
                GameSettings.SetWindowedMode();
            }
            else
            {
                GameSettings.SetFullscreen();
            }
            SetMenuEntryText();
        }

        private void DecreaseBackgroundMusicLevel(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.DecreaseBackgroundMusic();
            SetMenuEntryText();
        }

        private void IncreaseBackgroundMusicLevel(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.IncreaseBackgroundMusic();
            SetMenuEntryText();
        }

        private void DecreaseSoundEffectsLevel(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.DecreaseSoundEffectLevel();
            SetMenuEntryText();
        }
        private void IncreaseSoundEffectsLevel(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.IncreaseSoundEffectLevel();
            SetMenuEntryText();

        }

        /// <summary>
        /// Event handler for when the Elf menu entry is selected.
        /// </summary>
        private void ElfMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            elf++;

            SetMenuEntryText();
        }

        #endregion Handle Input
    }
}