using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class Fog : UIElement
    {
        // x Coordinate from where the fog image shoudl be drawn 
        private int xOffset;
        private float fogTransparency;
        // by how much should the transparency be reduced each update
        private float tSpeed;
        private float maxTransparency;
        private float minTransparency;
        // pixels before the image should start getting 100% invisible
        private int hidingOffset;

        public Fog()
        {
            xOffset = -1400;
            fogTransparency = 0.1f;
            tSpeed = 0.001f;
            maxTransparency = 0.5f;
            minTransparency = 0;
            hidingOffset = 150;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            // FOV + Fog
            if (Game1.gameSettings.fullScreen)
            {
                // FOV Texture = 720p , so for 1080p it needs upscaling of 1.5
                spritebatch.Draw(TextureManager.ui.FOV, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
                spritebatch.Draw(TextureManager.ui.MovingFog, new Vector2(xOffset, -30), null, Color.White*fogTransparency, 0, Vector2.Zero, 1.1f, SpriteEffects.None, 0);
                spritebatch.Draw(TextureManager.ui.Fog, Vector2.Zero, null, Color.White * 0.3f, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
            }
            else
            {
                spritebatch.Draw(TextureManager.ui.FOV, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                spritebatch.Draw(TextureManager.ui.MovingFog, new Vector2(xOffset, -30), null, Color.White * fogTransparency, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
                spritebatch.Draw(TextureManager.ui.Fog, Vector2.Zero, null, Color.White * 0.4f, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
        }

        public override void Update(GameTime gametime)
        {
            xOffset++;
            
            fogTransparency += tSpeed;
            //Debug.Print("off {0}, trans {1}, speed {2}", xOffset, fogTransparency, tSpeed);

            // has the texture reached the end? then set move it back to the futhest left of itself
            if (xOffset >= 0) xOffset = -1400;

            // we reached the end of the texture, means there will be a sudden texutre switch, lower the transparency so its not visible (*-1 becuse the texure coordiante is always negative)
            if (xOffset * -1 < hidingOffset)
            {
                tSpeed = -0.005f;
            }
            else
            {
                // reached max transparency, that means now lower
                if (fogTransparency >= maxTransparency)
                {
                    fogTransparency = maxTransparency;
                    tSpeed = -0.001f;
                }

                // reached min transparency, that means now higher
                if (fogTransparency <= minTransparency)
                {
                    fogTransparency = minTransparency;
                    tSpeed = 0.001f;
                }
            }

        }
    }
}
