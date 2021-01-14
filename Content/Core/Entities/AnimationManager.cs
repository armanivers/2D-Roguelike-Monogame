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
        private EntityBasis entity;
        private Animation animation;
        private float timer;
        private bool running = true;
        private bool reverse;
        private bool prioritized;
        public bool Reverse { get => reverse; set => reverse = value; }
        public bool Prioritized { get => prioritized; set => prioritized = value; }

        public Vector2 Position { get; set; }

        public AnimationManager(EntityBasis entity, Animation animation)
        {
            this.entity = entity;
            this.animation = animation;
            this.Position = entity.Position;
            Reverse = animation.Reverse;
            Prioritized = animation.Prioritized;
        }

        public void Play(Animation newAnimation, bool reverse)
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
            this.Reverse = reverse;
            this.Prioritized = animation.Prioritized;
            newAnimation.CurrentFrame = !this.Reverse ? 0 : animation.FrameCount - 1 ;
            timer = 0f;
            running=true;
        }

        public void Stop()
        {
            timer = 0f;
            // auf dem letzten Frame stehenbleiben
            animation.CurrentFrame+= !this.Reverse ? -1 : 1;
            
        }

        public bool IsRunning()
        {
            return running;
        }

        public bool IsPrioritized() {
            return Prioritized;
        }

        public void Update(GameTime gameTime)
        {
            if (IsRunning())
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > animation.FrameSpeed)
                {
                    timer = 0f;
                    animation.CurrentFrame+= !this.Reverse ? 1 : -1;


                    { if(!this.Reverse ? animation.CurrentFrame>=animation.FrameCount: animation.CurrentFrame < 0)
                        {
                         // Hier kommt das isLppoing zum Zuge
                        {
                            if (!animation.isLooping)
                            {
                                running = false;
                                Stop();

                            }
                            else
                                animation.CurrentFrame = !this.Reverse ? 0 : animation.FrameCount - 1;
                            }
                        }   
                    }
                   
                }

            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (entity.transparency > 0)
                //spritebatch.Draw(animation.Texture, Position, new Rectangle(animation.CurrentFrame * animation.FrameWidth, animation.yOffest, animation.FrameWidth, animation.FrameHeight), entity.colour*entity.transparency);
            
            spritebatch.Draw(animation.Texture,
                Position,
                new Rectangle(animation.CurrentFrame * animation.FrameWidth, animation.yOffest, animation.FrameWidth, animation.FrameHeight), 
                entity.currentColor * entity.transparency,
                0f,
                entity.DrawOrigin,
                entity.ScaleFactor,
                SpriteEffects.None, 0
                );
            
            
            //spriteBatch.Draw(texture, Position, null, colour * transparency, rotation, Size / 2, scaleFactor, SpriteEffects.None, 0);
        }
    }
}
