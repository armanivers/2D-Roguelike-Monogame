using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class Melee : Attack
    {
        private const int DAMAGE = 50;

        public Melee(Humanoid callInst) : base(callInst,new MeleeAnimationIdentifier("SlashRight", "SlashLeft", "SlashDown", "SlashUp"))
        {
        }

        public override void CommenceAttack()
        {
            // TODO: Melee-Weapon als eigenes Objekt erstellen, wie mit Arrow → dieser kümmert sich um Hits und expired nach einer gewissen Zeit

            Rectangle callingInstanceHitbox = CallingInstance.Hitbox;
            // TODO: Ausführen der Logik für den Angriff
            Vector2 attackDirection = CallingInstance.GetAttackDirection();

            Vector2 attackHitboxRangeUpperLeft = new Vector2(callingInstanceHitbox.X - callingInstanceHitbox.Width, callingInstanceHitbox.Y - callingInstanceHitbox.Height);
            Vector2 attackHitboxRangeDownRight = new Vector2(callingInstanceHitbox.X + callingInstanceHitbox.Width, callingInstanceHitbox.Y + callingInstanceHitbox.Height);

            Vector2 attackCoordinates = Vector2.Clamp(attackDirection, attackHitboxRangeUpperLeft, attackHitboxRangeDownRight);

            Rectangle attackHitbox = new Rectangle((int)attackCoordinates.X, (int)attackCoordinates.Y, callingInstanceHitbox.Width, callingInstanceHitbox.Height);

            if (CallingInstance is Player.Player)
            {
                // TODO: ERSETZEN Durch EnemyList des Raumes

                foreach (var enemy in EntityManager.entities)
                {
                    if (enemy is Enemy)
                    {
                        if (attackHitbox.Intersects(enemy.Hitbox))
                        {
                            ((Enemy)enemy).DeductHealthPoints(DAMAGE);
                        }
                    }
                }
            }
            else if (CallingInstance is Enemy)
            {
                if (attackHitbox.Intersects(Player.Player.Instance.Hitbox))
                {
                    Player.Player.Instance.DeductHealthPoints(DAMAGE);
                }
            }
        }
    }
}
