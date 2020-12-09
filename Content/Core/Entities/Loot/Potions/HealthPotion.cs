using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Loot;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class HealthPotion : Potion
    {
        private int healthModifier;
        // timer nur fur z.B. Regeneration wenn es ueber einen Zeitraum hinaus wirkt
        private float timer;

        public HealthPotion(Vector2 pos) : base(pos)
        {
            healthModifier = 20;
            texture = TextureManager.HealthPotion;
            this.Hitbox = new Rectangle((int)Position.X-16, (int)Position.Y-16, 32, 32);
        }

        public override void ActivateEffect()
        {
            Player.Instance.AddHealthPoints(healthModifier);
        }

        public override void OnContact()
        {
            ActivateEffect();
            isExpired = true;
        }
    }
}
