using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.EntityEffects.PotionEffects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core.EntityEffects
{
    public static class EntityEffectsManager
    {
        public static List<EntityEffectBase> activeEffects = new List<EntityEffectBase>();

        public enum EffectType
        {
            HealthRegeneration,
            Strength
        }

        public static void Update(GameTime gameTime)
        {
            foreach(var effect in activeEffects)
            {
                effect.Update(gameTime);
            }

            activeEffects = activeEffects.Where(x => !x.isExpired).ToList();
        }

 
        public static void ActivateEffect(EffectType type,Humanoid owner)
        {
            switch(type)
            {
                case EffectType.HealthRegeneration:
                    activeEffects.Add(new HealthRegeneration(owner));
                    break;
                case EffectType.Strength:
                    break;
                default:
                    
                    break;
            }
        }

        public static void Unload()
        {
            activeEffects.Clear();
        }

    }
}
