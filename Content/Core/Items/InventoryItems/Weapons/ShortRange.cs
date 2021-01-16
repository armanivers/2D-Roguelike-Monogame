using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.World;
using _2DRoguelike.Content.Core.GameDebugger;
using Microsoft.Xna.Framework;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public abstract class ShortRange : Weapon
    {
        private byte maximumHitsPerAttack;

        // Für gleichzeitiges Abziehen von zwei Richtungen (z.B. Schulter und Beine nicht miteinbeziehen)
        private float heightReduction = 0.8f;
        private float widthReduction = 1f;

        private float rangeMultiplierX;
        private float rangeMultiplierY;
        public float RangeMultiplierX { get => rangeMultiplierX; }
        public float RangeMultiplierY { get => rangeMultiplierY; }

        public ShortRange(Humanoid Owner, float rangeX, float rangeY, int weaponDamage, float weaponCooldown, byte maximumHitsPerAttack = 1) : base(Owner, weaponDamage, weaponCooldown)
        {
            this.rangeMultiplierX = rangeX * widthReduction;
            this.rangeMultiplierY = rangeY * heightReduction;

            this.maximumHitsPerAttack = maximumHitsPerAttack;
        }

        public Rectangle[] GetEffectiveRange()
        {
            Rectangle[] ret = new Rectangle[2];

            int attackHitboxWidth = Owner.Hitbox.Width;
            int attackHitboxHeight = Owner.Hitbox.Height;

            float tmpRangeMultiplierX = rangeMultiplierX;
            float tmpRangeMultiplierY = rangeMultiplierY;

            // horizontal
            Vector2 attackHitboxRangeUpperLeft = new Vector2(Owner.Hitbox.X - attackHitboxWidth * tmpRangeMultiplierX +
                  (attackHitboxWidth * (1f - tmpRangeMultiplierX) < 0 ? 0 : (attackHitboxWidth * (1f - tmpRangeMultiplierX) /*dieser Teil für Berücksichtigung der widthReduction*/ )),
              Owner.Hitbox.Y - attackHitboxHeight * tmpRangeMultiplierY +
               (attackHitboxHeight * (1f - tmpRangeMultiplierY) < 0 ? 0 : attackHitboxHeight * (1f - tmpRangeMultiplierY)/*dieser Teil für Berücksichtigung der heightReduction*/ ));

            Vector2 attackHitboxRangeDownRight = new Vector2(Owner.Hitbox.X + Owner.Hitbox.Width -
                   (attackHitboxWidth * (1f - tmpRangeMultiplierX) < 0 ? 0 : (attackHitboxWidth * (1f - tmpRangeMultiplierX) /*dieser Teil für Berücksichtigung der widthReduction*/ )),
                Owner.Hitbox.Y + Owner.Hitbox.Height -
                                  (attackHitboxHeight * (1f - tmpRangeMultiplierY) < 0 ? 0 : attackHitboxHeight * (1f - tmpRangeMultiplierY)/*dieser Teil für Berücksichtigung der heightReduction*/ ));

            attackHitboxRangeDownRight.X += (int)(attackHitboxWidth * tmpRangeMultiplierX);
            attackHitboxRangeDownRight.Y += (int)(attackHitboxHeight * tmpRangeMultiplierY);


            ret[0] = new Rectangle();
            ret[0].X = (int)attackHitboxRangeUpperLeft.X;
            ret[0].Y = (int)attackHitboxRangeUpperLeft.Y;
            ret[0].Width = (int)Vector2.Distance(attackHitboxRangeUpperLeft, new Vector2(attackHitboxRangeDownRight.X, attackHitboxRangeUpperLeft.Y));
            ret[0].Height = (int)Vector2.Distance(attackHitboxRangeUpperLeft, new Vector2(attackHitboxRangeUpperLeft.X, attackHitboxRangeDownRight.Y));


            // vertikal
            int swap = attackHitboxWidth;
            attackHitboxWidth = attackHitboxHeight;
            attackHitboxHeight = swap;

            float swap2 = rangeMultiplierX;
            tmpRangeMultiplierX = rangeMultiplierY;
            tmpRangeMultiplierY = swap2;

            attackHitboxRangeUpperLeft = new Vector2(Owner.Hitbox.X - attackHitboxWidth * tmpRangeMultiplierX +
                 (attackHitboxWidth * (1f - tmpRangeMultiplierX) < 0 ? 0 : (attackHitboxWidth * (1f - tmpRangeMultiplierX) /*dieser Teil für Berücksichtigung der widthReduction*/ )),
             Owner.Hitbox.Y - attackHitboxHeight * tmpRangeMultiplierY +
              (attackHitboxHeight * (1f - tmpRangeMultiplierY) < 0 ? 0 : attackHitboxHeight * (1f - tmpRangeMultiplierY)/*dieser Teil für Berücksichtigung der heightReduction*/ ));

            attackHitboxRangeDownRight = new Vector2(Owner.Hitbox.X + Owner.Hitbox.Width -
                   (attackHitboxWidth * (1f - tmpRangeMultiplierX) < 0 ? 0 : (attackHitboxWidth * (1f - tmpRangeMultiplierX) /*dieser Teil für Berücksichtigung der widthReduction*/ )),
                Owner.Hitbox.Y + Owner.Hitbox.Height -
                                  (attackHitboxHeight * (1f - tmpRangeMultiplierY) < 0 ? 0 : attackHitboxHeight * (1f - tmpRangeMultiplierY)/*dieser Teil für Berücksichtigung der heightReduction*/ ));

            attackHitboxRangeDownRight.X += (int)(attackHitboxWidth * tmpRangeMultiplierX);
            attackHitboxRangeDownRight.Y += (int)(attackHitboxHeight * tmpRangeMultiplierY);


            ret[1] = new Rectangle();
            ret[1].X = (int)attackHitboxRangeUpperLeft.X;
            ret[1].Y = (int)attackHitboxRangeUpperLeft.Y;
            ret[1].Width = (int)Vector2.Distance(attackHitboxRangeUpperLeft, new Vector2(attackHitboxRangeDownRight.X, attackHitboxRangeUpperLeft.Y));
            ret[1].Height = (int)Vector2.Distance(attackHitboxRangeUpperLeft, new Vector2(attackHitboxRangeUpperLeft.X, attackHitboxRangeDownRight.Y));

            return ret;

        }
        public override void UseWeapon()
        {
            int attackHitboxWidth = Owner.Hitbox.Width;
            int attackHitboxHeight = Owner.Hitbox.Height;

            float tmpRangeMultiplierX = rangeMultiplierX;
            float tmpRangeMultiplierY = rangeMultiplierY;

            // Spezialfall: nach oben oder unten gucken
            if (Owner.GetAttackLineOfSight().Y != 0)
            {
                int swap = attackHitboxWidth;
                attackHitboxWidth = attackHitboxHeight;
                attackHitboxHeight = swap;

                float swap2 = rangeMultiplierX;
                tmpRangeMultiplierX = rangeMultiplierY;
                tmpRangeMultiplierY = swap2;
            }

            // Mittelpunkt der Hitbox ist Maus
            Vector2 attackDirection = new Vector2(Owner.GetAttackDirection().X - attackHitboxWidth / 2 * tmpRangeMultiplierX,
                Owner.GetAttackDirection().Y - attackHitboxHeight / 2 * tmpRangeMultiplierY);

            /*
                
            */

            Vector2 attackHitboxRangeUpperLeft = new Vector2(Owner.Hitbox.X - attackHitboxWidth * tmpRangeMultiplierX +
                   (attackHitboxWidth * (1f - tmpRangeMultiplierX) < 0 ? 0 : (attackHitboxWidth * (1f - tmpRangeMultiplierX) /*dieser Teil für Berücksichtigung der widthReduction*/ )),
               Owner.Hitbox.Y - attackHitboxHeight * tmpRangeMultiplierY +
                (attackHitboxHeight * (1f - tmpRangeMultiplierY) < 0 ? 0 : attackHitboxHeight * (1f - tmpRangeMultiplierY)/*dieser Teil für Berücksichtigung der heightReduction*/ ));


            Vector2 attackHitboxRangeDownRight = new Vector2(Owner.Hitbox.X + Owner.Hitbox.Width -
                   (attackHitboxWidth * (1f - tmpRangeMultiplierX) < 0 ? 0 : (attackHitboxWidth * (1f - tmpRangeMultiplierX) /*dieser Teil für Berücksichtigung der widthReduction*/ )),
                Owner.Hitbox.Y + Owner.Hitbox.Height -
                                  (attackHitboxHeight * (1f - tmpRangeMultiplierY) < 0 ? 0 : attackHitboxHeight * (1f - tmpRangeMultiplierY)/*dieser Teil für Berücksichtigung der heightReduction*/ ));


            #region alterCode
            //Rectangle OwnerHitbox = Owner.Hitbox;
            //int attackHitboxWidth = OwnerHitbox.Width;
            //int attackHitboxHeight = OwnerHitbox.Height;



            //float tmpRangeMultiplierX = rangeMultiplierX;
            //float tmpRangeMultiplierY = rangeMultiplierY;


            //if (Owner.GetAttackLineOfSight().Y != 0)
            //{
            //    int swap = attackHitboxWidth;
            //    attackHitboxWidth = attackHitboxHeight;
            //    attackHitboxHeight = swap;

            //    float swap2 = rangeMultiplierX;
            //    tmpRangeMultiplierX = rangeMultiplierY;
            //    tmpRangeMultiplierY = swap2;
            //}

            //// Mittelpunkt der Hitbox ist Maus
            //Vector2 attackDirection = new Vector2(Owner.GetAttackDirection().X - attackHitboxWidth / 2 * tmpRangeMultiplierX ,
            //    Owner.GetAttackDirection().Y - attackHitboxHeight / 2  * tmpRangeMultiplierY );



            //// Intervallgrenzen für maximale Attack-Range: 
            //// so gewählt, dass die kleinste mögliche Hitbox erlaubt ist:
            //Vector2 attackHitboxRangeUpperLeft = new Vector2(OwnerHitbox.X - OwnerHitbox.Width * tmpRangeMultiplierX ,
            //    OwnerHitbox.Y - OwnerHitbox.Width * tmpRangeMultiplierY);


            //Vector2 attackHitboxRangeDownRight = new Vector2(OwnerHitbox.X + OwnerHitbox.Width - (attackHitboxWidth - OwnerHitbox.Width),
            //    OwnerHitbox.Y + OwnerHitbox.Height - (attackHitboxHeight - OwnerHitbox.Width));
            #endregion


            Vector2 attackCoordinates = Vector2.Clamp(attackDirection, attackHitboxRangeUpperLeft, attackHitboxRangeDownRight);

            Rectangle attackHitbox = new Rectangle((int)attackCoordinates.X, (int)attackCoordinates.Y, (int)(attackHitboxWidth * tmpRangeMultiplierX), (int)(attackHitboxHeight * tmpRangeMultiplierY));

            // Für Debug
            GameDebug.AddToBoxDebugBuffer(attackHitbox, Color.IndianRed, 10);

            if (Owner is Player)
            {
                if (LevelManager.currentmap.currentroom != null)
                {
                    byte enemiesHit = 0; 
                    foreach (var enemy in LevelManager.currentmap.currentroom.enemylist)
                    {
                        if (attackHitbox.Intersects(enemy.Hitbox) && !enemy.IsDead())
                        {
                            ((Enemy)enemy).DeductHealthPoints((int)(weaponDamage * Owner.temporaryDamageMultiplier));
                            if (++enemiesHit == maximumHitsPerAttack)
                                break;
                        }
                    }

                    foreach (var projectile in EntityManager.projectiles)
                    {
                        if (attackHitbox.Intersects(projectile.Hitbox))
                            projectile.isExpired = true;
                    }
                }

            }
            else if (Owner is Enemy)
            {
                if (attackHitbox.Intersects(Player.Instance.Hitbox))
                {
                    Player.Instance.DeductHealthPoints((int)(weaponDamage * Owner.DamageMultiplier));
                }
            }
        }
    }

}
