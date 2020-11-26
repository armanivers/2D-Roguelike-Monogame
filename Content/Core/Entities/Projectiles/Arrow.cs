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
        private Humanoid shootingEntity;
        private float timer;
        private const int DAMAGE = 15;
        private const float EXPIRATION_TIMER = 3;
        private const float SPEED = 10f;

        public Arrow(Humanoid creat) : base(new Vector2(creat.Hitbox.X + 16, creat.Hitbox.Y + 16), -7, +5, SPEED)
        {
            shootingEntity = creat;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 13, 13);
            this.Acceleration = Vector2.Normalize(GetDirection());
            this.rotation = (float)Math.Atan2(Acceleration.Y, Acceleration.X);
            this.texture = TextureManager.Arrow;
            this.timer = 0;
        }

        private Vector2 GetDirection() {
            return shootingEntity.GetAttackDirection() - Position;
        }

        public void checkCollision()
        {
            if (!WithinOwnHitbox())
            {
                if (CollidesWithSolidTile())
                {
                    SpeedModifier = 0f;
                    //Velocity = Vector2.Zero;
                    // isExpired = true; // Mittels expireTimer gelöst
                }
                else
                {
                    HittingLogic();
                }

            }
        }

        private bool WithinOwnHitbox() {
            return shootingEntity.Hitbox.Contains(hitbox);
        }

        private void HittingLogic() {
            if (shootingEntity is Player.Player) {
                // TODO: ERSETZEN Durch EnemyList des Raumes

                foreach (var enemy in EntityManager.entities)
                {
                    if (enemy is Enemies.Enemy)
                    { 
                    if (hitbox.Intersects(enemy.Hitbox))
                        {
                            ((Enemies.Enemy)enemy).DeductHealthPoints(DAMAGE);
                            isExpired = true;
                        }
                    } 
                }     
            } else if (shootingEntity is Enemies.Enemy) {
                if (hitbox.Intersects(Player.Player.Instance.Hitbox)) {
                    Player.Player.Instance.DeductHealthPoints(DAMAGE);
                    isExpired = true;
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

            if (timer > EXPIRATION_TIMER)
            {
                this.isExpired = true;
            }
        }
    }
}
