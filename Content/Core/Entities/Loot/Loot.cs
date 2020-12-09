using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot
{
    public abstract class Loot: EntityBasis
    {
        // how much should the loot object float up/down compared to its original spawn position
        private float floatOffset;
        // original spawn position of the object
        private Vector2 basePosition;
        // controls whether it float up or down
        private bool floatUp;
        // controls floating speed
        private float floatingSpeed;

        public Loot(Vector2 pos):base(pos) {
            basePosition = pos;
            floatOffset = 3f;
            floatingSpeed = 0.1f;
            floatUp = true;
 
        }

        public abstract void OnContact();

        public override void Update(GameTime gameTime)
        {
            if(floatUp)
            {
                position.Y += floatingSpeed;
            }
            else
            {
                position.Y -= floatingSpeed;
            }

            if(position.Y > basePosition.Y+floatOffset)
            {
                floatUp = false;

            } 
            if(position.Y < basePosition.Y - floatOffset)
            {
                floatUp = true;
            }
        }
    }
}
