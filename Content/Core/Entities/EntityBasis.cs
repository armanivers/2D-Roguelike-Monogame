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
        private float scaleFactor = 1f;
        public float ScaleFactor { get => scaleFactor; set => scaleFactor = value; } // Skalierung der Textur (muss für Hitboxen mitberücksichtigt werden!). 1f = Default für alle, in Unterklassen änderbar
        private Vector2 drawOrigin = Vector2.Zero; 
        public Vector2 DrawOrigin { get => drawOrigin; set => drawOrigin = value; } // Zentraler Punkt der Textur (Für Rotationen (→Hitbox) hilfreich). Vector2.Zero = Default für alle, in Unterklassen änderbar

        public bool shadow;
        public Vector2 shadowPosition;
        public Vector2 shadowOffset;

        protected Texture2D texture;

        private bool lockedAnimation = false;
        protected AnimationManager animationManager;
        protected Dictionary<string, Animation> animations;


        protected Rectangle hitbox;
        public Rectangle Hitbox { get => hitbox; set => hitbox = value; }

        public Vector2 HitboxCenter { get { return new Vector2(Hitbox.X + Hitbox.Width / 2, Hitbox.Y + Hitbox.Height / 2); } }

        private Vector2 position;

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
        public Vector2 TextureSize
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
                spriteBatch.Draw(TextureManager.Shadow, shadowPosition, null, Color.White * 0.45f * transparency, 0,
                    DrawOrigin + new Vector2(0, -35),
                    ScaleFactor, SpriteEffects.None, 0);

            if (animationManager != null)
            {
                animationManager.Draw(spriteBatch);
            }
            else if (texture != null)
            {
                if (transparency > 0)
                    // Size / 2: Origin in der Mitte muss geeinigt werden wann!!!
                    spriteBatch.Draw(texture, Position, null, colour * transparency, rotation, DrawOrigin, ScaleFactor, SpriteEffects.None, 0);
            }
            else { throw new Exception("Draw failed, there's a problem with the texture/animationManager!"); };
        }

        public bool AnimationExists(String animationIdent)
        {
            return animations.ContainsKey(animationIdent);
        }

        public int AnimationDuration(String animation)
        {
            Debug.WriteLine(animations[animation].FrameCount * (int)animations[animation].FrameSpeed * 100);
            return animations[animation].FrameCount * (int)animations[animation].FrameSpeed * 100;
        }
        public void SetAnimation(String animationIdentifier, bool revert = false)
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
                animationManager.Play(animations[animationIdentifier], revert ? !animations[animationIdentifier].Reverse : animations[animationIdentifier].Reverse);
                if (animationManager.IsPrioritized())
                    lockedAnimation = true;
            }

        }
    }
}
