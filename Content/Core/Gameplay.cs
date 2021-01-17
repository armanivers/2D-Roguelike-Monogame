using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Interactables.WorldObjects;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.Potions;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using _2DRoguelike.Content.Core.EntityEffects;
using _2DRoguelike.Content.Core.GameDebugger;
using _2DRoguelike.Content.Core.UI;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace _2DRoguelike.Content.Core
{
    class Gameplay
    {
        public bool gameOver;
        private static GameTime gameTime;

        public static GameTime GameTime { get => gameTime; private set => gameTime = value; }

        public Gameplay()
        {
        }

        public void LoadContent()
        {
            MediaPlayer.Play(SoundManager.Level00);
            MediaPlayer.Volume = Game1.gameSettings.backgroundMusicLevel;
            MediaPlayer.IsRepeating = true;

            StatisticsManager.InitializeScore();
            LevelManager.LoadContent();

            // EntityBasis Konstruktor fuegt automatisch zur EntityManager.entities hinzu

            new Player(LevelManager.currentmap.getSpawnpoint() * new Vector2(32), 100, 5);

            /*
            new GreenZombie(LevelManager.currentmap.getSpawnpoint() + new Vector2(5 * 32, 3 * 32), 100, 3);
            BrownZombie zombie = new BrownZombie(LevelManager.currentmap.getSpawnpoint() + new Vector2(5 * 32, 5 * 32), 50, 3);
            new Skeleton(LevelManager.currentmap.getSpawnpoint() + new Vector2(5 * 32, 7 * 32), 100, 3);
            new Wizard(LevelManager.currentmap.getSpawnpoint() + new Vector2(5 * 32, 9 * 32), 100, 3);
            
            new HealthPotion(LevelManager.currentmap.getSpawnpoint() + new Vector2(5 * 32, 13 * 32));
            new ExperiencePotion(LevelManager.currentmap.getSpawnpoint() + new Vector2(5 * 32, 11 * 32));

            new AxeLoot(LevelManager.currentmap.getSpawnpoint() + new Vector2(6 * 32, 15 * 32));
            new BowLoot(LevelManager.currentmap.getSpawnpoint() + new Vector2(8 * 32, 15 * 32));
            new DaggerLoot(LevelManager.currentmap.getSpawnpoint() + new Vector2(10 * 32, 15 * 32));
            new BombLoot(LevelManager.currentmap.getSpawnpoint() + new Vector2(12 * 32, 15 * 32));

            new LootBag(LevelManager.currentmap.getSpawnpoint()  + new Vector2(6 * 32, 21 * 32), zombie);
            new Chest(new Vector2(2 * 32, 2 * 32));
            new Ladder(new Vector2(2 * 32, 6 * 32));
            */

            UIManager.Load();

            gameOver = false;
        }
        public void UnloadContent()
        {
            // Unload all entities + delete current Player Intance
            EntityManager.UnloadAllEntities();
            Player.Instance.DeleteInstance();
            Camera.Unload();
            LevelManager.UnloadContent();
            StatisticsManager.ClearScore();
            UIManager.Unload();
            //MediaPlayer.Stop();
            CutsceneManager.ClearCutscene();
            GameDebug.UnloadHitboxBuffer();
            EntityEffectsManager.Unload();
        }

        public void Update(GameTime gameTime)
        {
            GameTime = gameTime;

            CutsceneManager.Update(gameTime);
            if (CutsceneManager.activeCutscene)
            {
                return;
            }

            // Game 
            Camera.Update(Player.Instance);
            InputController.Update();
            EntityManager.Update(gameTime);
            UIManager.Update(gameTime);
            LevelManager.Update(Player.Instance);
            EntityEffectsManager.Update(gameTime);

            if (Player.Instance.GameOver())
            {
                gameOver = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.transform);

            LevelManager.Draw(spriteBatch);
            EntityManager.Draw(spriteBatch);
            UIManager.DrawDynamic(spriteBatch);

            if (Game1.gameSettings.DEBUG)
            {
                GameDebug.DrawDynamic(spriteBatch);
            }

            spriteBatch.End();

            // seperate sprite batch begin+end which isn't attached to an active 2D Camera
            spriteBatch.Begin();

            // UI Elements
            UIManager.DrawStatic(spriteBatch);

            // draw current cutscene, if one is currently active
            if (CutsceneManager.activeCutscene) CutsceneManager.Draw(spriteBatch);

            if (Game1.gameSettings.DEBUG)
            {
                GameDebug.DrawStatic(spriteBatch);
            }
            spriteBatch.End();

        }
    }

}
