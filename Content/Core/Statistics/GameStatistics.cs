using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace _2DRoguelike.Content.Core.Statistics
{
    public class GameStatistics
    {
        private string fileName = "statistics.xml";

        // xmlserializer only serializes public attributes!

        [XmlArray("Scores")]
        [XmlArrayItem("ScoreItem")]
        public List<int> scores;
        public const int maxScores = 5;

        public int itemsRecieved;
        public int monstersKilled;
        public int timesLeveledUp;
        public int lootsOpened;
        public int levelsReached;

        public GameStatistics()
        {
            itemsRecieved = 0;
            monstersKilled = 0;
            timesLeveledUp = 0;
            lootsOpened = 0;
            levelsReached = 0;
            scores = new List<int>();
        }

        public void NewItem()
        {
            itemsRecieved++;
        }

        public void MonsterKilled()
        {
            monstersKilled++;
        }

        public void LeveledUp()
        {
            timesLeveledUp++;
        }

        public void LootOpened()
        {
            lootsOpened++;
        }

        public void LevelReached()
        {
            levelsReached++;
        }

        public void AddHighscore(PlayerScore score)
        {
            scores.Add(score.Score);
            scores.Sort((score1, score2) => score2.CompareTo(score1));

            if (scores.Count > maxScores)
            {
                scores.RemoveAt(maxScores);
            }
        }

        public void SaveStatistics()
        {
            using (TextWriter writer = new StreamWriter(Game1.projectPath + fileName))
            {
                XmlSerializer xmlser = new XmlSerializer(this.GetType());
                xmlser.Serialize(writer, this);
                //Debug.Print("monstersKilled {0}", monstersKilled);
            }
        }

        public void ApplyStatistics(GameStatistics stats)
        {
            itemsRecieved = stats.itemsRecieved;
            monstersKilled = stats.monstersKilled;
            timesLeveledUp = stats.timesLeveledUp;
            lootsOpened = stats.lootsOpened;
            levelsReached = stats.levelsReached;
            scores = stats.scores;
        }

        public void LoadStatistics()
        {
            if (!settingsFileExists())
            {
                SaveStatistics();
                //Debug.Print("no scores found, default loaded");
                return;
            }
            GameStatistics instance;

            using (TextReader reader = new StreamReader(Game1.projectPath + fileName))
            {
                XmlSerializer xmlser = new XmlSerializer(this.GetType());
                instance = (GameStatistics)xmlser.Deserialize(reader);
                //Debug.Print("settings found, load them");
            }
            ApplyStatistics(instance);
        }

        public bool settingsFileExists()
        {
            return File.Exists(Game1.projectPath + fileName);
        }

        public void DisplayScore()
        {
            Debug.Print("Items recieved: {0}, Monsters killed: {1}, Times Leveled up: {2}, Loots opened {3}, levels reached {4}", itemsRecieved, monstersKilled, timesLeveledUp, lootsOpened, levelsReached);
            foreach(var s in scores)
            {
                Debug.Print("Score: "+s);
            }
        }
    }
}
