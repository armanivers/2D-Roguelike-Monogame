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
        private string skipMessage = "Press T to skip";
        private Vector2 messagePosition;
        public NPCTalk(int npcTalkId, bool interactable = false) : base()
        {
            this.interactable = interactable;
            DetermineTextureCutscene(npcTalkId);
            color = Color.White;
            transparency = 0;
            position = new Vector2(0, 0);
            messagePosition = new Vector2(Game1.gameSettings.screenWidth/2-TextureManager.FontArial.MeasureString(skipMessage).X/2,100);
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
                case 3:
                    cutsceneTexture = TextureManager.menu.NPCTalk03;
                    break;
                default:
                    cutsceneTexture = TextureManager.menu.NPCTalk00;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.gameSettings.fullScreen)
            {
                spriteBatch.Draw(cutsceneTexture, position, null, color * transparency, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
            }
            else 
            {
                spriteBatch.Draw(cutsceneTexture, position, color * transparency);
            }

            if (paused) spriteBatch.DrawString(TextureManager.FontArial, skipMessage, messagePosition, Color.Red);

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
                if(interactable && !buttonPressed) paused = true;
                if (interactable) UpdateInteractable();
                transparency = 1f;
            }
            else if(timer <= cutsceneDuration)
            {
                transparency -= fadeOutSpeed;
            }

            // if cutscene done, remove it
            if (timer >= cutsceneDuration) cutsceneDone = true;

            if(!paused) timer += 0.01f;
        }

        public void UpdateInteractable()
        {
            // either the auto skip timer has reached max or user pressed the skip button
            if ((interactableTimer > interactbleAutoSkipDuration) || interactable && InputController.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.T))
            {
                paused = false;
                buttonPressed = true;
                timer = cutsceneDuration - fadeOutDuration;
            }

            // otherwise increment autoskippable timer
            interactableTimer += 0.1f;

        }

    }
}
