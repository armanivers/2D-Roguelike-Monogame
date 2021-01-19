using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Inventories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class UsableItemsBar : UIElementBasis
    {
        private Player target;

        // space from upper left corner
        private int xSafezone = 30;
        private int ySafezone = 250;

        private int scalingFactor = 1;

        private Vector2 UsableItemsBarPosition;
        private Texture2D usableItemsBarTexture;

        private int itemXOffset = 18;
        private int itemYOffset = 20;
        private int itemsSafeZone = 16;

        private int textXOffset = 15;
        private int textYOffset = 8;
        public UsableItemsBar(Player player)
        {
            target = player;

            usableItemsBarTexture = TextureManager.ui.UsableItemsBar;
            UsableItemsBarPosition = new Vector2(xSafezone* scalingFactor, ySafezone* scalingFactor);
        }

        public override void Update(GameTime gameTime){}

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(usableItemsBarTexture, UsableItemsBarPosition,null, Color.White, 0, Vector2.Zero, 1.4f, SpriteEffects.None, 0);

            for (int i = 0; i < PlayerInventory.USABLE_ITEMS_SIZE; i++)
                {
                var item = target.Inventory.usableItems[i];
                if (item != null) { 
                    spriteBatch.DrawString(TextureManager.GameFont, "" + item.quantity,
                        new Vector2(UsableItemsBarPosition.X+item.icon.Width+ itemXOffset + textXOffset, UsableItemsBarPosition.Y + i * (item.icon.Height + itemsSafeZone) + itemYOffset +textYOffset), Color.White);

                    spriteBatch.Draw(item.icon, new Vector2(UsableItemsBarPosition.X + itemXOffset, UsableItemsBarPosition.Y + i * (item.icon.Height + itemsSafeZone) + itemYOffset),
                        null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }

    }
}
