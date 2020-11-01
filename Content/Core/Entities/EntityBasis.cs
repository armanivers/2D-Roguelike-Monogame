using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    abstract class EntityBasis
    {
        protected Texture2D texture;

        public Vector2 Position, Velocity;

        public float Orientation;

        //is the entity dead? then shouldn't be drawn anymore + removed
        public bool IsExpired; 

        //Animation stuff
        protected AnimationManager animationManager;
        //assign an animation to a word -> for example walk => animation.walk
        protected Dictionary<string, Animation> animations;

        public Vector2 Size
        {
            //"collision"
            get
            {
                return texture == null ? Vector2.Zero : new Vector2(60, 70);
            }
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, Orientation, Size / 2f, 1f, SpriteEffects.None, 1);

        }
    }
}
