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
        Vector2 origin;
        Vector2 previousSize;
        Vector2 newSize;
        public FadeInCircle() : base()
        {
            
            cutsceneTexture = TextureManager.menu.CircleFade;
            color = Color.White;
            transparency = 1;
            position = new Vector2(0, 0);
            origin = new Vector2(0, 0);
            if (Game1.gameSettings.fullScreen)
            {
                scale = 1.5f;
            }
            else
            {
                scale = 1.0f;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cutsceneTexture, position, null, color * transparency, 0, origin, scale, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {

            previousSize = new Vector2(cutsceneTexture.Width * scale, cutsceneTexture.Height * scale);
            newSize = new Vector2(cutsceneTexture.Width * (scale + .01f), cutsceneTexture.Height * (scale + .01f));
            scale += 0.01f;
            origin.Y += (Math.Abs(previousSize.Y - newSize.Y) / 2)*scale;
            origin.X += (Math.Abs(previousSize.X - newSize.X) / 2)*scale;
            //position = new Vector2(cutsceneTexture.Width/2*scale,cutsceneTexture.Height/2* scale);


            // if cutscene done, remove it
            if (timer >= cutsceneDuration) cutsceneDone = true;

            timer += 0.01f;
        }

    }
}
