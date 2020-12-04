using System;
using System.Collections.Generic;
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

        public override Vector2 GetDirection()
        {
            // TODO: KI nach Angaben fragen
            return new Vector2(0, 0);
        }

        public override Vector2 GetAttackDirection()
        {
            // TODO: Position zum Player bestimmen
            return new Vector2(Player.Instance.Hitbox.X + Player.Instance.Hitbox.Width / 2, Player.Instance.Hitbox.Y +Player.Instance.Hitbox.Height / 2);
        }

        public override Vector2 GetAttackLineOfSight()
        {
            // TODO: Blickrichtung nach Angriff bestimmen
            var differenz = new Vector2(ControllingPlayer.Player.Instance.Hitbox.X + ControllingPlayer.Player.Instance.Hitbox.Width / 2, ControllingPlayer.Player.Instance.Hitbox.Y + ControllingPlayer.Player.Instance.Hitbox.Height / 2)
                - new Vector2(Hitbox.X + Hitbox.Width / 2, Hitbox.Y + Hitbox.Height / 2);
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
