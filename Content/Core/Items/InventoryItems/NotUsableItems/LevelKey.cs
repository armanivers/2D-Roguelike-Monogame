﻿using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Items.InventoryItems.NotUsableItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Items.ObtainableItems
{
    
    // fuer die Zukunft:
    // Hauptklasse ItemAbstract erstellen, davon 2 abstrakte unterklassen -> InventoryItem (weapons) und ObtainableItem(keys, questitems fuer quests von npcs, 
    public class LevelKey : NotUsableItem
    {
        public LevelKey(Humanoid owner) : base(owner)
        {

        }

    }
}
