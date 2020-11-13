using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    abstract class Enemy : Humanoid
    {
        protected override Vector2 GetDirection()
        {
            // TODO: KI nach Angaben fragen
            return new Vector2(0, 0);
        }

        public Enemy(Vector2 position, int maxHealthPoints, float attackCooldown, float movingSpeed) : base(position, maxHealthPoints, attackCooldown, movingSpeed)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }


}
