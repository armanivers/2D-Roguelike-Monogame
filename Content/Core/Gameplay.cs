﻿using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.Potions;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using _2DRoguelike.Content.Core.UI;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core
{
    class Gameplay
    {
        public bool gameOver;
        public Gameplay()
        {
        }

        public void LoadContent()
        {
            StatisticsManager.InitializeScore();
            LevelManager.LoadContent();
            // EntityBasis Konstruktor f�gt automatisch zur EntityManager.entities hinzu
            new GreenZombie(/*WorldGenerator.spawn*/LevelManager.maps.getSpawnpoint() + new Vector2(5 * 32, 3 * 32), 100, 3);
            new BrownZombie(/*WorldGenerator.spawn*/LevelManager.maps.getSpawnpoint() + new Vector2(5 * 32, 5 * 32), 50, 3);
            new Skeleton(/*WorldGenerator.spawn*/LevelManager.maps.getSpawnpoint() + new Vector2(5 * 32, 7 * 32), 100, 3);
            new Wizard(/*WorldGenerator.spawn*/LevelManager.maps.getSpawnpoint() + new Vector2(5 * 32, 9 * 32), 100, 3);
            new Player(LevelManager.maps.getSpawnpoint() * new Vector2(32), 100, 5);
            
            new HealthPotion(LevelManager.maps.getSpawnpoint() + new Vector2(5 * 32, 13 * 32));
            new ExperiencePotion(LevelManager.maps.getSpawnpoint() + new Vector2(5 * 32, 11 * 32));

            new AxeLoot(LevelManager.maps.getSpawnpoint() + new Vector2(6 * 32, 15 * 32));
            new BowLoot(LevelManager.maps.getSpawnpoint() + new Vector2(8 * 32, 15 * 32));
            new DaggerLoot(LevelManager.maps.getSpawnpoint() + new Vector2(10 * 32, 15 * 32));

            new LootBag(/*LevelManager.maps.getSpawnpoint() +*/ new Vector2(6 * 32, 21 * 32),null);
            new Chest(new Vector2(2 * 32, 2 * 32), null);

            UIManager.healthBar = new HealthBar(Player.Instance);
            UIManager.skillBar = new Skillbar(Player.Instance);
            UIManager.mobHealthBars = new MobHealthBars();
            UIManager.experienceBar = new ExperienceBar(Player.Instance);
            UIManager.toolTip = new ToolTip(Player.Instance);
            UIManager.highscore = new Highscore();
            UIManager.fog = new Fog();

            gameOver = false;
        }
        public void UnloadContent()
        {
            // Unload all entities + delete current Player Intance
            EntityManager.UnloadEntities();
            Player.Instance.DeleteInstance();
            Camera.Unload();
            LevelManager.UnloadContent();
            StatisticsManager.ClearScore();
        }

        public void Update(GameTime gameTime)
        {
            // Game 
            Camera.Update(Player.Instance);
            InputController.Update();
            EntityManager.Update(gameTime);
            UIManager.Update(gameTime);
            LevelManager.Update(Player.Instance);

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
                GameDebug.GameDebug.DrawDynamic(spriteBatch);
            }

            spriteBatch.End();

            // seperate sprite batch begin+end which isn't attached to an active 2D Camera
            spriteBatch.Begin();

            
            // UI Elements
            UIManager.DrawStatic(spriteBatch);

            if (Game1.gameSettings.DEBUG)
            {
                GameDebug.GameDebug.DrawStatic(spriteBatch);
            }
            spriteBatch.End();

        }
    }

}
