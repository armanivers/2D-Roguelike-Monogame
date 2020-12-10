using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.World;

using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    abstract class Enemy : Humanoid
    {


        public Enemy(Vector2 position, int maxHealthPoints, float attackTimespan, float movingSpeed) : base(position, maxHealthPoints, attackTimespan, movingSpeed)
        {
 
        }

        public override void AddToWeaponInventory(Weapon weapon)
        {
            if (weapon is ShortRange)
                WeaponInventory[0] = weapon;
            else
                WeaponInventory[1] = weapon;
        }

        public void DropExperiencePoints()
        {
            var xp = new Random().Next(1,5);
            Player.Instance.AddExperiencePoints(xp);
        }

        public override Vector2 GetDirection()
        {
            // TODO: KI nach Angaben fragen
            return new Vector2(0, 0);
        }

        public override Vector2 GetAttackDirection()
        {
            // TODO: Position zum Player bestimmen
            return new Vector2(Player.Instance.HitboxCenter.X, Player.Instance.HitboxCenter.Y);
        }

        public override Vector2 GetAttackLineOfSight()
        {
            // TODO: Blickrichtung nach Angriff bestimmen
            var differenz = new Vector2(Player.Instance.HitboxCenter.X, Player.Instance.HitboxCenter.Y)
                - new Vector2(HitboxCenter.X, HitboxCenter.Y);
            var angle = System.Math.Atan2(differenz.X, differenz.Y);
            if (angle > 1 && angle < 2)
            {
                return new Vector2(1, 0);
            }
            else if (angle > 2 && angle < 3)
            {
                return new Vector2(0, -1);
            }
            else if (angle > -3 && angle < -2)
            {

                return new Vector2(0, -1);
            }
            else if (angle > -1 && angle < 1)
            {
                return new Vector2(0, 1);
            }
            else if (angle < -1 && angle > -2)
            {
                return new Vector2(-1, 0);
            }
            return Vector2.Zero;
        }

        public bool CanAttack(int weaponPos)
        {
            // Check, ob bestimmte Waffe gerade genutzt
            return !IsAttacking() && !WeaponInventory[weaponPos].InUsage();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }


}
