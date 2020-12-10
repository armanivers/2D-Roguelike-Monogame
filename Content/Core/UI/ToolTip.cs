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
    class ToolTip
    {
        private Player target;

        private Vector2 tooltipPosition;
        private float scalingFactor;

        private bool interactable;

        private String interactWithContainer = "Press F To Open";
        private float interactWithContainerLength = TextureManager.FontArial.MeasureString("Press F To Open").X;

        public ToolTip(Player player)
        {
            target = player;
            scalingFactor = 2.2f;

            tooltipPosition = new Vector2(GameSettings.screenWidth / 2 - interactWithContainerLength/2, GameSettings.screenHeight/2+60);
        }

        public void Update(GameTime gameTime)
        {
            interactable = target.canInteract;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(interactable)
            {
                spriteBatch.DrawString(TextureManager.FontArial, interactWithContainer, tooltipPosition, Color.White);
            }

        }

        public void ForceResolutionUpdate()
        {
            tooltipPosition = new Vector2(GameSettings.screenWidth / 2 - interactWithContainerLength / 2, GameSettings.screenHeight / 2 + 60);
        }

    }
}
