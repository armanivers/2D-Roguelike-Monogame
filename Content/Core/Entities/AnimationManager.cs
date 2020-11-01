using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
            if (animation == null)
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
                animation.CurrentFrame = 0;

            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(animation.Texture, Position, new Rectangle(animation.CurrentFrame * animation.FrameWidth, animation.FrameHeight, animation.FrameWidth, animation.FrameHeight), Color.White);
        }
    }
}
