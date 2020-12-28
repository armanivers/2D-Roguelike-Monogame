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
        private char ro;
        private bool solid;
        public int x;
        public int y;
        public Tile(int type, int x, int y)
        {
            this.type = type;
            //this.texture = TileTextureManager.tiles[type];
            this.width = 32;
            this.height = 32;
            this.x = x * height;
            this.y = y * width;
            this.solid = DetermineSolid(type);
        }
        public Tile(char ro, int x, int y)
        {
            this.ro = ro;
            this.texture = DetermineTexture(ro);
            this.width = 32;
            this.height = 32;
            this.x = x * height;
            this.y = y * width;
        }
        public Tile(bool type, int x, int y)
        {
            this.type = type ? 1 : 0;
            this.texture = type?TileTextureManager.woodtileslist[0]: TileTextureManager.stonetileslist[0];
            this.width = 32;
            this.height = 32;
            this.x = x * height;
            this.y = y * width;
            this.solid = DetermineSolid(this.type);
        }
        private Texture2D DetermineTexture(char roomObject)
        {
            Texture2D retunTexture=null;
            int tmp;
            if (roomObject.Equals(RoomObject.Wall))
            {
                tmp = Map.Random.Next(0, TileTextureManager.STONETILES-1);
                retunTexture = TileTextureManager.stonetileslist[tmp];
                solid = true;
            }else if (roomObject.Equals(RoomObject.EmptySpace))
            {
                tmp = Map.Random.Next(0, TileTextureManager.WOODTILES-1);
                retunTexture = TileTextureManager.woodtileslist[tmp];
                solid = false;
            }
            else if (roomObject.Equals(RoomObject.Exit))
            {
                tmp = Map.Random.Next(0, TileTextureManager.LADDERTILES-1);
                retunTexture = TileTextureManager.laddertileslist[tmp];
                solid = false;
            }
            return retunTexture;
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
