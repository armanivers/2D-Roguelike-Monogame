using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World.Maps
{
    class BossMap :Map
    {
        public Room bossroom { get; set; }
        public  BossMap(int width, int height) : base(width, height)
        {
            bossroom = new Room(width,height);
            bossroom.setExit();
            map = fillTile(bossroom.room);
        }

        public override Vector2 getSpawnpoint()
        {
            return new Vector2((bossroom.Width / 2) , (bossroom.Height / 2) );
        }

        public override void Update(Player player)
        {
            
        }

        public override void clearEnemies()
        {
            
        }

        public override void clearEnities()
        {
            
        }
    }
}
