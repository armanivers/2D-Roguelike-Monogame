using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Loot;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;

namespace _2DRoguelike.Content.Core.Entities
{
    static class EntityManager
    {
        public static List<EntityBasis> creatures = new List<EntityBasis>();

        public static List<LootBase> loots = new List<LootBase>();
        public static List<Projectile> projectiles = new List<Projectile>();

        static bool isUpdating;
        static List<EntityBasis> addedEntities = new List<EntityBasis>();

        public static int Count { get { return creatures.Count; } }

        public static void AddCreatureEntity(EntityBasis entity)
        {
            if (!isUpdating)
                creatures.Add(entity);
            else
                addedEntities.Add(entity);
        }

        public static void AddLootEntity(LootBase loot)
        {
            loots.Add(loot);
        }

        public static void AddProjectileEntity(Projectile projectile)
        {
            projectiles.Add(projectile);
        }

        public static void Update(GameTime gameTime)
        {
            isUpdating = true;

            foreach (var entity in creatures)
                entity.Update(gameTime);

            isUpdating = false;

            foreach (var loot in loots)
                loot.Update(gameTime);

            foreach (var projectile in projectiles)
                projectile.Update(gameTime);

            foreach (var entity in addedEntities)
                creatures.Add(entity);

            addedEntities.Clear();

            // remove any expired entities.
            creatures = creatures.Where(x => !x.isExpired).ToList();
            projectiles = projectiles.Where(x => !x.isExpired).ToList();
            loots = loots.Where(x => !x.isExpired).ToList();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in creatures)
                entity.Draw(spriteBatch);

            foreach (var loot in loots)
                loot.Draw(spriteBatch);

            foreach (var projectile in projectiles)
                projectile.Draw(spriteBatch);
        }

        public static void UnloadEntities()
        {
            addedEntities.Clear();
            creatures.Clear();
            projectiles.Clear();
            loots.Clear();
        }
    }
}
