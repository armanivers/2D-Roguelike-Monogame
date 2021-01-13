using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Loot;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Special_Interactables;
using System.Diagnostics;

namespace _2DRoguelike.Content.Core.Entities
{
    static class EntityManager
    {

        public static bool isUpdatingInteractables;
        public static List<InteractableBase> addedInteractables = new List<InteractableBase>();
        public static List<InteractableBase> interactables = new List<InteractableBase>();

        public static bool isUpdatingProjectile;
        public static List<Projectile> addedProjectile = new List<Projectile>();
        public static List<Projectile> projectiles = new List<Projectile>();

        public static bool isUpdatingCreature;
        public static List<EntityBasis> creatures = new List<EntityBasis>();
        public static List<EntityBasis> addedEntities = new List<EntityBasis>();

        public static bool isUpdatingSpecialInteractables;
        public static List<EntityBasis> specialInteractables = new List<EntityBasis>();
        public static List<EntityBasis> addedspecialInteractables = new List<EntityBasis>();

        public static Player player;

        public static bool clearLevel = false;
        public static int CreaturesCount { get { return creatures.Count; } }
        public static int ProjectilesCount { get { return projectiles.Count; } } 
        public static int LootsCount { get { return interactables.Count; } }


        public static void AddCreatureEntity(EntityBasis entity)
        {
            if (!isUpdatingCreature)
                creatures.Add(entity);
            else
                addedEntities.Add(entity);
        }

        public static void AddInteractableEntity(InteractableBase loot)
        {
            if (!isUpdatingInteractables)
                interactables.Add(loot);
            else
                addedInteractables.Add(loot);
        }

        public static void AddProjectileEntity(Projectile projectile)
        {
            if (!isUpdatingProjectile)
                projectiles.Add(projectile);
            else
                addedProjectile.Add(projectile);
        }

        public static void AddSpecialInteractable(SpecialInteractableBase specialInter)
        {
            if (!isUpdatingSpecialInteractables)
                specialInteractables.Add(specialInter);
            else
                addedspecialInteractables.Add(specialInter);
        }

        public static void Update(GameTime gameTime)
        {
            if(clearLevel)
            {
                addedEntities.Clear();
                creatures.Clear();
                projectiles.Clear();
                addedProjectile.Clear();
                interactables.Clear();
                addedInteractables.Clear();
                specialInteractables.Clear();
                addedspecialInteractables.Clear();

                clearLevel = false;
            }

            player.Update(gameTime);

            isUpdatingCreature = true;
            foreach (var entity in creatures)
                entity.Update(gameTime);
            isUpdatingCreature = false;


            isUpdatingInteractables = true;
            foreach (var loot in interactables)
                loot.Update(gameTime);
            isUpdatingInteractables = false;

            isUpdatingProjectile = true;
            foreach (var projectile in projectiles)
                projectile.Update(gameTime);
            isUpdatingProjectile = false;

            isUpdatingSpecialInteractables = true;
            foreach (var specialInter in specialInteractables)
                specialInter.Update(gameTime);
            isUpdatingSpecialInteractables = false;

            foreach (var entity in addedEntities)
                creatures.Add(entity);
            foreach (var loot in addedInteractables)
                interactables.Add(loot);
            foreach (var projectile in addedProjectile)
                projectiles.Add(projectile);
            foreach (var specialInter in addedspecialInteractables)
                specialInteractables.Add(specialInter);

            addedEntities.Clear();
            addedInteractables.Clear();
            addedProjectile.Clear();
            addedspecialInteractables.Clear();

            // remove any expired entities.
            creatures = creatures.Where(x => !x.isExpired).ToList();
            projectiles = projectiles.Where(x => !x.isExpired).ToList();
            interactables = interactables.Where(x => !x.isExpired).ToList();
            specialInteractables = specialInteractables.Where(x => !x.isExpired).ToList();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var loot in interactables)
                loot.Draw(spriteBatch);

            foreach (var specialInter in specialInteractables)
                specialInter.Draw(spriteBatch);

            foreach (var entity in creatures)
                entity.Draw(spriteBatch);

            player.Draw(spriteBatch);

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
            addedInteractables.Clear();
            addedProjectile.Clear();
            addedspecialInteractables.Clear();

            creatures.Clear();
            projectiles.Clear();
            interactables.Clear();
            specialInteractables.Clear();
        }
    }
}
