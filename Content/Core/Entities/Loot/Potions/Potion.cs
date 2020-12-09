using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public abstract class Potion : Loot
    {
        public Potion(Vector2 pos) : base(pos) { }
        public abstract void ActivateEffect();
    }
}
