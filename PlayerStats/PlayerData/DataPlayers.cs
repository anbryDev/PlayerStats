using Newtonsoft.Json.Linq;
using PlayerStats.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace PlayerStats.PlayerData
{
    public class DataPlayers
    {

        public const string pathFile = "C:\\temp\\data\\headtohead.json";

        /// <summary>
        /// Read json file 
        /// </summary>
        /// <returns>List of Player</returns>
        public List<Player> GetData()
        {
            List<Player> allPlayers = new List<Player>();
            string json = string.Empty;
            json = File.ReadAllText(pathFile);
            JObject jsonObject = JObject.Parse(json);
            List<JToken> results = jsonObject["players"].Children().ToList();

            foreach (JToken result in results)
            {
                Player searchResult = result.ToObject<Player>();
                allPlayers.Add(searchResult);
            }

            return allPlayers;    
        }
        
    }
}