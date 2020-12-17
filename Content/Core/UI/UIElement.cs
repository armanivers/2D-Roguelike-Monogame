using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    abstract class UIElement
    {
        public abstract void Update(GameTime gametime);
        public abstract void Draw(SpriteBatch spritebatch);
        public virtual void ForceResolutionUpdate() { }
    }
}
