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
            // TODO: Angemessene AttackTimespan für jede Waffe wählen (Animationsdauer)

            // TODO: Von Hitbox.Center aus gehen, Angriffsrichtung bestimmen, und evtl. Width und Height tauschen


            Rectangle OwnerHitbox = Owner.Hitbox;
            int attackHitboxWidth = OwnerHitbox.Width;
            int attackHitboxHeight = OwnerHitbox.Height;

            if (Owner.GetAttackLineOfSight().Y != 0)
            {
                var swap = attackHitboxWidth;
                attackHitboxWidth = attackHitboxHeight;
                attackHitboxHeight = swap;
            }

            // Mittelpunkt der Hitbox ist Maus
            Vector2 attackDirection = new Vector2(Owner.GetAttackDirection().X - attackHitboxWidth /2 *rangeMultiplierX, 
                Owner.GetAttackDirection().Y - attackHitboxHeight/2 * 0.8f * rangeMultiplierY);

            const float heightReduction = 1f; // 0.8f

            // Intervallgrenzen für maximale Attack-Range: 
            // so gewählt, dass die kleinste mögliche Hitbox erlaubt ist:
            Vector2 attackHitboxRangeUpperLeft = new Vector2(OwnerHitbox.X - OwnerHitbox.Width * rangeMultiplierX,
                OwnerHitbox.Y - OwnerHitbox.Width * heightReduction * rangeMultiplierY);


            Vector2 attackHitboxRangeDownRight = new Vector2(OwnerHitbox.X + OwnerHitbox.Width - (attackHitboxWidth - OwnerHitbox.Width), 
                OwnerHitbox.Y + OwnerHitbox.Height - (attackHitboxHeight - OwnerHitbox.Width)) ;

            Vector2 attackCoordinates = Vector2.Clamp(attackDirection, attackHitboxRangeUpperLeft, attackHitboxRangeDownRight);

            Rectangle attackHitbox = new Rectangle((int)attackCoordinates.X, (int)attackCoordinates.Y, (int)(attackHitboxWidth * rangeMultiplierX), (int)(attackHitboxHeight * heightReduction * rangeMultiplierY));

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
