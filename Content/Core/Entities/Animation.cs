using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    class Animation
    {
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get; private set; }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get; private set; }
        public bool isLooping { get; set; }
        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount, int frameWidth, int frameHeight)
        {
            Texture = texture;
            FrameCount = frameCount;
            FrameHeight = frameHeight;
            FrameWidth = frameWidth;
            isLooping = true;
            FrameSpeed = 1f;
        }
    }
}
