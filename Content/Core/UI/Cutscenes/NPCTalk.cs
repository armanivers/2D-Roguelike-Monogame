using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Cutscenes
{
    class NPCTalk : CutsceneBasis
    {
        public NPCTalk(int npcTalkId) : base()
        {
            DetermineTextureCutscene(npcTalkId);
            color = Color.White;
            transparency = 0;
            position = new Vector2(0, 0);
        }

        private void DetermineTextureCutscene(int npcTalkId)
        {
            switch(npcTalkId)
            {
                case 0:
                    cutsceneTexture = TextureManager.menu.NPCTalk00;
                    break;
                case 1:
                    cutsceneTexture = TextureManager.menu.NPCTalk01;
                    break;
                case 2:
                    cutsceneTexture = TextureManager.menu.NPCTalk02;
                    break;
                default:
                    cutsceneTexture = TextureManager.menu.NPCTalk00;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(Game1.gameSettings.fullScreen) spriteBatch.Draw(cutsceneTexture, position, null, color * transparency, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
            else spriteBatch.Draw(cutsceneTexture, position, color * transparency);
        }

        public override void Update(GameTime gameTime)
        {

            // determine in which phase the cutscene is at, fadein/show cutscene/fadeout
            if(timer <= fadeInDuration)
            {
                transparency += fadeInSpeed;
            }
            else if(timer >= fadeInDuration && timer <= cutsceneDuration-fadeOutDuration)
            {
                transparency = 1f;
            }
            else if(timer <= cutsceneDuration)
            {
                transparency -= fadeOutSpeed;
            }    

            // if cutscene done, remove it
            if (timer >= cutsceneDuration) cutsceneDone = true;

            timer += 0.01f;
        }

    }
}
