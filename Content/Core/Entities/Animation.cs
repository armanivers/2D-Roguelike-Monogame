using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    public class Animation
    {
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return 64; } }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public bool isLooping { get; set; }
        
        public bool Prioritized { get; set; }
        public Texture2D Texture { get; private set; }
        public int yOffest;



        public Animation(Texture2D texture, int yOffest, int frameCount,float frameSpeed, bool isLoop = true, bool priority = false)
        {
            Texture = texture;
            FrameCount = frameCount;
            isLooping = isLoop;
            Prioritized = priority;
            // lower FrameSpeed means faster Animation
            FrameSpeed = frameSpeed;
            this.yOffest = yOffest * FrameHeight;
        }
    }
}
