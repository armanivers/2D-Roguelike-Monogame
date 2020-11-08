using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    static class EntityManager
    {
        public static List<EntityBasis> entities = new List<EntityBasis>();

        static bool isUpdating;
        static List<EntityBasis> addedEntities = new List<EntityBasis>();

        public static int Count { get { return entities.Count; } }

        public static void Add(EntityBasis entity)
        {
            if (!isUpdating)
                entities.Add(entity);
            else
                addedEntities.Add(entity);
        }

        public static void Update(GameTime gameTime)
        {
            isUpdating = true;

            foreach (var entity in entities)
                entity.Update(gameTime);

            isUpdating = false;

            foreach (var entity in addedEntities)
                entities.Add(entity);

            addedEntities.Clear();

            // remove any expired entities.
            entities = entities.Where(x => !x.isExpired).ToList();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
                entity.Draw(spriteBatch);
        }
    }
}
