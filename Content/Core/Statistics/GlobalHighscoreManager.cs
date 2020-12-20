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
        public static List<KeyValuePair<string, int>> FetchGlobalHighscoreData()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    var xml = wc.DownloadString(serverURL);
                    //Debug.Print(xml);
                    return ParseXMLData(xml);
                }
            }
            catch (WebException ex)
            {
                Debug.Print("Error fetching highscore data from seever! " + ex.Message);
                List<KeyValuePair<string, int>> err = new List<KeyValuePair<string, int>>();
                err.Add(new KeyValuePair<string, int>("Error retrieving highscore data from server! ", 404));
                return err;
            }

        }

        public static List<KeyValuePair<string, int>> ParseXMLData(string xml)
        {
            List<KeyValuePair<string, int>> xmlDeserialized = new List<KeyValuePair<string, int>>();
            XmlSerializer serializer = new XmlSerializer(typeof(Score));
            Score result;

            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    result = (Score)serializer.Deserialize(reader);
                    foreach (var r in result.Rank)
                    {
                        xmlDeserialized.Add(new KeyValuePair<string, int>(r.Name, r.Score));
                    }
                }
            }
            catch (WebException ex)
            {
                Debug.Print(ex.Message);
                xmlDeserialized.Add(new KeyValuePair<string, int>("Error retrieving data", 404));
            }
            catch (System.InvalidOperationException ex)
            {
                Debug.Print("XML Response couldnt' be serialized, maybe no response from server or damaged XML File? " + ex.Message);
                xmlDeserialized.Add(new KeyValuePair<string, int>("Error retrieving highscore data from server! ", 404));
            }


            return xmlDeserialized;
        }

        public static void SendHighscoreToServer(string name, int score)
        {
            // send data to url
            // form should be "Name"=gamesettings.playername and "score"=highscore.score;
            //Debug.Print("Data sent: name = {0} and score {1}", name, score);
            string data = "name=" + name + "&score=" + score;

            WebRequest request = WebRequest.Create(serverURL);
            request.Method = "POST";

            Stream dataStream;

            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();

            // Get the original response.
            //WebResponse response = request.GetResponse();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    var status = ((HttpWebResponse)response).StatusDescription;

                    // Get the stream containing all content returned by the requested server.
                    dataStream = response.GetResponseStream();

                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);

                    // Read the content fully up to the end.
                    string responseFromServer = reader.ReadToEnd();

                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    //Debug.Print("Server Response: "+responseFromServer + " Status: " +status);
                }
            }
            catch (WebException ex)
            {
                Debug.Print("WebException while sending Highscore data to server" + ex.Message);
            }

        }
    }
}
