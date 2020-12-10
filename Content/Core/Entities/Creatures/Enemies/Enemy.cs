﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.World;

using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    public abstract class Enemy : Humanoid
    {

        public EnemyAI ai;
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
            //return ai.DeterminePath();
            return Vector2.Zero;
        }

        public override Vector2 GetAttackDirection()
        {
            return new Vector2(Player.Instance.HitboxCenter.X, Player.Instance.HitboxCenter.Y);
        }

        public override Vector2 GetAttackLineOfSight()
        {
            var differenz = new Vector2(Player.Instance.HitboxCenter.X, Player.Instance.HitboxCenter.Y)
                - new Vector2(HitboxCenter.X, HitboxCenter.Y);
            var angle = System.Math.Atan2(differenz.Y, differenz.X);
            return CalculateDirection(angle);
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
