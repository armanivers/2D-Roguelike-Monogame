using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
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
        public static List<EntityEffectBase> activeEnemyEffects = new List<EntityEffectBase>();
        public static List<EntityEffectBase> activePlayerEffects = new List<EntityEffectBase>();


        public enum EffectType
        {
            HealthRegeneration,
            Strength,
            Speed
        }

        public static void Update(GameTime gameTime)
        {
            foreach(var effect in activeEnemyEffects)
            {
                effect.Update(gameTime);
            }
            foreach(var effect in activePlayerEffects)
            {
                effect.Update(gameTime);
            }

            activeEnemyEffects = activeEnemyEffects.Where(x => !x.isExpired).ToList();
            activePlayerEffects = activePlayerEffects.Where(x => !x.isExpired).ToList();
        }

 
        public static void ActivateEffect(EffectType type,Humanoid owner)
        {
            switch(type)
            {
                case EffectType.HealthRegeneration:
                    AddToBuffer(new HealthRegeneration(owner));
                    break;
                case EffectType.Strength:
                    AddToBuffer(new StrengthBoost(owner));
                    break;
                case EffectType.Speed:
                    AddToBuffer(new SpeedBoost(owner));
                    break;
                default:
                    break;
            }
        }

        public static void AddToBuffer(EntityEffectBase effect)
        {
            if (effect.owner is Player)
                activePlayerEffects.Add(effect);
            else
                activeEnemyEffects.Add(effect);
        }

        public static void Unload()
        {
            activeEnemyEffects.Clear();
            activePlayerEffects.Clear();
        }

    }
}
