
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Inventories;
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
        public EnemyInventory Inventory
        {
            get
            {
                return (EnemyInventory)inventory;
            }
        }

        public EnemyAI ai;

        // 30% to drop an loot bag
        private const int dropChance = 40;
        public Enemy(Vector2 position, int maxHealthPoints, float attackTimespan, float movingSpeed, float scaleFactor = 1f) : base(position, maxHealthPoints, attackTimespan, movingSpeed, scaleFactor)
        {
            inventory = new EnemyInventory(this);
        }

        public override bool CannotWalkHere()
        {
            if (LevelManager.currentmap.currentroom == null || !LevelManager.currentmap.currentroom.roomhitbox.Contains(Hitbox) || base.CannotWalkHere())
                return true;
            foreach (Enemy otherEnemy in LevelManager.currentmap.currentroom.enemylist)
            {
                if (otherEnemy != this && !otherEnemy.IsDead() && GetTileCollisionHitbox().Intersects(otherEnemy.GetTileCollisionHitbox()))
                {
                    // Debug.WriteLine("Enemy at Position:{0} is Stuck with Enemy at Position:{1}!", Position/32, otherEnemy.Position/32);
                    return true;
                }
            }
            return false;
        }

        protected void DropExperiencePoints()
        {
            var xp = new System.Random().Next(1, 5);
            Player.Instance.AddExperiencePoints(xp);
        }

        protected void DropLoot()
        {

            var randomPercentage = Game1.rand.Next(0, 100);

            if (randomPercentage <= dropChance)
            {
                new LootBag(new Vector2(Hitbox.X, Hitbox.Y), this);
            }
        }

        public override Actions.Action DetermineAction(float elapsedTime)
        {
            return ai.DetermineAction();
        }

        public override Vector2 GetDirection()
        {
            return ai.DeterminePath();
        }

        public override bool IsUsingProtectAbility()
        {

            return ai.IsUsingProtectAbility();
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
            return !IsAttacking() && !inventory.WeaponInventory[weaponPos].InUsage();
        }

        public bool IsPlayerInTheSameRoom()
        {

            if (LevelManager.currentmap.currentroom == null)
            {
                return false;
            }
            return LevelManager.currentmap.currentroom.roomhitbox.Intersects(this.Hitbox);

        }

        protected override void Disappear()
        {
            base.Disappear();
            DropExperiencePoints();
            DropLoot();
            StatisticsManager.MonsterKilled();
        }

        public override bool DeductHealthPoints(int damage)
        {
            bool ret = base.DeductHealthPoints(damage);
            if (ret)
            {
                ai.ReactionTimer += ai.currentReactionTimeGap / 5;
                if (ai.ReactionTimer > ai.currentReactionTimeGap)
                    ai.ReactionTimer = ai.currentReactionTimeGap;
                // Debug.WriteLine("ReactionTime successfully cut to " + ai.ReactionTimer);
            }
            return ret;
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.IsDead() && LevelManager.currentmap.currentroom != null)
                // Wenn Endlossschleifen verursacht: Auskommentieren
                for (int i = 0; i < EntityManager.creatures.Count; i++)
                {
                    if (EntityManager.creatures[i] is Creature && EntityManager.creatures[i] != this)

                        if (!((Creature)EntityManager.creatures[i]).IsDead() && GetTileCollisionHitbox().Intersects(((Creature)EntityManager.creatures[i]).GetTileCollisionHitbox()))
                        {
                            this.isExpired = true;
                            break;
                        }
                }

            base.Update(gameTime);
        }

    }


}
