using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    public class AnimationManager
    {
        private Animation animation;
        private float timer;
        private bool running = true;

        public Vector2 Position { get; set; }


        public AnimationManager(Animation animation)
        {
            this.animation = animation;
        }

        public void Play(Animation newAnimation)
        {
            /*
             check whether animation is null OR the animation that should be played is the same as the previous one
             if yes, then no need to set the timer to 0 again!
            */
            if (newAnimation == null || newAnimation == this.animation)
            {
                return;
            }
            this.animation = newAnimation;
            newAnimation.CurrentFrame = 0;
            timer = 0f;
            running=true;
        }

        public void Stop()
        {
            timer = 0f;
            // auf dem letzten Frame stehenbleiben
            animation.CurrentFrame--;
            // animation.CurrentFrame = 0;
        }

        public bool IsRunning()
        {
            return running;
        }

        public bool IsPrioritized() {
            return animation.Prioritized;
        }

        public void Update(GameTime gameTime)
        {
            if (IsRunning())
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > animation.FrameSpeed)
                {
                    timer = 0f;
                    animation.CurrentFrame++;
                    if (animation.CurrentFrame >= animation.FrameCount)
                    // Hier kommt das isLppoing zum Zuge
                    {
                        if (!animation.isLooping)
                        {
                            running = false;
                            Stop();

                        }else
                        animation.CurrentFrame = 0;
                    }
                }

            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(animation.Texture, Position, new Rectangle(animation.CurrentFrame * animation.FrameWidth, animation.yOffest, animation.FrameWidth, animation.FrameHeight), Color.White);
        }
    }
}
