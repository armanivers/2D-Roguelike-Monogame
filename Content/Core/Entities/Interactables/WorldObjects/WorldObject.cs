using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Interactables.WorldObjects
{
    public abstract class WorldObject : InteractableBase
    {
        public WorldObject(Vector2 pos) : base(pos)
        {
            // set hitbox + texture in subclasses!
        }
    }
}
