using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Projectiles
{
    abstract class Projectile : EntityBasis
    {
        public readonly float flyingSpeed;
        public float SpeedModifier { get; set; }

        protected int xHitboxOffset;
        protected int yHitboxOffset;
        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                hitbox.X = (int)(value.X + xHitboxOffset*scaleFactor);
                hitbox.Y = (int)(value.Y + xHitboxOffset*scaleFactor);

                if (animationManager != null)
                {
                    animationManager.Position = base.Position;
                }
            }
        }

        public Projectile(Vector2 pos,int xHitboxOffset, int yHitboxOffset, float speed) : base(pos)
        {
            EntityManager.AddProjectileEntity(this);
            this.xHitboxOffset = xHitboxOffset;
            this.yHitboxOffset = yHitboxOffset;
            flyingSpeed = speed;
            SpeedModifier = 1;
        }
    }
}
