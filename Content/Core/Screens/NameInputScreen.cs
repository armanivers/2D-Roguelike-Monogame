#region File Description

//-----------------------------------------------------------------------------
// MessageBoxScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Linq;

#endregion Using Statements

namespace _2DRoguelike.Content.Core.Screens
{
    /// <summary>
    /// A popup message box screen, used to display "are you sure?"
    /// confirmation messages.
    /// </summary>
    internal class NameInputScreen : GameScreen
    {
        #region Fields

        private string message;
        private string name = Game1.gameSettings.playerName;
        private string line1 = "-----------------------";
        private string line2;

        private const int maxCharacters = 15;
        private const string nameOutlineCharacter = "-";

        private Texture2D gradientTexture;
        private Keys[] lastPreesedKeys = new Keys[2];
        #endregion Fields

        #region Events

        public event EventHandler<PlayerIndexEventArgs> Accepted;

        public event EventHandler<PlayerIndexEventArgs> Cancelled;

        #endregion Events

        #region Initialization

        /// <summary>
        /// Constructor lets the caller specify whether to include the standard
        /// "A=ok, B=cancel" usage text prompt.
        /// </summary>
        public NameInputScreen()
        {
            line2 = "Current Name: " + name;
            this.message = "Please input your name:";

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            gradientTexture = TextureManager.menu.Gradient;
        }

        #endregion Initialization

        #region Handle Input

        /// <summary>
        /// Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;

            // player accepted the new name
            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                if (Accepted != null)
                    Accepted(this, new PlayerIndexEventArgs(playerIndex));
                // empty strig isn't allowed as a name
                if(!name.Equals("")) Game1.gameSettings.SetName(name);
                ExitScreen();
            }
            // player cancelled name input window
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                if (Cancelled != null)
                    Cancelled(this, new PlayerIndexEventArgs(playerIndex));
                ExitScreen();
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            CheckKeyboardInput();
        }

        public void CheckKeyboardInput()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (!lastPreesedKeys.Contains(key))
                {
                    PressedKey(key);
                }
            }
            lastPreesedKeys = pressedKeys;
            UpdateNameString();
        }

        public void UpdateNameString()
        {
            // if min/max chars reached, dont update string
            if (name.Length < 0 || name.Length > maxCharacters) return;

            line1 = "";

            int nameOutlineAmount = maxCharacters - name.Length;

            // used to make sure the name is always in the center of the string
            if(name.Length%2 != 0)
            {
                nameOutlineAmount++;
            }

            for (int i = 0; i < nameOutlineAmount / 2; i++)
            {
                line1 += nameOutlineCharacter;
            }
            line1 += name;
            for (int i = 0; i < nameOutlineAmount / 2; i++)
            {
                line1 += nameOutlineCharacter;
            }
        }

        public void PressedKey(Keys key)
        {
            // input characters
            if (name.Length < maxCharacters)
            {
                // check if capslock is activated
                if(!Keyboard.GetState().CapsLock) 
                    name += ConvertPressedKey(key).ToLower();
                else 
                    name += ConvertPressedKey(key);
            }

            // deleting characters
            if (name.Length > 0 && key == Keys.Back)
            {
                name = name.Remove(name.Length - 1);
            }
        }

        private string ConvertPressedKey(Keys k)
        {
            string keyValue = k.ToString();
            
            // check if special character
            if (keyValue.Length > 1)
            {
                string firstChar = keyValue[0].ToString();

                // for example numbers (d0-d9)
                if (firstChar== "D")
                {
                    return "" +k.ToString()[1];
                }
                // ignore everything else
                else
                {
                    return "";
                }
            }
            // if normal character, return it
            else return k.ToString();
        }


        #endregion Handle Input

        #region Draw

        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            //line 1 and 2
            Vector2 line1Size = font.MeasureString(line1);
            Vector2 textPosition1 = (viewportSize - line1Size) / 2;
            textPosition1.Y += line1Size.Y + textSize.Y;

            Vector2 line2Size = font.MeasureString(line2);
            Vector2 textPosition2 = (viewportSize - line2Size) / 2;
            textPosition2.Y += line2Size.Y + line1Size.Y + textSize.Y;

            // The background includes a border somewhat larger than the text itself.
            const int hPad = 32;
            const int vPad = 16;

            // Rectangle of box
            Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y * 4 + vPad * 2);

            // Rectangle of accept or maybe "selected" shadow?
            Rectangle acceptRectangle = new Rectangle((int)textPosition1.X,
                                                         (int)textPosition1.Y,
                                                         (int)line1Size.X,
                                                         (int)line1Size.Y);


            // Rectangle of deny

            // Fade the popup alpha during transitions.
            Color color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            // Draw the background rectangle.
            spriteBatch.Draw(gradientTexture, backgroundRectangle, color);
            //spriteBatch.Draw(gradientTexture, acceptRectangle, Color.Yellow);

            // Draw the message box text.
            spriteBatch.DrawString(font, message, textPosition, color);
            spriteBatch.DrawString(font, line1, textPosition1, color);
            spriteBatch.DrawString(font, line2, textPosition2, color);

            spriteBatch.End();
        }

        #endregion Draw
    }
}