using _2DRoguelike.Content.Core.Entities.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.GameDebug
{
    class PlayerDebug
    {
        private Vector2 playerPositionOnScreen;
        private Vector2 playerPositionOnMap;
        private Vector2 mousePosition;
        public PlayerDebug()
        { 
            playerPositionOnScreen = new Vector2(0, 0);
            playerPositionOnMap = new Vector2(0, 40);
            mousePosition = new Vector2(0, 80);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(TextureManager.FontArial, 
                "Position Player auf Screen: {X: " + (int)Player.Instance.Position.X + " Y:" + (int)Player.Instance.Position.Y + "}",
                playerPositionOnScreen, Color.White);
            spriteBatch.DrawString(TextureManager.FontArial,
            "Position Player auf Map: {X: " + (int)(Player.Instance.GetTileCollisionHitbox().X / 32) + " Y:" + (int)(Player.Instance.GetTileCollisionHitbox().Y / 32) + "}",
                playerPositionOnMap, Color.White);
            spriteBatch.DrawString(TextureManager.FontArial,
                "Position Maus auf Map: {X: " + (int)InputController.MousePosition.X/32 + " Y:" + (int)InputController.MousePosition.Y/32 + "}",
                mousePosition, Color.White);
        }
    }
}
