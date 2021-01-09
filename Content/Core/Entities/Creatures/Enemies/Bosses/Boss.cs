using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses
{
    public abstract class Boss : Enemy
    {
        public Boss(Vector2 position, int maxHealthPoints, float attackTimespan, float movingSpeed) : base(position,maxHealthPoints, attackTimespan, movingSpeed){
            ScaleFactor = 1.5f;
        }

        // TODO

    }
}
