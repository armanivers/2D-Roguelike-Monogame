using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Projectiles
{
    abstract class Projectile : EntityBasis
    {
        public readonly float flyingSpeed;
        public float SpeedModifier { get; set; }

        protected int xHitboxOffset;
        protected int yHitboxOffset;
        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                hitbox.X = (int)(value.X + xHitboxOffset * ScaleFactor);
                hitbox.Y = (int)(value.Y + xHitboxOffset * ScaleFactor);

                if (animationManager != null)
                {
                    animationManager.Position = base.Position;
                }
            }
        }



        public Projectile(Vector2 pos, int xHitboxOffset, int yHitboxOffset, float speed, float scaleFactor = 1f) : base(pos, scaleFactor)
        {
            EntityManager.AddProjectileEntity(this);
            this.xHitboxOffset = xHitboxOffset;
            this.yHitboxOffset = yHitboxOffset;
            flyingSpeed = speed;
            SpeedModifier = 1;
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
    }
}
