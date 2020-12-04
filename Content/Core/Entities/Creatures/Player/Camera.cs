using Microsoft.Xna.Framework;
using System;

namespace _2DRoguelike.Content.Core.Entities.ControllingPlayer
{
    static class Camera
    {
        // camera view attributes
        public static Matrix transform;
        //public static Viewport view = Game1.Viewport;
        public static float zoom;

        // camera shake attributes
        public static bool screenShake;
        public static int shakeStartAngle;
        public static float shakeRadius;
        public static Random rand = new Random();

        public static void Update(Player player) 
        {
            initZoom();

            Vector2 shakeOffset = CalculateShake();
            var shakeOffsetMultiplier = Matrix.CreateTranslation(shakeOffset.X, shakeOffset.Y, 0);

            const int idleSheetFrameCount_Width = 6;
            const int idleSheetFrameCount_Height = 4;

            var target = Matrix.CreateTranslation(new Vector3(-player.Position.X - (player.Size.X/ idleSheetFrameCount_Width / 2), -player.Position.Y - (player.Size.Y/ idleSheetFrameCount_Height / 2), 0));
            
            var zoomFactor = Matrix.CreateScale(zoom);

            // instead of gameSettings the actual viewport should be used, but its buggy??
            var translationMatrix = Matrix.CreateTranslation(new Vector3(GameSettings.screenWidth * 0.5f, GameSettings.screenHeight* 0.5f, 0));

            transform = target *  zoomFactor *  translationMatrix * shakeOffsetMultiplier;
        } 

        // evtl. paramter fur raid + angle angeben lassen
        public static void ShakeScreen()
        {
            screenShake = true;
            shakeRadius = 15;
            shakeStartAngle = 15;
        }

        public static Vector2 CalculateShake()
        {
            var shakeOffset = new Vector2(0, 0);
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
            return shakeOffset;
        }

        public static void Unload()
        {
            screenShake = false;
            shakeRadius = 0;
            shakeStartAngle = 0;
        }
        
        public static void initZoom()
        {
            if (GameSettings.fullScreen)
            {
                zoom = 2.0f;
            }
            else
            {
                zoom = 1.5f;
            }
        }

    }
}
