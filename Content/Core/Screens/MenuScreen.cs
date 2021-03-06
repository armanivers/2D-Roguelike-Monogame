#region File Description

//-----------------------------------------------------------------------------
// MenuScreen.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata;
using Color = Microsoft.Xna.Framework.Color;

#endregion Using Statements

namespace _2DRoguelike.Content.Core.Screens
{
    /// <summary>
    /// Base class for screens that contain a menu of options. The user can
    /// move up and down to select an entry, or cancel to back out of the screen.
    /// </summary>
    internal abstract class MenuScreen : GameScreen
    {
        #region Fields

        private List<MenuEntry> menuEntries = new List<MenuEntry>();
        private int selectedEntry = 0;
        private string menuTitle;

        #endregion Fields

        #region Properties
        protected bool notEscapable = false;
        protected String dataString;
        protected bool customMenu = false;
        protected int customSelectEntry;

        protected bool customUpdate = false;
        /// <summary>
        /// Gets the list of menu entries, so derived classes can add
        /// or change the menu contents.
        /// </summary>
        protected IList<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }

        #endregion Properties

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public MenuScreen(string menuTitle)
        {
            this.menuTitle = menuTitle;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public MenuScreen(string menuTitle, bool customMenu, int customSelectedEntry)
        {
            this.menuTitle = menuTitle;

            this.customMenu = customMenu;
            this.customSelectEntry = customSelectedEntry;


            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public MenuScreen(string menuTitle, bool customMenu, int customSelectedEntry,bool customUpdate)
        {
            this.menuTitle = menuTitle;

            this.customMenu = customMenu;
            this.customSelectEntry = customSelectedEntry;
            this.customUpdate = false;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion Initialization

        #region Handle Input

        /// <summary>
        /// Responds to user input, changing the selected entry and accepting
        /// or cancelling the menu.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            // Move to the previous menu entry?
            if (!MenuEntries[selectedEntry].Selectable || input.IsMenuUp(ControllingPlayer))
            {
                selectedEntry--;
                SoundManager.MenuItemSelect.Play(Game1.gameSettings.soundeffectsLevel, 0.3f, 0);
                if (selectedEntry < 0 || selectedEntry < customSelectEntry)
                    selectedEntry = menuEntries.Count - 1;
                if (menuEntries[selectedEntry].Selectable)
                {
                    //Debug.Print("Not Selectable");
                }
                else
                {
                    //Debug.Print("selectable");
                }
            }

            // Move to the next menu entry?
            if (!MenuEntries[selectedEntry].Selectable || input.IsMenuDown(ControllingPlayer))
            {
                selectedEntry++;
                SoundManager.MenuItemSelect.Play(Game1.gameSettings.soundeffectsLevel, 0.3f, 0);
                if (selectedEntry >= menuEntries.Count)
                    if (customMenu)
                    {
                        selectedEntry = customSelectEntry;
                    }
                    else
                    {
                        selectedEntry = 0;
                    }
                    

                if (menuEntries[selectedEntry].Selectable)
                {
                    //Debug.Print("Not Selectable");
                }
                else
                {
                    //Debug.Print("selectable");
                }
            }

            // Accept or cancel the menu? We pass in our ControllingPlayer, which may
            // either be null (to accept input from any player) or a specific index.
            // If we pass a null controlling player, the InputState helper returns to
            // us which player actually provided the input. We pass that through to
            // OnSelectEntry and OnCancel, so they can tell which player triggered them.
            PlayerIndex playerIndex;

            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                OnSelectEntry(selectedEntry, playerIndex);
            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex) && !notEscapable)
            {
                if(this is OptionsMenuScreen || this is MainMenuScreen || this is SoundOptionsMenuScreen || this is DebugmodeOptionsMenusScreen)
                {
                    OnCancel(playerIndex);
                }
                else
                {
                    OnCancelPause(playerIndex);
                }
            }
        }

        /// <summary>
        /// Handler for when the user has chosen a menu entry.
        /// </summary>
        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            Game1.gameSettings.SaveSettings();
            menuEntries[entryIndex].OnSelectEntry(playerIndex);
        }

        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }

        protected virtual void OnCancelPause(PlayerIndex playerIndex)
        {
            MediaPlayer.Resume();

            if (this is GamestatsScreen)
            {
                LoadingScreen.LoadCustom(ScreenManager, true, null, new BackgroundScreen(), new MainMenuScreen());
            }
            ExitScreen();
        }

        /// <summary>
        /// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        /// </summary>
        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }

        protected void OnCancelPause(object sender, PlayerIndexEventArgs e)
        {
            OnCancelPause(e.PlayerIndex);
        }

        #endregion Handle Input

        #region Update and Draw

        /// <summary>
        /// Allows the screen the chance to position the menu entries. By default
        /// all menu entries are lined up in a vertical list, centered on the screen.
        /// </summary>
        protected virtual void UpdateMenuEntryLocations()
        {
            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // start at Y = 175; each X value is generated per entry
            Vector2 position = new Vector2(0f, 175f);

            // update each menu entry's location in turn
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];

                // each entry is to be centered horizontally
                position.Y = ScreenManager.GraphicsDevice.Viewport.Height / 3 + menuEntry.GetHeight(this) * (i*2);
                position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - menuEntry.GetWidth(this) / 2;

                if (ScreenState == ScreenState.TransitionOn)
                    position.Y -= transitionOffset * 256;
                else
                    position.Y += transitionOffset * 512;

                // set the entry's position
                menuEntry.Position = position;

                // move down for the next entry the size of this entry
                position.Y += menuEntry.GetHeight(this);
            }

            if(ScreenState == ScreenState.TransitionOn)
            {
                customUpdate = true;
            }
        }

        /// <summary>
        /// Updates the menu.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Update each nested MenuEntry object.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                menuEntries[i].Update(this, isSelected, gameTime);
            }

            if(customUpdate)
            {
                CustomUpdate();
            }
        }

        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // make sure our entries are in the right place before we draw them
            UpdateMenuEntryLocations();


            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            spriteBatch.Begin();

            // Draw each menu entry in turn.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];

                bool isSelected = IsActive && (i == selectedEntry);

                menuEntry.Draw(this, isSelected, gameTime);
            }

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;

            Color titleColor = new Color(255, 64, 25) * TransitionAlpha;
            Color dataColor = new Color(255, 191, 128) * TransitionAlpha;

            float titleScale = 1.25f;
            float dataScale = Game1.gameSettings.GetTextScale();

            titlePosition.Y -= transitionOffset * 100;

            if (dataString != null)
            {
                Vector2 dataPosition = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height/2);
                Vector2 dataOrigin = font.MeasureString(dataString) / 2;
                dataPosition.Y -= transitionOffset * 100;
                spriteBatch.DrawString(font, dataString, dataPosition, dataColor, 0,
                                   dataOrigin, dataScale, SpriteEffects.None, 0);
            }

            spriteBatch.DrawString(font, menuTitle, titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }

        #endregion Update and Draw
    }
}