using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core
{
    class GameSettings
    {
        public static int screenWidth;
        public static int screenHeight;
        public static int backgroundMusicLevel;
        public static int soundeffectsLevel;

        public static bool fullScreen;

        public static void SetFullscreen()
        {
            fullScreen = true;
            Game1._graphics.PreferredBackBufferWidth = 1920;
            Game1._graphics.PreferredBackBufferHeight = 1080;
            screenWidth = 1920;
            screenHeight = 1080;
            Game1._graphics.ToggleFullScreen();
            //Game1._graphics.ApplyChanges();
        }

        public static void SetWindowedMode()
        {
            fullScreen = false;
            Game1._graphics.PreferredBackBufferWidth = 1280;
            Game1._graphics.PreferredBackBufferHeight = 720;
            screenWidth = 1280;
            screenHeight = 720;
            Game1._graphics.ToggleFullScreen();
            //Game1._graphics.ApplyChanges();
        }

        public static float GetTextScale()
        {
            if (fullScreen)
            {
                return 0.9f;
            }
            else
            {
                return 0.6f;
            }
        }

        public static void SaveSettings()
        {
            // TODO Save all settings to a config file
        }

        public static void LoadSettings()
        {
            // TODO Load all settings from a config file
        }
    }
}
