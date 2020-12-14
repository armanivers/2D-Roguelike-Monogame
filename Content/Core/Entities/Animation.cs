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

        private int frameheight;
        public int FrameHeight { get => frameheight; set => frameheight = value; }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public bool isLooping { get; set; }
        public bool Prioritized { get; set; }
        public bool Reverse { get; set; }
        public Texture2D Texture { get; private set; }
        

        public int yOffest;



        public Animation(Texture2D texture, int yOffest, int frameCount,float frameSpeed, bool isLoop = true, bool priority = false, bool reverse=false,int FrameHeight=64)
        {
            this.FrameHeight = FrameHeight;
            Texture = texture;
            FrameCount = frameCount;
            isLooping = isLoop;
            Prioritized = priority;
            Reverse = reverse;
            // lower FrameSpeed means faster Animation
            FrameSpeed = frameSpeed;
            this.yOffest = yOffest * FrameHeight;
        }
    }
}
