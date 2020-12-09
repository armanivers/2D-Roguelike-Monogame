using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    public abstract class EntityBasis
    {
        public Color colour = Color.White;
        public float transparency = 1f;

        protected Texture2D texture;

        protected AnimationManager animationManager;
        protected Dictionary<string, Animation> animations;

        public Rectangle hitbox; // Problem: bei protected: erlaubt nicht Änderung von Hitbox-Koordinaten
        public Rectangle Hitbox { get { return hitbox; } set { hitbox = value; } }

        public Vector2 HitboxCenter { get { return new Vector2(Hitbox.X + Hitbox.Width / 2, Hitbox.Y + Hitbox.Height / 2); } }

        protected Vector2 position;
        public virtual Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;

                if (animationManager != null)
                {
                    animationManager.Position = position;
                }
            }
        }
        public Vector2 Size
        {
            get
            {
                return texture == null ? Vector2.Zero : new Vector2(texture.Width, texture.Height);
            }
        }

        public Vector2 Acceleration;
        public float rotation;
        public bool isExpired;

        public EntityBasis(Vector2 pos)
        {
            Position = pos;
            isExpired = false;
            rotation = 0;
        }
        // TODO: Setter fuer die Hitbox fixen (fuer untere Klassen), Bsp Klasse Creature


        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {


            if (animationManager != null)
            {
                animationManager.Draw(spriteBatch);
            }
            else if (texture != null)
            {
                if (transparency > 0)
                    spriteBatch.Draw(texture, Position, null, colour * transparency, rotation, Size / 2, 1f, SpriteEffects.None, 0);
            }
            else { throw new Exception("Draw failed, there's a problem with the texture/animationManager!"); };
        }

        public bool AnimationExists(String animationIdent)
        {
            return animations.ContainsKey(animationIdent);
        }

    }
}
