using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.GameDebugger;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Special_Interactables
{
    abstract class SpecialInteractableBase : EntityBasis
    {
        public SpecialInteractableBase(Vector2 pos, float scaleFactor = 1f) : base(pos, scaleFactor)
        {
            EntityManager.AddSpecialInteractable(this);
            // a trap hitbox is now 32x32, size of a tile block in world
            Hitbox = new Rectangle((int)(Position.X), (int)(Position.Y), (int)(32 * ScaleFactor), (int)(32 * ScaleFactor));
        }

        public abstract void ActivateEffect();

        public override void Update(GameTime gameTime)
        {
            GameDebug.AddToBoxDebugBuffer(hitbox, Color.Firebrick);
            // statt hitbox ggf tilehitbox?
            if(Hitbox.Intersects(Player.Instance.GetTileCollisionHitbox()))
            {
                ActivateEffect();
            }
        }
    }
}
