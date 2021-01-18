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

        // space from bottom left corner
        private int xSafezone = 30;
        private int ySafezone = 150;

        private int effectIconSpace = 15;

        private float iconScaleFactor = 1.5f;

        public PlayerEffects()
        {
            UsableItemsBarPosition = new Vector2(xSafezone, Game1.gameSettings.screenHeight - ySafezone);
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int i = 0;
            foreach (var effect in EntityEffectsManager.activePlayerEffects)
            {

                    spriteBatch.DrawString(TextureManager.GameFont, "" + (int)(effect.effectDuration-effect.effectTimer),
                        new Vector2(UsableItemsBarPosition.X + i * effect.effectIcon.Width* iconScaleFactor + effectIconSpace, UsableItemsBarPosition.Y + effect.effectIcon.Height * iconScaleFactor), Color.White);

                    spriteBatch.Draw(effect.effectIcon, new Vector2(UsableItemsBarPosition.X + i * effect.effectIcon.Width * iconScaleFactor + effectIconSpace, UsableItemsBarPosition.Y),
                        null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
                i++;
            }
        }

        public override void ForceResolutionUpdate()
        {
            UsableItemsBarPosition = new Vector2(xSafezone, Game1.gameSettings.screenHeight - ySafezone);
        }

    }
}
