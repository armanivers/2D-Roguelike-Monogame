using _2DRoguelike.Content.Core.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace _2DRoguelike.Content.Core
{
    public class GameSettings
    {
        public static string playerName;

        public static bool fullScreen;
        public static int screenWidth;
        public static int screenHeight;

        public static float backgroundMusicLevel;
        public static float prevBackgroundMusicLevel;
        public static float soundeffectsLevel;
        public static float prevSoundeffectsLevel;

        public static bool DEBUG;

        // debug cheats

        public static bool godMode;
        public static bool showHitbox;
        public static bool showMouse;
        public static bool playerDebug;
        public static bool attackHitbox;
        
        public GameSettings()
        {
            playerName = "User01";

            backgroundMusicLevel = 0.2f;
            soundeffectsLevel = 0.2f;

            DEBUG = true;

            godMode = true;
            showHitbox = true;
            showMouse = true;
            playerDebug = true;
            attackHitbox = true;

            SetWindowedMode();
    }

        public static void SwitchGodMode()
        {
            godMode = !godMode;
        }
        public static void SwitchShowHitbox()
        {
            showHitbox = !showHitbox;
        }

        public static void SwitchShowMouse()
        {
            showMouse = !showMouse;
        }

        public static void SwitchPlayerDebug()
        {
            playerDebug = !playerDebug;
        }

        public static void SwitchAttackHitox()
        {
            attackHitbox = !attackHitbox;
        }

        public static void SetFullscreen()
        {
            fullScreen = true;
            Game1._graphics.PreferredBackBufferWidth = 1920;
            Game1._graphics.PreferredBackBufferHeight = 1080;
            screenWidth = 1920;
            screenHeight = 1080;
            Game1._graphics.ToggleFullScreen();
            UIManager.ForceResolutionUpdate();
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
            UIManager.ForceResolutionUpdate();
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

        public static void DecreaseBackgroundMusic()
        {
            backgroundMusicLevel -= 0.1f;
            if (backgroundMusicLevel <= 0)
            {
                backgroundMusicLevel = 0;
            }
        }

        public static void IncreaseBackgroundMusic()
        {
            backgroundMusicLevel += 0.1f;
            if (backgroundMusicLevel >= 1)
            {
                backgroundMusicLevel = 1;
            }
        }

        public static void DecreaseSoundEffectLevel()
        {
            soundeffectsLevel -= 0.1f;
            if(soundeffectsLevel < 0)
            {
                soundeffectsLevel = 0;
            }
        }

        public static bool SoundEffectsEnabled()
        {
            if(soundeffectsLevel > 0)
            {
                return true;
            }
            return false;
        }

        public static void IncreaseSoundEffectLevel()
        {
            soundeffectsLevel += 0.1f;
            if (soundeffectsLevel > 1)
            {
                soundeffectsLevel = 1;
            }
        }

        public static bool BackgroundMusicEnabled()
        {
            if (backgroundMusicLevel > 0)
            {
                return true;
            }
            return false;
        }

        public static void MuteUnmuteSoundEffects()
        {
            if (SoundEffectsEnabled())
            {
                prevSoundeffectsLevel = soundeffectsLevel;
                soundeffectsLevel = 0;
            }
            else
            {
                soundeffectsLevel = prevSoundeffectsLevel; 
            }
        }

        public static void MuteUnmuteBackgroundMusic()
        {
            if (BackgroundMusicEnabled())
            {
                prevBackgroundMusicLevel = backgroundMusicLevel;
                backgroundMusicLevel = 0;
            }
            else
            {
                backgroundMusicLevel = prevBackgroundMusicLevel;
            }
        }

        public static void SwitchDebugMode()
        {
            DEBUG = !DEBUG;
        }

        public static void SaveSettings()
        {
            // TODO Save all settings to a config file

        }

        public static void LoadSettings(GameSettings settings)
        {
            // TODO Load all settings from a config file

        }

        public static bool settingsFileExists()
        {
            //return File.Exists();
            return true;
        }


    }
}
