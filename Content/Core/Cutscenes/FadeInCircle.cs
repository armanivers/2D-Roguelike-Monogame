using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Cutscenes
{
    class FadeInCircle : CutsceneBasis
    {
        private float fadingSpeed;
        private int phaseCounter;

        public FadeInCircle()
        {
            cutsceneTexture = TextureManager.menu.NPCTalk00;
            cutsceneDuration = 120;
            color = Color.White;
            transparency = 0;
            position = new Vector2(0, 0);
            phaseCounter = 0;
            fadingSpeed = 1 / (cutsceneDuration * 100);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(Game1.gameSettings.fullScreen) spriteBatch.Draw(cutsceneTexture, position, null, color * transparency, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
            else spriteBatch.Draw(cutsceneTexture, position, color * transparency);
        }

        // kann schoener gemacht werden, mit phasen "fadingIn","display","fadingout"
        public override void Update(GameTime gameTime)
        {
            if(transparency <= 1 && phaseCounter == 0)
            {
                transparency += 0.01f;
            }
            else if(transparency >= 1 && phaseCounter == 1 && timer < cutsceneDuration)
            {
                timer++;
            }
            else if (transparency >= 0 && phaseCounter == 2)
            {
                transparency -= 0.05f;
            }
            else
            {
                phaseCounter++;
            }

            if(phaseCounter >= 3)
            {
                cutsceneDone = true;
            }

            
        }

    }
}
