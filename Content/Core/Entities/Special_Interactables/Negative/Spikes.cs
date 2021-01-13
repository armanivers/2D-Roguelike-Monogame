using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Special_Interactables.Negative
{
    class Spikes : SpecialInteractableBase
    {
        private int damage = 1;
        private float timer = 0;
        private float effectCooldown = 10; 

        public Spikes(Vector2 pos) : base(pos)
        {
            texture = TextureManager.Spikes01;
            ScaleFactor = 1.5f;
        }

        public override void ActivateEffect()
        {
            if(timer >= effectCooldown)
            {
                Player.Instance.DeductHealthPoints(damage);
                timer = 0;
            }
            timer += 0.1f;
        }

    }
}
