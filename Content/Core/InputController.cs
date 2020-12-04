using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public static KeyboardState keyboardState, previousKeyboardState;
        public static MouseState mouseState, previousMouseState;
        public static Vector2 MousePosition { 
            get {
                return Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), Matrix.Invert(Camera.transform));
            } 
        }

        public static void Update()
        {
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            previousMouseState = mouseState;
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

            return direction;
        }

        public static bool IsMouseScrolled() {
            return mouseState.ScrollWheelValue != previousMouseState.ScrollWheelValue;
        }

        public static bool IsMouseScrolledDown()
        {
            return mouseState.ScrollWheelValue < previousMouseState.ScrollWheelValue;
        }
        public static bool IsMouseScrolledUp()
        {
            return mouseState.ScrollWheelValue > previousMouseState.ScrollWheelValue;
        }


        public static bool IsMouseButtonPressed()
        {
            return IsLeftMouseButtonPressed() || IsRightMouseButtonPressed();
        }
        public static bool IsMouseButtonHeld() {
            return IsLeftMouseButtonHeld() || IsRightMouseButtonHeld();
        }

        public static bool IsLeftMouseButtonPressed() {
            return IsLeftMouseButtonHeld() && previousMouseState.LeftButton != ButtonState.Pressed;
        }

        public static bool IsLeftMouseButtonHeld()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }      
        public static bool IsRightMouseButtonPressed() {
            return IsRightMouseButtonHeld() &&  previousMouseState.RightButton != ButtonState.Pressed;
        }

        public static bool IsRightMouseButtonHeld()
        {
            return mouseState.RightButton == ButtonState.Pressed;
        }

        public static bool IsKeyDown(Keys key) {
            return keyboardState.IsKeyDown(key);
        }

        public static bool IsKeyPressed(Keys key) {
            return IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }



        public static Vector2 GetMouseClickPosition()
        {
            if (IsMouseButtonHeld())
            {
                return MousePosition;
            }
            else
            {
                return Vector2.Zero;
            }

            /* if mouse isn't clicked, return a zero vector */
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
