using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Inventories;
using _2DRoguelike.Content.Core.EntityEffects;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class PlayerCompass : UIElementBasis
    {
        private Vector2 compassPosition;

        // space from bottom left corner
        private int xSafezone = 90;
        private int ySafezone = 150;

        private float scalingFactor = 0.9f;

        private Vector2 rotationVector;
        public PlayerCompass()
        {
            compassPosition = new Vector2(Game1.gameSettings.screenWidth - xSafezone,Game1.gameSettings.screenHeight - ySafezone);
        }

        public override void Update(GameTime gameTime) 
        {
            rotationVector = new Vector2(Room.exithitbox.X + Room.exithitbox.Width / 2, Room.exithitbox.Y + Room.exithitbox.Height / 2) - new Vector2(Player.Instance.HitboxCenter.X, Player.Instance.HitboxCenter.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.ui.Compass,
                compassPosition,
                null,
                Color.White,
                (float)(Math.Atan2(rotationVector.Y, rotationVector.X)
                + Math.PI / 2),
                new Vector2(TextureManager.ui.Compass.Width/ 2, TextureManager.ui.Compass.Height / 2),
                scalingFactor,
                SpriteEffects.None,
                0f
            );
        }

        public override void ForceResolutionUpdate()
        {
            compassPosition = new Vector2(Game1.gameSettings.screenWidth - xSafezone, Game1.gameSettings.screenHeight - ySafezone);
        }

    }
}
