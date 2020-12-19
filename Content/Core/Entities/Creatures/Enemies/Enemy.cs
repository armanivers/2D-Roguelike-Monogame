﻿
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.World;
using _2DRoguelike.Content.Core.World.Rooms;

using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    public abstract class Enemy : Humanoid
    {

        public EnemyAI ai;
        // Für ATTACK-Range Debug
        public Rectangle AttackRangeHitbox;
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
            var xp = new System.Random().Next(1, 5);
            Player.Instance.AddExperiencePoints(xp);
        }

        public override Actions.Action DetermineAction()
        {
            return ai.DetermineAction();
        }

        public override Vector2 GetDirection()
        {
            return ai.DeterminePath();
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

        // TODO: Nachschauen, wie das funktioniert da ist noch was faul
        public bool IsPlayerInTheSameRoom()
        {
            //// Test
            //Rectangle outer = new Rectangle(0, 0, 5, 5);
            //Rectangle inner = new Rectangle(1, 1, 2, 2);
            //Debug.WriteLine("Test Intersect: " + outer.Intersects(inner));
            //Debug.WriteLine("Test contains: " + outer.Contains(inner));
            if (LevelManager.maps.currentroom == null)
            {
                Debug.WriteLine("Current Room is null\n------");
                return false;
            }
            return LevelManager.maps.currentroom.enemylist.Contains(this);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }


}
