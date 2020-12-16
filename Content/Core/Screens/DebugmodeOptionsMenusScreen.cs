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
            debugMode.Text = "Debug Mode: " + (Game1.gameSettings.DEBUG ? "on" : "off");
            godMode.Text = "God Mode: " + (Game1.gameSettings.godMode ? "on" : "off");
            showHitbox.Text = "Show Hitbox: " + (Game1.gameSettings.showHitbox ? "on" : "off");
            showMouse.Text = "Show Mouse Debug: " + (Game1.gameSettings.showMouse ? "on" : "off");
            playerDebug.Text = "Show Player Debug: " + (Game1.gameSettings.playerDebug ? "on" : "off");
            //attackHitbox.Text = "Show Attackhitbox: " + (GameSettings.attackHitbox ? "on" : "off");
        }

        private void SwitchGodMode(object sender, PlayerIndexEventArgs e)
        {
            Game1.gameSettings.SwitchGodMode();
            SetMenuEntryText();
        }

        private void SwitchShowHitbox(object sender, PlayerIndexEventArgs e)
        {
            Game1.gameSettings.SwitchShowHitbox();
            SetMenuEntryText();
        }

        private void SwitchShowMouse(object sender, PlayerIndexEventArgs e)
        {
            Game1.gameSettings.SwitchShowMouse();
            SetMenuEntryText();
        }
        private void SwitchShowPlayerDebug(object sender, PlayerIndexEventArgs e)
        {
            Game1.gameSettings.SwitchPlayerDebug();
            SetMenuEntryText();

        }

        private void SwitchShowAttackHitbox(object sender, PlayerIndexEventArgs e)
        {
            Game1.gameSettings.SwitchAttackHitox();
            SetMenuEntryText();

        }

        private void SwitchDebugMode(object sender, PlayerIndexEventArgs e)
        {
            Game1.gameSettings.SwitchDebugMode();
            SetMenuEntryText();
        }
    }
}