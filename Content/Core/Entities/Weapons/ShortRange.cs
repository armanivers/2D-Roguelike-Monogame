using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public abstract class ShortRange : Weapon
    {
        float rangeMultiplierX;
        float rangeMultiplierY;
        public ShortRange(Humanoid Owner, float rangeX, float rangeY, int weaponDamage, float weaponCooldown) : base(Owner, weaponDamage, weaponCooldown) {
            this.rangeMultiplierX = rangeX;
            this.rangeMultiplierY = rangeY;
        }

        public override void UseWeapon()
        {
            // TODO: Angemessene AttackTimespan für jede Waffe wählen (Animationsdauer


            Rectangle OwnerHitbox = Owner.Hitbox;
            int attackHitboxWidth = OwnerHitbox.Width;
            int attackHitboxHeight = OwnerHitbox.Height;
            float heightReduction = 0.5f;
            float widthReduction= 1f;

            if (Owner.GetAttackLineOfSight().Y != 0)
            {
                int swap = attackHitboxWidth;
                attackHitboxWidth = attackHitboxHeight;
                attackHitboxHeight = swap;

                float swap2 = widthReduction;
                widthReduction = heightReduction;
                heightReduction = swap2;
            }

            // Mittelpunkt der Hitbox ist Maus
            Vector2 attackDirection = new Vector2(Owner.GetAttackDirection().X - attackHitboxWidth /2 *rangeMultiplierX * widthReduction, 
                Owner.GetAttackDirection().Y - attackHitboxHeight/2 * 0.8f * rangeMultiplierY * heightReduction);

            // TODO: Dieses Attribut nicht mehr nur für Höhe, sondern je anch Lage auf die Breite rechnen
            

            // Intervallgrenzen für maximale Attack-Range: 
            // so gewählt, dass die kleinste mögliche Hitbox erlaubt ist:
            Vector2 attackHitboxRangeUpperLeft = new Vector2(OwnerHitbox.X - OwnerHitbox.Width * rangeMultiplierX * widthReduction,
                OwnerHitbox.Y - OwnerHitbox.Width * rangeMultiplierY * heightReduction * rangeMultiplierY);


            Vector2 attackHitboxRangeDownRight = new Vector2(OwnerHitbox.X + OwnerHitbox.Width - (attackHitboxWidth - OwnerHitbox.Width), 
                OwnerHitbox.Y + OwnerHitbox.Height - (attackHitboxHeight - OwnerHitbox.Width)) ;

            Vector2 attackCoordinates = Vector2.Clamp(attackDirection, attackHitboxRangeUpperLeft, attackHitboxRangeDownRight);

            Rectangle attackHitbox = new Rectangle((int)attackCoordinates.X, (int)attackCoordinates.Y, (int)(attackHitboxWidth * widthReduction * rangeMultiplierX), (int)(attackHitboxHeight * heightReduction * rangeMultiplierY));

            // Für Debug
            if (GameDebug.HitboxDebug.DEBUG)
                Owner.AttackHitbox = attackHitbox;

            if (Owner is ControllingPlayer.Player)
            {
                // TODO: ERSETZEN Durch EnemyList des Raumes

                foreach (var enemy in EntityManager.creatures)
                {
                    if (enemy is Enemy)
                    {
                        if (attackHitbox.Intersects(enemy.Hitbox))
                        {
                            ((Enemy)enemy).DeductHealthPoints(weaponDamage);
                        }
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
