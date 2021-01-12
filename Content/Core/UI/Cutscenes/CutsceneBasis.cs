using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Cutscenes
{
    public abstract class CutsceneBasis
    {
        protected float cutsceneDuration;
        protected float timer = 0;

        // basic duration values, can be overwritten
        protected float fadeInDuration = 0.6f; // 1 sekunden
        protected float fadeInSpeed;

        protected float displayDuration = 1.2f; // 2 sekunden
        
        protected float fadeOutDuration = 0.6f; // 1 sekunden
        protected float fadeOutSpeed;

        // used for scenes which don't need start/middle/end part
        protected float sceneDuration = 8f;

        // by default cutscene is not interactable
        protected bool interactable = false;
        protected bool buttonPressed = false;

        // after 15 seconds, interactable scenes are auto skipped
        protected int interactbleAutoSkipDuration = 15; 

        protected Texture2D cutsceneTexture;
        
        protected Vector2 position;
        protected float transparency;
        protected float scale;
        protected Color color;

        public bool cutsceneDone;

        public CutsceneBasis()
        {
            cutsceneDuration = fadeInDuration + displayDuration + fadeOutDuration;
            fadeInSpeed = 1 / (fadeInDuration * 100);
            fadeOutSpeed = 1 / (fadeOutDuration * 100);
            if (interactable) cutsceneDuration += interactbleAutoSkipDuration;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);

    }
}
