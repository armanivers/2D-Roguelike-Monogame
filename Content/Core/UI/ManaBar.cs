using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class ManaBar : UIElementBasis
    {
        private Player target;

        private int currentMana;
        private int fullWidth;
        private int currentWidth;

        // space from upper left corner
        private int xSafezone = 25;
        private int ySafezone = 90;

        // text position relative to xpbar position
        //private int textOffsetY = 25;

        private Texture2D manabarContainer;
        private Texture2D manabarBar;

        private float scalingFactor = 1.5f;

        // red bar starts at x = 69, before it everything is empty
        private int manabarOffsetStart = 19;
        // redbar ends 3 pixels before the end of iamge, after it everything is empty
        private int manabarOffsetEnd = 22;

        private Vector2 manabarContainerPosition;
        private Vector2 manabarBarPosition;
        private Vector2 textPosition;

        public ManaBar(Player player)
        {
            target = player;

            manabarContainer = TextureManager.ui.ManaBarContainer;
            manabarBar = TextureManager.ui.ManaBarBar;

            manabarContainerPosition = new Vector2(xSafezone * scalingFactor, ySafezone * scalingFactor);
            manabarBarPosition = manabarContainerPosition + new Vector2(manabarOffsetStart * scalingFactor, 0);

            fullWidth = manabarBar.Width - manabarOffsetStart - manabarOffsetEnd;

            textPosition = manabarContainerPosition - new Vector2(0, 20);

            currentWidth = fullWidth;
            currentMana = (int) player.Mana;
        }

        public override void Update(GameTime gameTime)
        {

            currentMana = (int) target.Mana;

            currentWidth = (int)(currentMana / Creature.MAX_MANA * fullWidth);

            //Debug.Print("Percentage {0} width = {1}", target.LevelupPercentage, currentWidth);
            //textPosition = containerPositon + new Vector2(redbarOffsetStart + fullWidth / 2 - TextureManager.FontArial.MeasureString("" + target.HealthPoints).X / 2, xSafezone);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(manabarContainer, manabarContainerPosition, null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            spriteBatch.Draw(manabarBar, manabarBarPosition, new Rectangle(manabarOffsetStart, 0, currentWidth, manabarBar.Height), Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            spriteBatch.DrawString(TextureManager.GameFont, "Mana: " + currentMana, textPosition, Color.Gold);
        }

    }
}
