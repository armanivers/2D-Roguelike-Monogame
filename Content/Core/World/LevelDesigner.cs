using _2DRoguelike.Content.Core.World.Tiles;
using Microsoft.Xna.Framework.Graphics;
using _2DRoguelike.Content.Core.World.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    static class LevelDesigner
    {
        public static Texture2D ForgottenDungeon(char roomObject)
        {
            Texture2D retunTexture = null;
            int textureidentifier=0;
            if (roomObject.Equals(RoomObject.Wall))
            {
                retunTexture = TileTextureManager.stonetileslist[textureidentifier];
            }
            else if (roomObject.Equals(RoomObject.EmptySpace))
            {
                retunTexture = TileTextureManager.woodtileslist[textureidentifier];
            }
            else if (roomObject.Equals(RoomObject.Exit))
            {
                retunTexture = TileTextureManager.laddertileslist[textureidentifier];
            }
            return retunTexture;
        }
        public static Texture2D DoomedWorld(char roomObject)
        {
            Texture2D retunTexture = null;
            int textureidentifier = 0;
            if (roomObject.Equals(RoomObject.Wall))
            {
                textureidentifier = Map.Random.Next(1, 3);
                retunTexture = TileTextureManager.stonetileslist[textureidentifier];
            }
            else if (roomObject.Equals(RoomObject.EmptySpace))
            {
                textureidentifier = 0;
                retunTexture = TileTextureManager.grasstileslist[textureidentifier];
            }
            else if (roomObject.Equals(RoomObject.Exit))
            {
                textureidentifier = 1;
                retunTexture = TileTextureManager.laddertileslist[textureidentifier];
            }
            return retunTexture;
        }
        public static Texture2D HeavenOrHell(char roomObject)
        {
            Texture2D retunTexture = null;
            int textureidentifier = 0;
            if (roomObject.Equals(RoomObject.Wall))
            {
                textureidentifier =3;
                retunTexture = TileTextureManager.stonetileslist[textureidentifier];
            }
            else if (roomObject.Equals(RoomObject.EmptySpace))
            {
                textureidentifier = 0;
                retunTexture = TileTextureManager.mabletileslist[textureidentifier];
            }
            else if (roomObject.Equals(RoomObject.Exit))
            {
                textureidentifier = 2;
                retunTexture = TileTextureManager.laddertileslist[textureidentifier];
            }
            return retunTexture;
        }

    }
}
