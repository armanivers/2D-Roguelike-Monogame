using _2DRoguelike.Content.Core.World.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World.Rooms
{
    static class RoomFactory
    {
        public static Room RandomRoomWithEnemies()
        {
            Room returnvalue = new Room();
            //returnvalue.placeEnemies();
            return returnvalue;
        }
        public static Room RoomWithChest()
        {
            return new Room(12, 8);
        }
        public static Room StartingRoom()
        {
            Room returnvalue = new Room(10,10);
            return returnvalue;
        }
        public static Room FinalRoom()
        {
            Room returnvalue = new Room();
            returnvalue.setExit();
            return returnvalue;
        }
    }
}