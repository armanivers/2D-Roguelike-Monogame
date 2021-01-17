using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Inventories;
using _2DRoguelike.Content.Core.EntityEffects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class PlayerEffects : UIElementBasis
    {
        private Vector2 UsableItemsBarPosition;

        public PlayerEffects(Player player)
        {
            //skillbarPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - (skillbarTexture.Width * scalingFactor - skillbarWhitespaceWidth) / 2, Game1.gameSettings.screenHeight - (skillbarTexture.Height * scalingFactor - skillbarWhitespaceHeight) - skillbarYOffset);

            UsableItemsBarPosition = new Vector2(220, Game1.gameSettings.screenHeight / 2);

        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int i = 0;
            foreach (var effect in EntityEffectsManager.activePlayerEffects)
            {

                    spriteBatch.DrawString(TextureManager.FontArial, "" + (int)(effect.effectDuration-effect.effectTimer),
                        new Vector2(UsableItemsBarPosition.X + i * effect.effectIcon.Width*1.5f + 15, UsableItemsBarPosition.Y + effect.effectIcon.Height * 1.5f), Color.White);

                    spriteBatch.Draw(effect.effectIcon, new Vector2(UsableItemsBarPosition.X + i * effect.effectIcon.Width * 1.5f + 15, UsableItemsBarPosition.Y),
                        null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
                i++;
            }
        }

        public override void ForceResolutionUpdate()
        {
            //skillbarPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - (skillbarTexture.Width * scalingFactor - skillbarWhitespaceWidth) / 2, Game1.gameSettings.screenHeight - (skillbarTexture.Height * scalingFactor - skillbarWhitespaceHeight) - skillbarYOffset);
        }

    }
}
