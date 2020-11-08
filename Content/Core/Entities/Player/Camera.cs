using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Player
{
    static class Camera
    {
        public static Matrix transform;
        public static Viewport view = Game1.Viewport;
        public static Vector2 target;

        public static void Update(GameTime gameTime, Player player) 
        {
            target = new Vector2(player.Position.X + (player.Size.X / 2) - 400, player.Position.Y + (player.Size.Y/2) - 200);

            var offest = Matrix.CreateTranslation(new Vector3(-target.X, -target.Y, 0));

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * offest;
        } 

    }
}
