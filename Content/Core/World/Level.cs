using _2DRoguelike.Content.Core.World.ExitConditions;
using _2DRoguelike.Content.Core.World.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    class Level
    {

        public Map map { get;}
        public ExitCondition exitCondition { get; set; }

        public Level(Map map, ExitCondition exit)
        {
            this.map = map;
            exitCondition = exit;
        }
    }
}
