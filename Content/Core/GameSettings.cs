﻿using _2DRoguelike.Content.Core.UI;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace _2DRoguelike.Content.Core
{
    public class GameSettings
    {
        private string fileName = "settings.xml";

        public string playerName;

        public bool fullScreen;
        public int screenWidth;
        public int screenHeight;

        public float backgroundMusicLevel;
        public float prevBackgroundMusicLevel;
        public float soundeffectsLevel;
        public float prevSoundeffectsLevel;

        public bool DEBUG;

        // debug cheats

        public bool godMode;
        public bool showHitbox;
        public bool showMouse;
        public bool playerDebug;
        public bool attackHitbox;
        public bool noclip;
       
        // default settings 
        public GameSettings()
        {
            // Defaults
            playerName = GenerateRandomPUID();

            backgroundMusicLevel = 0.2f;
            soundeffectsLevel = 0.2f;

            DEBUG = false;

            godMode = false;
            showHitbox = false;
            showMouse = false;
            playerDebug = false;
            attackHitbox = false;
            noclip = false;

            SetWindowedMode();
         }

        public string GenerateRandomPUID()
        {
            string playerID = "User";
            for(int i = 0; i < 4; i++)
            {
                var randomNumber = Game1.rand.Next(0,10);
                playerID += randomNumber.ToString();
            }
            return playerID;
        }

        public void SwitchGodMode()
        {
            godMode = !godMode;
        }
        public void SwitchShowHitbox()
        {
            showHitbox = !showHitbox;
        }

        public void SwitchShowMouse()
        {
            showMouse = !showMouse;
        }

        public void SwitchPlayerDebug()
        {
            playerDebug = !playerDebug;
        }

        public void SwitchAttackHitox()
        {
            attackHitbox = !attackHitbox;
        }

        public void SetFullscreen()
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

        public void SetWindowedMode()
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

        public float GetTextScale()
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

        public void DecreaseBackgroundMusic()
        {
            backgroundMusicLevel -= 0.1f;
            if (backgroundMusicLevel <= 0)
            {
                backgroundMusicLevel = 0;
            }
            MediaPlayer.Volume = backgroundMusicLevel;
        }

        public void IncreaseBackgroundMusic()
        {
            backgroundMusicLevel += 0.1f;
            if (backgroundMusicLevel >= 1)
            {
                backgroundMusicLevel = 1;
            }
            MediaPlayer.Volume = backgroundMusicLevel;
        }

        public void DecreaseSoundEffectLevel()
        {
            soundeffectsLevel -= 0.1f;
            if(soundeffectsLevel < 0)
            {
                soundeffectsLevel = 0;
            }
        }

        public bool SoundEffectsEnabled()
        {
            if(soundeffectsLevel > 0)
            {
                return true;
            }
            return false;
        }

        public void IncreaseSoundEffectLevel()
        {
            soundeffectsLevel += 0.1f;
            if (soundeffectsLevel > 1)
            {
                soundeffectsLevel = 1;
            }
        }

        public bool BackgroundMusicEnabled()
        {
            if (backgroundMusicLevel > 0)
            {
                return true;
            }
            return false;
        }

        public void MuteUnmuteSoundEffects()
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

        public void MuteUnmuteBackgroundMusic()
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
            MediaPlayer.Volume = backgroundMusicLevel;
        }

        public void SwitchDebugMode()
        {
            DEBUG = !DEBUG;
        }

        public void SwitchNoclip()
        {
            noclip = !noclip;
        }

        public void SetName(String name)
        {
            playerName = name;
        }
        public void ApplySettings(GameSettings settings)
        {
            if(settings == null)
            {
                return;
            }

            playerName = settings.playerName;
            
            backgroundMusicLevel = settings.backgroundMusicLevel;
            prevBackgroundMusicLevel = settings.prevBackgroundMusicLevel;

            soundeffectsLevel = settings.soundeffectsLevel;
            prevSoundeffectsLevel = settings.prevSoundeffectsLevel;

            DEBUG = settings.DEBUG;
            godMode = settings.godMode;
            showHitbox = settings.showHitbox;
            showMouse = settings.showMouse;
            playerDebug = settings.playerDebug;
            attackHitbox = settings.attackHitbox;
            noclip = settings.noclip;

            if(settings.fullScreen)
            {
                SetFullscreen();
            }
            else
            {
                SetWindowedMode();
            }

        }

        public void SaveSettings()
        {
            using(TextWriter writer = new StreamWriter(Game1.projectPath+fileName))
            {
                XmlSerializer xmlser = new XmlSerializer(this.GetType());
                xmlser.Serialize(writer,this);
            }
        }

        public void LoadSettings()
        {
            if(!SettingsFileExists())
            {
                SaveSettings();
                return;
            }
            GameSettings instance;

            using(TextReader reader = new StreamReader(Game1.projectPath+fileName))
            {
                XmlSerializer xmlser = new XmlSerializer(this.GetType());
                instance = (GameSettings)xmlser.Deserialize(reader);
            }
            ApplySettings(instance);

        }

        public bool SettingsFileExists()
        {
            return File.Exists(Game1.projectPath + fileName);
        }


    }
}
