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
        public Color colour;
        public float transparency;
        public float scaleFactor = 1f; // Default für alle, nicht hier ändern

        public bool shadow;
        public Vector2 shadowPosition;
        public Vector2 shadowOffset;

        protected Texture2D texture;

        protected AnimationManager animationManager;
        protected Dictionary<string, Animation> animations;

        public Rectangle hitbox; // Problem: bei protected: erlaubt nicht Änderung von Hitbox-Koordinaten
        public Rectangle Hitbox { get { return hitbox; } set { hitbox = value; } }
        private bool lockedAnimation = false;
        public Vector2 HitboxCenter { get { return new Vector2(Hitbox.X + Hitbox.Width / 2, Hitbox.Y + Hitbox.Height / 2); } }

        private Vector2 position;

        public virtual Vector2 Position {
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
            colour = Color.White;
            transparency = 1f;
            shadow = false;
        }
        // TODO: Setter fuer die Hitbox fixen (fuer untere Klassen), Bsp Klasse Creature


        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            if (shadow)
                // Size / 2: Was, wenn die Textur ein sheet ist (breite größer, als das gezeichnete???)
                spriteBatch.Draw(TextureManager.Shadow, shadowPosition, null, Color.White*0.7f*transparency, 0, Size / 2 - new Vector2(0, 35), 1f, SpriteEffects.None, 0);

            if (animationManager != null)
            {
                animationManager.Draw(spriteBatch);
            }
            else if (texture != null)
            {
                if (transparency > 0)
                    // Size / 2: Origin in der Mitte muss geeinigt werden wann!!!
                    spriteBatch.Draw(texture, Position, null, colour * transparency, rotation, Size / 2, scaleFactor, SpriteEffects.None, 0);
            }
            else { throw new Exception("Draw failed, there's a problem with the texture/animationManager!"); };
        }

        public bool AnimationExists(String animationIdent)
        {
            return animations.ContainsKey(animationIdent);
        }
        public void SetAnimation(String animationIdentifier)
        {
            if (lockedAnimation)
            {
                if (!animationManager.IsRunning())
                    lockedAnimation = false;
                else return;
            }

            // Debug.WriteLine(animationIdentifier);

            if (animationManager != null)
            {
                animationManager.Play(animations[animationIdentifier]);
                if (animationManager.IsPrioritized())
                    lockedAnimation = true;
            }

        }
    }
}
