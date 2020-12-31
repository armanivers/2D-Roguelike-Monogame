using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class ToolTip : UIElement
    {
        private Player target;

        private Vector2 tooltipPosition;
        private float scalingFactor;
        private float visibility;

        private String interactWithContainer = "Press F To Open";
        private float interactWithContainerLength = TextureManager.FontArial.MeasureString("Press F To Open").X;

        public ToolTip(Player player)
        {
            target = player;
            scalingFactor = 2.2f;
            visibility = 0;
            tooltipPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - interactWithContainerLength/2, Game1.gameSettings.screenHeight/2+60);
        }

        public override void Update(GameTime gameTime)
        {
            if (target.canInteract)
            {
                if (visibility < 1)
                {
                    visibility += 0.1f;
                }
            }
            else
            {
                if (visibility > 0)
                {
                    visibility -= 0.1f;
                }
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visibility > 0)
                spriteBatch.DrawString(TextureManager.FontArial, interactWithContainer, tooltipPosition, Color.White * visibility);
        }

        public override void ForceResolutionUpdate()
        { 
            tooltipPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - interactWithContainerLength / 2, Game1.gameSettings.screenHeight / 2 + 60);
        }

    }
}
