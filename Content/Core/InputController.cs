using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core
{
    static class InputController
    {
        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;
        private static GamePadState gamepadState, lastGamepadState;

        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }

        public static void Update()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            gamepadState = GamePad.GetState(PlayerIndex.One);

        }

        public static Vector2 GetDirection()
        {

            Vector2 direction = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                direction.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                direction.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                direction.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                direction.Y += 1;

            return direction;
        }
    }
}
