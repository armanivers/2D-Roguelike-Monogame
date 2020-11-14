using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Projectiles
{
    abstract class Projectile : EntityBasis
    {
        protected float speed;
        protected int xHitboxOffset;
        protected int yHitboxOffset;
        public override Vector2 Position
        {
            get { return base.position; }
            set
            {
                base.position = value;
                hitbox.X = (int)value.X + xHitboxOffset;
                hitbox.Y = (int)value.Y + xHitboxOffset;

                if (animationManager != null)
                {
                    animationManager.Position = base.position;
                }
            }
        }

        public Projectile(Vector2 pos,int xHitboxOffset, int yHitboxOffset) : base(pos)
        {
            this.xHitboxOffset = xHitboxOffset;
            this.yHitboxOffset = yHitboxOffset;
        }
    }
}
