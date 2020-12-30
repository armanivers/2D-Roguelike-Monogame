using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World.Rooms
{
    static class RoomFactory
    {
        public static Map TestMap()
        {
            return new TestMap(24, 24);
        }
        public static Room RandomRoomWithEnemies()
        {
            Room returnvalue = new Room();
            //returnvalue.placeEnemies();
            return returnvalue;
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