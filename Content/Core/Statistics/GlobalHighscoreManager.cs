using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace _2DRoguelike.Content.Core.Statistics
{
    public class GlobalHighscoreManager
    {
        public static string serverURL = "http://latenitearii.ddns.net/2dmonogame/highscore";
        public static List<KeyValuePair<string,int>> FetchGlobalHighscoreData()
        {
            using (WebClient wc = new WebClient())
            {
                var xml = wc.DownloadString(serverURL);
                return ConvertJson(xml);
            }
        }

        public static List<KeyValuePair<string, int>> ConvertJson(string xml)
        {
            List<KeyValuePair<string, int>> xmlDeserialized = new List<KeyValuePair<string, int>>();
            XmlSerializer serializer = new XmlSerializer(typeof(Score));
            Score result;
            using (TextReader reader = new StringReader(xml))
            {
                try
                {
                    result = (Score)serializer.Deserialize(reader);
                    foreach (var r in result.Rank)
                    {
                        xmlDeserialized.Add(new KeyValuePair<string, int>(r.Name, r.Score));
                    }
                }
                catch (WebException ex)
                {
                    Debug.Print(ex.Message);
                    xmlDeserialized.Add(new KeyValuePair<string, int>("Error retrieving data", 404));
                }
                catch (System.InvalidOperationException ex)
                {
                    Debug.Print("XML Response couldnt' be serialized, maybe no response from server?");
                    xmlDeserialized.Add(new KeyValuePair<string, int>("Error retrieving highscore data from server! ",404));
                } 
            }

            return xmlDeserialized;
        }

        public static void SendHighscoreToServer(string name,int score)
        {
            // send data to url
            // form should be "Name"=gamesettings.playername and "score"=highscore.score;
            Debug.Print("Data sent: name = {0} and score {1}", name, score);
        }
    }
}
