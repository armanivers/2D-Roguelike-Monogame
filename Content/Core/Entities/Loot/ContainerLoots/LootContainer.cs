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


        protected float timeToOpen; // 1.2 = 2 Sekunden
        private float fadingSpeed; //0.00833f;
        protected float openingTimer;

        public string currentAnimation = "Chest_Idle";

        protected List<InventoryItem> dropList;

        public LootContainer(Vector2 pos, float timeToOpen) : base(pos)
        {
            this.closed = true;
            this.timeToOpen = timeToOpen;
            fadingSpeed = 1 / (timeToOpen*100);
            this.openingTimer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            SetAnimation(currentAnimation);
            animationManager?.Update(gameTime);
            base.Update(gameTime);
            if (!closed)
            {
                transparency -= fadingSpeed;
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
