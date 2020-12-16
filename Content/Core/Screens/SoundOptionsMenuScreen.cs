using System;
using System.Diagnostics;

namespace _2DRoguelike.Content.Core.Screens
{
    internal class SoundOptionsMenuScreen : MenuScreen
    {
        //bg music
        private MenuEntry backgroundMusicLevel;

        private MenuEntry bgDecrease;
        private MenuEntry bgIncrease;

        //sfx 
        private MenuEntry soundeffectsLevel;
        private MenuEntry sfxDecrease;
        private MenuEntry sfxIncrease;


        /// <summary>
        /// Constructor.
        /// </summary>
        public SoundOptionsMenuScreen()
            : base("Sound Options")
        {

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

            //bg music
            bgDecrease.Selected += DecreaseBackgroundMusicLevel;
            bgIncrease.Selected += IncreaseBackgroundMusicLevel;

            //sfx
            sfxDecrease.Selected += DecreaseSoundEffectsLevel;
            sfxIncrease.Selected += IncreaseSoundEffectsLevel;

            back.Selected += OnCancel;

            //bg music
            MenuEntries.Add(backgroundMusicLevel);
            MenuEntries.Add(bgDecrease);
            MenuEntries.Add(bgIncrease);

            //sfx
            MenuEntries.Add(soundeffectsLevel);
            MenuEntries.Add(sfxDecrease);
            MenuEntries.Add(sfxIncrease);
        }

        private void SetMenuEntryText()
        {
            backgroundMusicLevel.Text = String.Format("Background Music: {0:0} %", Game1.gameSettings.backgroundMusicLevel*100);
            soundeffectsLevel.Text = String.Format("Soundeffects: {0:0} %", Game1.gameSettings.soundeffectsLevel*100);
            bgDecrease.Text = "-";
            bgIncrease.Text = "+";
            sfxDecrease.Text = "-";
            sfxIncrease.Text = "+";
        }

        private void DecreaseBackgroundMusicLevel(object sender, PlayerIndexEventArgs e)
        {
            Game1.gameSettings.DecreaseBackgroundMusic();
            SetMenuEntryText();
        }

        private void IncreaseBackgroundMusicLevel(object sender, PlayerIndexEventArgs e)
        {
            Game1.gameSettings.IncreaseBackgroundMusic();
            SetMenuEntryText();
        }

        private void DecreaseSoundEffectsLevel(object sender, PlayerIndexEventArgs e)
        {
            Game1.gameSettings.DecreaseSoundEffectLevel();
            SetMenuEntryText();
        }
        private void IncreaseSoundEffectsLevel(object sender, PlayerIndexEventArgs e)
        {
            Game1.gameSettings.IncreaseSoundEffectLevel();
            SetMenuEntryText();

        }
    }
}