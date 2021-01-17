using _2DRoguelike.Content.Core.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static _2DRoguelike.Content.Core.EntityEffects.EntityEffectsManager;

namespace _2DRoguelike.Content.Core.EntityEffects
{
    public abstract class EntityEffectBase
    {
        public Humanoid owner;
        // how long should the potion heal the player (how long effect lasts)
        public float effectDuration;

        public float effectTimer = 0;
        
        // used to determine end of effect
        public bool isExpired = false;

        public Texture2D effectIcon;
        public EntityEffectBase(Humanoid owner)
        {
            this.owner = owner;
        }
        public abstract void UseEffect();
        public virtual void Update(GameTime gameTime)
        {
            UseEffect();
            effectTimer += 0.1f;
            if (effectTimer >= effectDuration) isExpired = true;
        }
    }
}
