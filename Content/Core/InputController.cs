﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace _2DRoguelike.Content.Core
{
    static class InputController
    {
        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;
        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }

        public static void Update()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public static Vector2 GetDirection()
        {

            Vector2 direction = Vector2.Zero;

            
            KeyboardState kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (kstate.IsKeyDown(Keys.D))
                direction.X += 1;
            if (kstate.IsKeyDown(Keys.W))
                direction.Y -= 1;
            if (kstate.IsKeyDown(Keys.S))
                direction.Y += 1;

            // wurden mehrere Richtungstasten gedrueckt? → Nur ein Drittel der Geschwindigkeit gehen (diagonal langsamer)


            return direction;
        }

        public static Vector2 GetMouseClickPosition()
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                return MousePosition;
            }
            else
            {
                return Vector2.Zero;
            }
        }

        public static Vector2 GetMousePosition()
        {
            return MousePosition;
        }

        public static Keys[] GetPressedKeys() {
            return keyboardState.GetPressedKeys();
        }
    }
}
