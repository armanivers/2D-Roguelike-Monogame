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
        
        private Vector2 UsableItemsBarPosition;

        public UsableItemsBar(Player player)
        {
            target = player;

            //skillbarPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - (skillbarTexture.Width * scalingFactor - skillbarWhitespaceWidth) / 2, Game1.gameSettings.screenHeight - (skillbarTexture.Height * scalingFactor - skillbarWhitespaceHeight) - skillbarYOffset);

            UsableItemsBarPosition = new Vector2(Game1.gameSettings.screenWidth / 2+120, Game1.gameSettings.screenHeight/2);

        }

        public override void Update(GameTime gameTime){}

        public override void Draw(SpriteBatch spriteBatch)
        {
 
                for (int i = 0; i < PlayerInventory.USABLE_ITEMS_SIZE; i++)
                {
                var item = target.Inventory.usableItems[i];
                if (item != null) { 
                    spriteBatch.DrawString(TextureManager.FontArial, ""+item.quantity, 
                        new Vector2(UsableItemsBarPosition.X + i * item.icon.Width + 15, UsableItemsBarPosition.Y + item.icon.Height), Color.White);

                    spriteBatch.Draw(item.icon, new Vector2(UsableItemsBarPosition.X + i * item.icon.Width+15, UsableItemsBarPosition.Y), 
                        null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }

        public override void ForceResolutionUpdate()
        {
            //skillbarPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - (skillbarTexture.Width * scalingFactor - skillbarWhitespaceWidth) / 2, Game1.gameSettings.screenHeight - (skillbarTexture.Height * scalingFactor - skillbarWhitespaceHeight) - skillbarYOffset);
        }

    }
}
