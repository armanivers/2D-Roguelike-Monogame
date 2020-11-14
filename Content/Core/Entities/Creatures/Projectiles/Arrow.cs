using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Projectiles
{
    class Arrow : EntityBasis
    {
        private float lifeSpan;
        private float timer;
        private Vector2 origin;
        private Vector2 targetDirection;
        private Vector2 direction;
        private float velocity;

        public Arrow(Vector2 position,Vector2 targetDirection) :base(position)
        {
            this.targetDirection = targetDirection;
            this.origin = Position;
            this.direction =  32*(targetDirection - origin);
            Hitbox = new Rectangle((int)position.X - 16, (int)position.Y - 16, 32, 32);
            this.lifeSpan = 5;
            this.velocity = 2f;
            orientation = 1;
            this.texture = TextureManager.tiles[0];
            Debug.Print("CREATED Pos = " + Position.ToPoint() + " target = " + targetDirection.ToPoint() + " direction =" + direction.ToPoint());
        }

        public override void Update(GameTime gameTime)
        {
            //timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if (timer > lifeSpan)
            //{
            //    isExpired = true;
            //}
            //else
            Position = new Vector2(Position.X + 10, Position.Y);
        }
    }
}
