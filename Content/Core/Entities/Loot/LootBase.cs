using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot
{
    public abstract class LootBase: EntityBasis
    {
        // how much should the loot object float up/down compared to its original spawn position
        protected float floatOffset;
        // original spawn position of the object
        protected Vector2 basePosition;
        // controls whether it float up or down
        protected bool floatUp;
        // controls floating speed
        protected float floatingSpeed;
        
        // should this loot object float or not
        protected bool floatable;

        public LootBase(Vector2 pos):base(pos) {
            EntityManager.AddLootEntity(this);
            // we are using 32x32px loot items drop texturs 
            Hitbox = new Rectangle((int)Position.X - 16, (int)Position.Y - 16, 32, 32);
            basePosition = pos;
            floatOffset = 3f;
            floatingSpeed = 0.1f;
            floatUp = true;
            floatable = true;
 
        }

        public abstract void OnContact();

        public override void Update(GameTime gameTime)
        {
            if(floatable)
            {
                Float();
            }
        }

        public void Float()
        {
            if (floatUp)
            {
                position.Y += floatingSpeed;
            }
            else
            {
                position.Y -= floatingSpeed;
            }

            if (position.Y > basePosition.Y + floatOffset)
            {
                floatUp = false;

            }
            if (position.Y < basePosition.Y - floatOffset)
            {
                floatUp = true;
            }
        }
    }
}
