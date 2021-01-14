using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Cutscenes
{
    class FadeOutCircle : CutsceneBasis
    {
        Vector2 origin;
        private float scalingFactor;
        public FadeOutCircle() : base()
        {
            cutsceneTexture = TextureManager.menu.CircleFade;
            
            color = Color.White;
            transparency = 1;
            
            position = new Vector2(0, 0);
            origin = new Vector2(0, 0);
            
            sceneDuration = 1.5f;

            if (Game1.gameSettings.fullScreen)
            {
                scale = 1.5f;
                scalingFactor = 1.5f;
            }
            else
            {
                scale = 1f;
                scalingFactor = 1f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cutsceneTexture, position, null, color * transparency, 0, origin, scale, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            // zoom in the circle
            scale += 0.1f;
            
            // center the origin of circle image
            origin = new Vector2((cutsceneTexture.Width / 2), (cutsceneTexture.Height / 2));

            // center the image depending on resolution
            position = new Vector2((cutsceneTexture.Width * scalingFactor / 2), (cutsceneTexture.Height * scalingFactor / 2));

            if (scale > scalingFactor * 10) transparency-=0.01f;

            // if cutscene done, remove it
            if (timer >= sceneDuration) cutsceneDone = true;

            timer += 0.01f;
        }

    }
}
