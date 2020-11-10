using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Player
{
    static class Camera
    {
        // camera view attributes
        public static Matrix transform;
        public static Viewport view = Game1.Viewport;
        public static Vector2 target;

        // camera shake attributes
        public static bool screenShake;
        public static int shakeStartAngle;
        public static float shakeRadius;
        public static Random rand = new Random();

        public static void Update(Player player) 
        {
            target = new Vector2(player.Position.X + (player.Size.X / 2) - 400, player.Position.Y + (player.Size.Y/2) - 200);

            var offset = Matrix.CreateTranslation(new Vector3(-target.X, -target.Y, 0));

            Vector2 shakeOffset = new Vector2(0, 0);

            if (screenShake)
            {
                shakeOffset = new Vector2((float)(Math.Sin(shakeStartAngle) * shakeRadius), (float)(Math.Cos(shakeStartAngle) * shakeRadius));
                shakeRadius -= 0.25f;
                shakeStartAngle += (150 + rand.Next(60));

                if (shakeRadius <= 0)
                {
                    screenShake = false;
                }
            }

            var shakeOffsetMultiplier = Matrix.CreateTranslation(shakeOffset.X, shakeOffset.Y, 0);

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * offset * shakeOffsetMultiplier;
        } 

        // evtl. paramter fur raid + angle angeben lassen
        public static void ShakeScreen()
        {
            screenShake = true;
            shakeRadius = 15;
            shakeStartAngle = 15;
        }

    }
}
