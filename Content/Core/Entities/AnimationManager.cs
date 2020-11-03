using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    class AnimationManager
    {
        private Animation animation;
        private float timer;
        public Vector2 Position { get; set; }
        public AnimationManager(Animation animation)
        {
            this.animation = animation;
        }

        public void Play(Animation animation)
        {
            /*
             check whether animation is null OR the animation that should be played is the same as the previous one
             if yes, then no need to set the timer to 0 again!
            */
            if (animation == null ||  animation == this.animation)
            {
                return;
            }
            this.animation = animation;
            animation.CurrentFrame = 0;
            timer = 0f;
        }

        public void Stop()
        {
            timer = 0f;
            animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > animation.FrameSpeed)
            {
                timer = 0f;
                animation.CurrentFrame++;
                if(animation.CurrentFrame >= animation.FrameCount)
                {
                    animation.CurrentFrame = 0;
                }

            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(animation.Texture, Position, new Rectangle(animation.CurrentFrame * animation.FrameWidth, 0, animation.FrameWidth, animation.FrameHeight), Color.White);
        }
    }
}
