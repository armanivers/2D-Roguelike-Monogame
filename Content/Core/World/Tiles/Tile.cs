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
            this.x = x*height;
            this.y = y*width;
            this.solid = DetermineSolid(type);
        }

        public bool DetermineSolid(int type)
        {
            bool solid = false;
            switch (type)
            {
                case 0:
                    solid = false;
                    break;
                case 1:
                    solid = true;
                    break;
                default:
                    solid = false;
                    break;
            }
            return solid;
        }

        public Boolean IsSolid()
        {
            return this.solid;
        }
    }
}
