using _2DRoguelike.Content.Core.Entities.Weapons;
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
        private int DAMAGE;
        private const float EXPIRATION_TIMER = 3;
        private const float SPEED = 10f;

        public Arrow(Humanoid creat) : base(new Vector2(creat.Hitbox.X + 16, creat.Hitbox.Y + 25), -7, +5, SPEED)
        {
            this.texture = TextureManager.Arrow;
            DrawOrigin = TextureSize / 2;
            shootingEntity = creat;
            DAMAGE = ((Bow)shootingEntity.CurrentWeapon).weaponDamage;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)(13*ScaleFactor), (int)(13*ScaleFactor));
            this.Acceleration = Vector2.Normalize(GetDirection());
            this.rotation = (float)Math.Atan2(Acceleration.Y, Acceleration.X);
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
            return shootingEntity.Hitbox.Intersects(Hitbox);
        }

        private void HittingLogic() {
            if (shootingEntity is ControllingPlayer.Player) {
                // TODO: ERSETZEN Durch EnemyList des Raumes

                foreach (var enemy in EntityManager.creatures)
                {
                    if (enemy is Enemies.Enemy)
                    { 
                    if (Hitbox.Intersects(enemy.Hitbox))
                        {
                            ((Enemies.Enemy)enemy).DeductHealthPoints(DAMAGE);
                            isExpired = true;
                        }
                    } 
                }     
            } else if (shootingEntity is Enemies.Enemy) {
                if (Hitbox.Intersects(ControllingPlayer.Player.Instance.Hitbox)) {
                    ControllingPlayer.Player.Instance.DeductHealthPoints(DAMAGE);
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
