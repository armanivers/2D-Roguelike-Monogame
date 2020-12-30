using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Loot;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;

namespace _2DRoguelike.Content.Core.Entities
{
    static class EntityManager
    {

        public static bool isUpdatingLoot;
        public static List<LootBase> addedLoot = new List<LootBase>();
        public static List<LootBase> loots = new List<LootBase>();

        public static bool isUpdatingProjectile;
        public static List<Projectile> addedProjectile = new List<Projectile>();
        public static List<Projectile> projectiles = new List<Projectile>();

        public static bool isUpdatingCreature;
        public static List<EntityBasis> creatures = new List<EntityBasis>();
        public static List<EntityBasis> addedEntities = new List<EntityBasis>();

        public static Player player;

        public static bool clearLevel = false;
        public static int CreaturesCount { get { return creatures.Count; } }
        public static int ProjectilesCount { get { return projectiles.Count; } } 
        public static int LootsCount { get { return loots.Count; } }


        public static void AddCreatureEntity(EntityBasis entity)
        {
            if (!isUpdatingCreature)
                creatures.Add(entity);
            else
                addedEntities.Add(entity);
        }

        public static void AddLootEntity(LootBase loot)
        {
            if (!isUpdatingLoot)
                loots.Add(loot);
            else
                addedLoot.Add(loot);
        }

        public static void AddProjectileEntity(Projectile projectile)
        {
            if (!isUpdatingProjectile)
                projectiles.Add(projectile);
            else
                addedProjectile.Add(projectile);
        }

        public static void Update(GameTime gameTime)
        {
            if(clearLevel)
            {
                addedEntities.Clear();
                creatures.Clear();
                projectiles.Clear();
                addedProjectile.Clear();
                loots.Clear();
                addedLoot.Clear();

                clearLevel = false;
            }

            player.Update(gameTime);

            isUpdatingCreature = true;
            foreach (var entity in creatures)
                entity.Update(gameTime);
            isUpdatingCreature = false;


            isUpdatingLoot = true;
            foreach (var loot in loots)
                loot.Update(gameTime);
            isUpdatingLoot = false;

            isUpdatingProjectile = true;
            foreach (var projectile in projectiles)
                projectile.Update(gameTime);
            isUpdatingProjectile = false;

            foreach (var entity in addedEntities)
                creatures.Add(entity);
            foreach (var loot in addedLoot)
                loots.Add(loot);
            foreach (var projectile in addedProjectile)
                projectiles.Add(projectile);

            addedEntities.Clear();
            addedLoot.Clear();
            addedProjectile.Clear();

            // remove any expired entities.
            creatures = creatures.Where(x => !x.isExpired).ToList();
            projectiles = projectiles.Where(x => !x.isExpired).ToList();
            loots = loots.Where(x => !x.isExpired).ToList();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);

            foreach (var loot in loots)
                loot.Draw(spriteBatch);

            foreach (var entity in creatures)
                entity.Draw(spriteBatch);

            foreach (var projectile in projectiles)
                projectile.Draw(spriteBatch);
        }

        public static void ClearLevelEntities()
        {
            clearLevel = true;
        }

        public static void UnloadAllEntities()
        {
            addedEntities.Clear();
            addedLoot.Clear();
            addedProjectile.Clear();

            creatures.Clear();
            projectiles.Clear();
            loots.Clear();
        }
    }
}
