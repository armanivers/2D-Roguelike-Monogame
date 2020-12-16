using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public abstract class LootContainer : LootBase
    {
        // kann genutzt werden um z.B. eine Animation abzuspielen
        protected bool closed;
        public bool Closed { get { return closed; } }

        private const float DISAPPEARING_SPEED = 0.01f;

        protected float timeToOpen;
        protected float openingTimer;

        public string currentAnimation = "Chest_Idle";

        protected List<InventoryItem> dropList;

        public LootContainer(Vector2 pos, float timeToOpen) : base(pos)
        {
            this.closed = true;
            this.timeToOpen = timeToOpen;
            this.openingTimer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            SetAnimation(currentAnimation);
            animationManager?.Update(gameTime);
            base.Update(gameTime);
            if (!closed)
            {
                transparency -= DISAPPEARING_SPEED;
                openingTimer += 0.01f;
                if (openingTimer >= timeToOpen)
                {
                    OpenContainer();
                }
            }

        }

        public override void OnContact()
        {
            StatisticsManager.LootOpen();
            currentAnimation = "Chest_Open";
            closed = false;
        }

        public abstract void OpenContainer();
    }
}
