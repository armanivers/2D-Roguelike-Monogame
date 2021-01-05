using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI
{
    public class SimulatedArrow
    {
        public Enemy shootingEntity;
        public Rectangle Hitbox = Rectangle.Empty;
        protected int xHitboxOffset;
        protected int yHitboxOffset;
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                Hitbox.X = (int)value.X + xHitboxOffset;
                Hitbox.Y = (int)value.Y + xHitboxOffset;
            }
        }
        public readonly float flyingSpeed = 10f;
        public float speedModifier = 1;

        public Vector2 Acceleration;

        public SimulatedArrow(Enemy shootingEntity)
        {
            this.shootingEntity = shootingEntity;
            position = new Vector2(shootingEntity.Hitbox.X + 16, shootingEntity.Hitbox.Y + 16);
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 13, 13);
            xHitboxOffset = -7;
            yHitboxOffset = 5;
            Acceleration = Vector2.Normalize(shootingEntity.GetAttackDirection() - Position);
        }

        public bool TestForImpact()
        {

            while (true)
            {
                if (CollidesWithSolidTile())
                    return false;
                if (!Hitbox.Intersects(shootingEntity.Hitbox))
                {
                    if (Hitbox.Intersects(ControllingPlayer.Player.Instance.Hitbox))
                    {
                        return true;
                    }
                }

                Position += Acceleration * flyingSpeed * speedModifier;
            }
            return false;
        }

        public bool CollidesWithSolidTile()
        {

            int levelWidth = LevelManager.currenttilemap.GetLength(0);
            int levelHeight = LevelManager.currenttilemap.GetLength(1);
            // Handling von NullPointer-Exception
            int northWest = Hitbox.X < 0 ? 0 : Hitbox.X / 32;
            int northEast = (Hitbox.X + Hitbox.Width) / 32 >= levelWidth ? levelWidth - 1 : (Hitbox.X + Hitbox.Width) / 32;
            int southWest = Hitbox.Y < 0 ? 0 : Hitbox.Y / 32;
            int southEast = (Hitbox.Y + Hitbox.Height) / 32 >= levelHeight ? levelHeight - 1 : (Hitbox.Y + Hitbox.Height) / 32;


            for (int x = northWest; x <= northEast; x++)
            {
                for (int y = southWest; y <= southEast; y++)
                {
                    if (LevelManager.currenttilemap[x, y].IsSolid())
                        return true;
                }
            }
            return false;


        }



    }
}
