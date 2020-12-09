using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot
{
    public abstract class Loot: EntityBasis
    {
        public Loot(Vector2 pos):base(pos) { }

        public abstract void OnContact();

        public override void Update(GameTime gameTime)
        {
            // TODO: Implementierung von Verhalten des Loots
            //throw new NotImplementedException();
        }
    }
}
