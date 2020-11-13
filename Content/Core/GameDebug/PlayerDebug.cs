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
        private Vector2 playerPosition;
        private Vector2 mousePosition;
        public PlayerDebug()
        { 
            playerPosition = new Vector2(0, 0);
            mousePosition = new Vector2(0, 40);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(TextureManager.FontArial, 
                "X: " + (int)Player.Instance.Position.X/32 + " Y:" + (int)Player.Instance.Position.Y/32,
                playerPosition, Color.White);
            spriteBatch.DrawString(TextureManager.FontArial,
                "X: " + (int)InputController.MousePosition.X + " Y:" + (int)InputController.MousePosition.Y,
                mousePosition, Color.White);
        }
    }
}
