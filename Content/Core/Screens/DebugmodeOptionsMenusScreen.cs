using System;
using System.Diagnostics;

namespace _2DRoguelike.Content.Core.Screens
{
    internal class DebugmodeOptionsMenusScreen : MenuScreen
    {
        private MenuEntry debugMode;
        private MenuEntry godMode;
        private MenuEntry showHitbox;
        private MenuEntry showMouse;
        private MenuEntry playerDebug;
        private MenuEntry attackHitbox;


        /// <summary>
        /// Constructor.
        /// </summary>
        public DebugmodeOptionsMenusScreen()
            : base("Debug Options")
        {

            debugMode = new MenuEntry(string.Empty);
            godMode = new MenuEntry(string.Empty);
            showHitbox = new MenuEntry(string.Empty);
            showMouse = new MenuEntry(string.Empty);
            playerDebug = new MenuEntry(string.Empty);
            //attackHitbox = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            debugMode.Selected += SwitchDebugMode;
            godMode.Selected += SwitchGodMode;
            showHitbox.Selected += SwitchShowHitbox;
            showMouse.Selected += SwitchShowMouse;
            playerDebug.Selected += SwitchShowPlayerDebug;
            //attackHitbox.Selected += SwitchShowAttackHitbox;

            back.Selected += OnCancel;

            MenuEntries.Add(debugMode);
            MenuEntries.Add(godMode);
            MenuEntries.Add(showHitbox);
            MenuEntries.Add(showMouse);
            MenuEntries.Add(playerDebug);
            //MenuEntries.Add(attackHitbox);
        }

        private void SetMenuEntryText()
        {
            debugMode.Text = "Debug Mode: " + (GameSettings.DEBUG ? "on" : "off");
            godMode.Text = "God Mode: " + (GameSettings.godMode ? "on" : "off");
            showHitbox.Text = "Show Hitbox: " + (GameSettings.showHitbox ? "on" : "off");
            showMouse.Text = "Show Mouse Debug: " + (GameSettings.showMouse ? "on" : "off");
            playerDebug.Text = "Show Player Debug: " + (GameSettings.playerDebug ? "on" : "off");
            //attackHitbox.Text = "Show Attackhitbox: " + (GameSettings.attackHitbox ? "on" : "off");
        }

        private void SwitchGodMode(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.SwitchGodMode();
            SetMenuEntryText();
        }

        private void SwitchShowHitbox(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.SwitchShowHitbox();
            SetMenuEntryText();
        }

        private void SwitchShowMouse(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.SwitchShowMouse();
            SetMenuEntryText();
        }
        private void SwitchShowPlayerDebug(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.SwitchPlayerDebug();
            SetMenuEntryText();

        }

        private void SwitchShowAttackHitbox(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.SwitchAttackHitox();
            SetMenuEntryText();

        }

        private void SwitchDebugMode(object sender, PlayerIndexEventArgs e)
        {
            GameSettings.SwitchDebugMode();
            SetMenuEntryText();
        }
    }
}