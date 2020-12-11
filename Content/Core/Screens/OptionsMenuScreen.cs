using System;
using System.Diagnostics;

namespace _2DRoguelike.Content.Core.Screens
{

    internal class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        private MenuEntry ungulateMenuEntry;
        private MenuEntry languageMenuEntry;
        private MenuEntry elfMenuEntry;

        private MenuEntry fullscreenMenuEntry;
        
        //bg music
        private MenuEntry backgroundMusicLevel;

        //sfx 
        private MenuEntry soundeffectsLevel;

        private MenuEntry soundOptions;

        // Debug
        private MenuEntry debugMode;

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

            // bg music
            backgroundMusicLevel = new MenuEntry(string.Empty);
            // sfx
            soundeffectsLevel = new MenuEntry(string.Empty);
            soundOptions = new MenuEntry(string.Empty);

            // debug mode
            debugMode = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            ungulateMenuEntry.Selected += UngulateMenuEntrySelected;
            languageMenuEntry.Selected += LanguageMenuEntrySelected;
            elfMenuEntry.Selected += ElfMenuEntrySelected;

            fullscreenMenuEntry.Selected += FrobnicateMenuEntrySelected;

            backgroundMusicLevel.Selected += EnableDisableBackgroundMusic;
            soundeffectsLevel.Selected += EnableDisableSoundeffects;
            soundOptions.Selected += OnSoundOptionsSelected;

            debugMode.Selected += SwitchDebugMode;

            back.Selected += OnCancel;

            // Add entries to the menu.
            //MenuEntries.Add(ungulateMenuEntry);
            //MenuEntries.Add(languageMenuEntry);
            //MenuEntries.Add(elfMenuEntry);

            MenuEntries.Add(fullscreenMenuEntry);

            // music
            MenuEntries.Add(backgroundMusicLevel);
            MenuEntries.Add(soundeffectsLevel);

            // debug mode
            MenuEntries.Add(debugMode);

            // sound options
            MenuEntries.Add(soundOptions);
        }

        private void SetMenuEntryText()
        {
            ungulateMenuEntry.Text = "Preferred ungulate: " + currentUngulate;
            languageMenuEntry.Text = "Language: " + languages[currentLanguage];
            fullscreenMenuEntry.Text = "Fullscreen: " + (GameSettings.fullScreen ? "on" : "off");
            elfMenuEntry.Text = "elf: " + elf;
            backgroundMusicLevel.Text = "Background Music: " + (GameSettings.BackgroundMusicEnabled() ? "on" : "off");
            soundeffectsLevel.Text = "SoundEffects: " + (GameSettings.SoundEffectsEnabled() ? "on" : "off");
            soundOptions.Text = "Sound Options";
            debugMode.Text = "Debug Mode: " + (GameSettings.DEBUG ? "on" : "off");
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

        private void EnableDisableBackgroundMusic(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.MuteUnmuteBackgroundMusic();
            SetMenuEntryText();
        }

        private void EnableDisableSoundeffects(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.MuteUnmuteSoundEffects();
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

        private void OnSoundOptionsSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new SoundOptionsMenuScreen(), e.PlayerIndex);
        }

        private void SwitchDebugMode(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.SwitchDebugMode();
            SetMenuEntryText();
        }
        private void ElfMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            elf++;

            SetMenuEntryText();
        }

        #endregion Handle Input
    }
}