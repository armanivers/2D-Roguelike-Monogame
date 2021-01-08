using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public abstract class ShortRange : Weapon
    {
        // Für gleichzeitiges Abziehen von zwei Richtungen (z.B. Schulter und Beine nicht miteinbeziehen)
        private float heightReduction = 0.8f;
        private float widthReduction = 1f;

        private float rangeMultiplierX;
        private float rangeMultiplierY;
        public float RangeMultiplierX { get => rangeMultiplierX; }
        public float RangeMultiplierY { get => rangeMultiplierY; }

        public ShortRange(Humanoid Owner, float rangeX, float rangeY, int weaponDamage, float weaponCooldown) : base(Owner, weaponDamage, weaponCooldown)
        {
            this.rangeMultiplierX = rangeX * widthReduction;
            this.rangeMultiplierY = rangeY * heightReduction;
        }

        public Rectangle GetEffectiveRange()
        {
            Rectangle ret = new Rectangle();
            Vector2 upperLeft = new Vector2(Owner.Hitbox.X - Owner.Hitbox.Width * rangeMultiplierX,
                 Owner.Hitbox.Y - Owner.Hitbox.Width * rangeMultiplierX);


            Vector2 upperRight = new Vector2(Owner.Hitbox.X + Owner.Hitbox.Width + Owner.Hitbox.Width * rangeMultiplierX,
                Owner.Hitbox.Y - Owner.Hitbox.Width * rangeMultiplierX);


            Vector2 downLeft = new Vector2(Owner.Hitbox.X - Owner.Hitbox.Width * rangeMultiplierX,
                    Owner.Hitbox.Y + Owner.Hitbox.Height + Owner.Hitbox.Width * rangeMultiplierX);


            //Vector2 downRight = new Vector2(Owner.Hitbox.X + Owner.Hitbox.Width - (Owner.Hitbox.Height - Owner.Hitbox.Width),
            //    Owner.Hitbox.Y + Owner.Hitbox.Height - (Owner.Hitbox.Height - Owner.Hitbox.Width));

            ret.X = (int)upperLeft.X;
            ret.Y = (int)upperLeft.Y;
            ret.Width = (int)Vector2.Distance(upperLeft, upperRight);
            ret.Height = (int)Vector2.Distance(upperLeft, downLeft);
            return ret;

        }
        public override void UseWeapon()
        {
            // TODO: ÄNDERN!!!


            Rectangle OwnerHitbox = Owner.Hitbox;
            int attackHitboxWidth = OwnerHitbox.Width;
            int attackHitboxHeight = OwnerHitbox.Height;



            float tmpRangeMultiplierX = rangeMultiplierX;
            float tmpRangeMultiplierY = rangeMultiplierY;
            

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
            Vector2 attackDirection = new Vector2(Owner.GetAttackDirection().X - attackHitboxWidth / 2 * tmpRangeMultiplierX ,
                Owner.GetAttackDirection().Y - attackHitboxHeight / 2  * tmpRangeMultiplierY );

            // TODO: Dieses Attribut nicht mehr nur für Höhe, sondern je anch Lage auf die Breite rechnen


            // Intervallgrenzen für maximale Attack-Range: 
            // so gewählt, dass die kleinste mögliche Hitbox erlaubt ist:
            Vector2 attackHitboxRangeUpperLeft = new Vector2(OwnerHitbox.X - OwnerHitbox.Width * tmpRangeMultiplierX ,
                OwnerHitbox.Y - OwnerHitbox.Width * tmpRangeMultiplierY);


            Vector2 attackHitboxRangeDownRight = new Vector2(OwnerHitbox.X + OwnerHitbox.Width - (attackHitboxWidth - OwnerHitbox.Width),
                OwnerHitbox.Y + OwnerHitbox.Height - (attackHitboxHeight - OwnerHitbox.Width));

            Vector2 attackCoordinates = Vector2.Clamp(attackDirection, attackHitboxRangeUpperLeft, attackHitboxRangeDownRight);

            Rectangle attackHitbox = new Rectangle((int)attackCoordinates.X, (int)attackCoordinates.Y, (int)(attackHitboxWidth  * tmpRangeMultiplierX), (int)(attackHitboxHeight  * tmpRangeMultiplierY));

            // Für Debug
            GameDebug.GameDebug.AddToBoxDebugBuffer(attackHitbox, Color.White, 10);

            if (Owner is ControllingPlayer.Player)
            {
                // TODO: ERSETZEN Durch EnemyList des Raumes
                if (LevelManager.currentmap.currentroom != null)
                    foreach (var enemy in LevelManager.currentmap.currentroom.enemylist)
                    {

                        if (attackHitbox.Intersects(enemy.Hitbox))
                        {
                            ((Enemy)enemy).DeductHealthPoints(weaponDamage);
                        }

                    }
            }
            else if (Owner is Enemy)
            {
                if (attackHitbox.Intersects(ControllingPlayer.Player.Instance.Hitbox))
                {
                    ControllingPlayer.Player.Instance.DeductHealthPoints(weaponDamage);
                }
            }
        }
    }

}
