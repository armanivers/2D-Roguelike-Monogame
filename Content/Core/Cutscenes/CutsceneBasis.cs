using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Cutscenes
{
    public abstract class CutsceneBasis
    {
        protected int cutsceneDuration;
        protected int timer = 0;
        protected Texture2D cutsceneTexture;
        
        protected Vector2 position;
        protected float transparency;
        protected float scale;
        protected Color color;

        public bool cutsceneDone;

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);

    }
}
