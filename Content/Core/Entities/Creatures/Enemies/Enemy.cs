
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI;
using _2DRoguelike.Content.Core.Entities.Loot.Potions;
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
        // 30% to drop an loot bag
        private const int dropChance = 30;
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

        public override bool CannotWalkHere()
        {
            return LevelManager.maps.currentroom == null ? true : (!LevelManager.maps.currentroom.roomhitbox.Contains(Hitbox) || base.CannotWalkHere() );
        }

        protected void DropExperiencePoints()
        {
            var xp = new System.Random().Next(1, 5);
            Player.Instance.AddExperiencePoints(xp);
        }

        protected void DropLoot()
        {

            var randomPercentage = Game1.rand.Next(0, 100);
            
            if(randomPercentage <= dropChance)
            {
                new LootBag(new Vector2(Hitbox.X, Hitbox.Y), this);
            }
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
           
            if (LevelManager.maps.currentroom == null)
            {
                // Debug.WriteLine("Current Room is null\n------");
                return false;
            }
            return LevelManager.maps.currentroom.roomhitbox.Intersects(this.Hitbox);

        }

        protected override void Disappear()
        {
            base.Disappear();
            DropExperiencePoints();
            DropLoot();
            StatisticsManager.MonsterKilled();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }


}
