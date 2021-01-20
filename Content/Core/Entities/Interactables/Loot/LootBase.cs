using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot
{
    public abstract class LootBase : InteractableBase
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


        public override float ScaleFactor
        {
            get => base.ScaleFactor; set
            {
                base.ScaleFactor = value;
                hitbox.Width = (int)(32 * ScaleFactor);
                hitbox.Height = (int)(32 * ScaleFactor);
            }
        }

        public LootBase(Vector2 pos) : base(pos)
        {
            // we are using 32x32px loot items drop texturs 
            Hitbox = new Rectangle((int)(Position.X), (int)(Position.Y), (int)(32 * ScaleFactor), (int)(32 * ScaleFactor));
            basePosition = pos;

            floatOffset = 3f;
            floatingSpeed = 0.1f;
            floatUp = true;
            floatable = true;

            shadow = true;
            shadowOffset = new Vector2(0, 10);
            shadowPosition = pos - shadowOffset;
        }

        public override void Update(GameTime gameTime)
        {
            if (floatable)
            {
                Float();
            }
        }

        public void Float()
        {
            if (floatUp)
            {
                Position += new Vector2(0, floatingSpeed);
            }
            else
            {
                Position -= new Vector2(0, floatingSpeed);
            }

            if (Position.Y > basePosition.Y + floatOffset)
            {
                floatUp = false;

            }
            if (Position.Y < basePosition.Y - floatOffset)
            {
                floatUp = true;
            }
        }
    }
}
