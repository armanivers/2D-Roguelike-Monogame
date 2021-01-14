using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses
{
    public abstract class Boss : Enemy
    {
        public string bossName;
        public Boss(Vector2 position, int maxHealthPoints, float attackTimespan, float movingSpeed, float scaleFactor = 1.5f) : base(position,maxHealthPoints, attackTimespan, movingSpeed, scaleFactor){
        }


    }
}
