using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    public abstract class InteractableBase : EntityBasis
    {
        public InteractableBase(Vector2 pos) : base(pos)
        {
            EntityManager.AddInteractableEntity(this);
        }

        public virtual void OnContact(){}
    }
}
