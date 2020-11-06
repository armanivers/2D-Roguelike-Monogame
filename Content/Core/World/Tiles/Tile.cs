using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World.Tiles
{
    class Tile
    {
        public Texture2D texture;
        public int width;
        public int height;
        private int type;
        private bool solid;
        public int x;
        public int y;
        public Tile(int type,int x, int y)
        {
            this.type = type;
            this.texture = TextureManager.tiles[type];
            this.width = 32;
            this.height = 32;
            this.x = x*width;
            this.y = y*height;
        }

        public Boolean IsSolid()
        {
            return this.solid;
        }
    }
}
