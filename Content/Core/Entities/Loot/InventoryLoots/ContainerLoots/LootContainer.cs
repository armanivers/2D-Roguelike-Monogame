﻿using Microsoft.Xna.Framework;
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
        public bool Closed { get; }

        protected float timeToOpen;
        protected float openingTimer;

        public LootContainer(Vector2 pos,int timeToOpen) : base(pos) {
            this.closed = true;
            this.timeToOpen = timeToOpen;
            this.openingTimer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // 3 sekunden  = 0% => 0
            // 1.5 sek = 50% => 0.5
            // 0 sek = 100% => 1
            if(!closed)
            {
                transparency -= 0.01f;
                openingTimer += 0.01f;
            }

            if(openingTimer >= timeToOpen)
            {
                OpenContainer();
            }
        }

        public abstract void OpenContainer();
    }
}
