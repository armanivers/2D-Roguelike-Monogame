using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class Skillbar
    {
        private Player target;

        private Texture2D skillbarTexture;
        private Vector2 skillbarPosition;

        private Texture2D usedSlotTexture;
        private Vector2 usedSlotPosition;
        private float slotFullHeight;
        private float slotCurrentHeight;

        private float scalingFactor;

        private float xOffset;
        private float yOffset;

        public Skillbar(Player player)
        {
            target = player;
            scalingFactor = 2.2f;
            skillbarTexture = TextureManager.skillbar;
            skillbarPosition = new Vector2(GameSettings.screenWidth / 2 - skillbarTexture.Width * scalingFactor / 2, GameSettings.screenHeight-skillbarTexture.Height * scalingFactor - 30);
            usedSlotTexture = TextureManager.slotUsed;
            xOffset = 12;
            yOffset = 14;
            usedSlotPosition = new Vector2(skillbarPosition.X+xOffset,skillbarPosition.Y+yOffset);
            slotFullHeight = usedSlotTexture.Height;
            slotCurrentHeight = slotFullHeight;
        }

        public void Update(GameTime gameTime)
        {
            var cooldown = target.AttackTimeSpanTimer;
            slotCurrentHeight = usedSlotTexture.Height - cooldown*9;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(skillbarTexture, skillbarPosition, null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            spriteBatch.Draw(usedSlotTexture, usedSlotPosition, new Rectangle((int)usedSlotPosition.Y,(int)usedSlotPosition.X,usedSlotTexture.Width,(int) slotCurrentHeight), Color.White*0.5f, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
        }

    }
}
