﻿using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.GameDebugger;
using _2DRoguelike.Content.Core.World.Tiles;
using _2DRoguelike.Content.Core.World.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using _2DRoguelike.Content.Core.World.ExitConditions;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.UI;
using _2DRoguelike.Content.Core.Cutscenes;
using static _2DRoguelike.Content.Core.UI.MessageFactory.Message;
using _2DRoguelike.Content.Core.Entities.Inventories;
using _2DRoguelike.Content.Core.World.Rooms;

namespace _2DRoguelike.Content.Core.World
{
    static class LevelManager
    {
        public static int level = 0;
        // which boss should be spawned, example; bossmap1 = dragon bossm bossmap2 = ...
        public static int bossStage = 0;
        public static List<Level> levelList;
        private static Vector2 playerposition;
        public static Map currentmap;
        public static Tile[,] currenttilemap;

        // max level
        public static int maxLevel = 9;
        // "gameover" but successfullly
        public static bool gameOverSucc;

        public static List<string> levelNames = new List<string>()
        { "The Forbidden Dugeon",
            "Dragon's cave",
            "Last Hope",
            "EndScreen"
        };


        public static object EnityManager { get; private set; }

        public static void LoadContent()
        {
            gameOverSucc = false;
            levelList = new List<Level>();
            levelList.Add(new Level(new Dungeon(),ExitCondition.getRandomExitCondition()));
            currentmap = levelList[level].map;
            currenttilemap = currentmap.tilearray;
            playerposition = new Vector2();
            PrintStageName();
        }

        public static void PrintStageName() {
            MessageFactory.DisplayMessage("Level " + (level / 3 + 1) + " - " + levelNames[level/3], Color.White, AnimationType.LeftToRightCos);
        }

        public static void NextLevel()
        {
            GameDebug.UnloadHitboxBuffer();
            EntityManager.UnloadAllEntities();

            level++;
            if (level >= maxLevel)
            {
                gameOverSucc = true;
            }

            switch (level)
            {
                case 1:
                    levelList.Add(new Level(new Dungeon(), ExitCondition.getRandomExitCondition()));
                    break;
                case 2:
                    levelList.Add(new Level(new BossMap(24, 12), new KillAllEnemies()));
                    break;
                case 3:
                    levelList.Add(new Level(new Dungeon(), ExitCondition.getRandomExitCondition()));
                    break;
                case 4:
                    levelList.Add(new Level(new Dungeon(), ExitCondition.getRandomExitCondition()));
                    break;
                case 5:
                    levelList.Add(new Level(new BossMap(24, 12), new KillAllEnemies()));
                    break;
                case 6:
                    levelList.Add(new Level(new Dungeon(), ExitCondition.getRandomExitCondition()));
                    break;
                case 7:
                    levelList.Add(new Level(new Dungeon(), ExitCondition.getRandomExitCondition()));
                    break;
                case 8:
                    levelList.Add(new Level(new BossMap(24, 12), new KillAllEnemies()));
                    break;

                default:
                    // default case is used for gameover!
                    levelList.Add(new Level(new Dungeon(), ExitCondition.getRandomExitCondition()));
                    break;
            }
            levelList[level - 1].map.clearEnemies();

            Player.Instance.Inventory.ClearKey();

            Player.Instance.Position = levelList[level].map.getSpawnpoint() * new Vector2(32);
            currentmap = levelList[level].map;
            currenttilemap = currentmap.tilearray;

            // start next Level scene
            CutsceneManager.PlayCutsceneDelayed(new FadeOutCircle());

            if (currentmap is BossMap)
            {
                // queue bosstalk scene
                CutsceneManager.QueueScene(new NPCTalk(bossStage + 1,true));
                bossStage++;
                UIManager.SwitchBossBarState();
            }

            MessageFactory.ClearMessages();

            // TODO: Anzeigen des "Levels" anpassen
            if (level % 3 == 0)
                PrintStageName();
        }
        public static void Update(Player player)
        {
            levelList[level].map.Update(player);
            playerposition = player.HitboxCenter;

            if (levelList[level].exitCondition.Exit())
            {
                SoundManager.FulfilledExitCondition.Play(Game1.gameSettings.soundeffectsLevel, 0.0f, 0);
                currentmap.AddKeyToRoom(10);
            }
            CheckEndgame();
        }

        public static void CheckEndgame()
        {
            if (currentmap is BossMap)
            {
                ((BossMap)currentmap).StageCleared();
            }
            else
            {
                // set game over = true and display game over screen
            }
        }

        public static void UnloadContent()
        {
            level = 0;
            bossStage = 0;
            levelList.Clear();
            Room.placedStartLoot = false;
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            DrawWithVisibility(spriteBatch, 1.0f, 12, 9);
        }
        private static void DrawWithVisibility(SpriteBatch spriteBatch, float visibility, int Xvisiblerange, int Yvisiblerange)
        {
            int y = (int)playerposition.Y / 32;
            int x = (int)playerposition.X / 32;

            for (int i = 0; i < Xvisiblerange; i++)
            {
                if (x > 0) x--;
            }
            for (int i = 0; i < Yvisiblerange; i++)
            {
                if (y > 0) y--;
            }

            for (int j = y; j < playerposition.Y / 32 + Yvisiblerange && j < levelList[level].map.height; j++)
            {
                for (int i = x; i < playerposition.X / 32 + Xvisiblerange && i < levelList[level].map.width; i++)
                {
                    spriteBatch.Draw(levelList[level].map.tilearray[i, j].texture, new Rectangle(levelList[level].map.tilearray[i, j].x, levelList[level].map.tilearray[i, j].y, levelList[level].map.tilearray[i, j].width, levelList[level].map.tilearray[i, j].height), Color.White * visibility);
                }
            }
        }
    }
}