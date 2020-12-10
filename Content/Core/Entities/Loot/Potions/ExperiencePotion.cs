using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Loot;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class ExperiencePotion : Potion
    {
        private int experienceModifier;

        public ExperiencePotion(Vector2 pos) : base(pos)
        {
            experienceModifier = 15;
            texture = TextureManager.ExperiencePotion;
        }

        public override void ActivateEffect()
        {
            Player.Instance.AddExperiencePoints(experienceModifier);
        }

        public override void OnContact()
        {
            ActivateEffect();
            isExpired = true;
        }
    }
}
