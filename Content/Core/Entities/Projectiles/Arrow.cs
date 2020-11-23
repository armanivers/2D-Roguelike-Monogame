using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Projectiles
{
    class Arrow : Projectile
    {
        private const float expireTimer = 3;
        private float timer;

        public Arrow() : base(new Vector2(Player.Player.Instance.Hitbox.X + 16, Player.Player.Instance.Hitbox.Y + 16), -7, +5, 10f)
        {
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 13, 13);
            this.Acceleration = Vector2.Normalize(InputController.MousePosition - Position);
            this.rotation = (float)Math.Atan2(Acceleration.Y, Acceleration.X);
            this.texture = TextureManager.Arrow;
            this.timer = 0;
        }

        public void checkCollision()
        {
            if (!Player.Player.Instance.hitbox.Contains(Hitbox))
            {
                if (CollidesWithSolidTile())
                {
                    SpeedModifier = 0f;
                    //Velocity = Vector2.Zero;
                    // isExpired = true; // Mittels expireTimer gelöst
                }
                else
                {
                    foreach (var livingEntity in EntityManager.entities)
                    {
                        //&& livingEntity != Player.Player.Instance pruefen damit das projectile dem spieler keinen schaden gibt
                        if (livingEntity is Creature && livingEntity != Player.Player.Instance)
                        {
                            if (hitbox.Intersects(livingEntity.Hitbox))
                            {
                                ((Creature)livingEntity).DeductHealthPoints(15);
                                isExpired = true;
                            }

                        }
                    }
                }

            }
        }

        public bool CollidesWithSolidTile()
        {

            int levelWidth = LevelManager.currentLevel.GetLength(0);
            int levelHeight = LevelManager.currentLevel.GetLength(1);
            // Handling von NullPointer-Exception
            int northWest = Hitbox.X < 0 ? 0 : Hitbox.X / 32;
            int northEast = (Hitbox.X + Hitbox.Width) / 32 >= levelWidth ? levelWidth - 1 : (Hitbox.X + Hitbox.Width) / 32;
            int southWest = Hitbox.Y < 0 ? 0 : Hitbox.Y / 32;
            int southEast = (Hitbox.Y + Hitbox.Height) / 32 >= levelHeight ? levelHeight - 1 : (Hitbox.Y + Hitbox.Height) / 32;


            for (int x = northWest; x <= northEast; x++)
            {
                for (int y = southWest; y <= southEast; y++)
                {
                    if (LevelManager.currentLevel[x, y].IsSolid())
                        return true;
                }
            }
            return false;


            #region AlterCode
            //Point p = new Point(arrowHitbox.X / 32, arrowHitbox.Y / 32);    // NW

            //if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            //{
            //    return true;
            //}
            //p = new Point((arrowHitbox.X + arrowHitbox.Width) / 32, arrowHitbox.Y / 32);   // NE

            //if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            //{
            //    return true;
            //}
            //p = new Point(arrowHitbox.X / 32, ((arrowHitbox.Y + arrowHitbox.Height) / 32));    // SW

            //if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            //{
            //    return true;
            //}

            //p = new Point((arrowHitbox.X + arrowHitbox.Width) / 32, ((arrowHitbox.Y + arrowHitbox.Height) / 32));    // SE
            //if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            //{
            //    return true;
            //}
            //return false;
            #endregion

        }

        public override void Update(GameTime gameTime)
        {
            checkCollision();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Acceleration * flyingSpeed * SpeedModifier;

            if (timer > expireTimer)
            {
                this.isExpired = true;
            }
        }
    }
}
