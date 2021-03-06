﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace SoccerStats
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
          
            var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");
            var filecontents = ReadSoccerResult(fileName);
            fileName = Path.Combine(directory.FullName, "Players.json");
            var players = DeserializePlayers(fileName);
            var toptenPlayer = GetTopTenPlayers(players);
            foreach(var player in toptenPlayer)
            {
                Console.WriteLine("Name: "+player.FirstName+" PPG: "+player.PointsPerGame);
            }
            fileName = Path.Combine(directory.FullName, "topTen.json");
            serializePlayerToFile(toptenPlayer, fileName);
            Console.WriteLine("--------------------------------------------------");
            string[] h = readLinebyLine(GetGoogleHomePage());
            foreach(var t in h )
            {
                Console.WriteLine(t);
            }
            Console.ReadLine();


        }

        public static string RedFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }
        public static List<GameResult> ReadSoccerResult(string fileName)
        {
            var soccerResults = new List<GameResult>();
            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    var gameResult = new GameResult();
                    string[] values = line.Split(',');

                    DateTime gameDate;
                    if (DateTime.TryParse(values[0], out gameDate))
                    {
                        gameResult.GameDate = gameDate;
                    }
                    gameResult.TeamName = values[1];
                    HomeOrAway homeoraway;
                    if (Enum.TryParse(values[2], out homeoraway))
                    {
                        gameResult.HomeOrAway = homeoraway;
                    }
                    int parseInt;
                    if (int.TryParse(values[3], out parseInt))
                    {
                        gameResult.Goals = parseInt;
                    }
                    if (int.TryParse(values[4], out parseInt))
                    {
                        gameResult.GoalAttempts = parseInt;
                    }
                    if (int.TryParse(values[5], out parseInt))
                    {
                        gameResult.ShotsOnGoal = parseInt;
                    }
                    if (int.TryParse(values[6], out parseInt))
                    {
                        gameResult.ShotsOffGoal = parseInt;
                    }
                    double posessionPercent;
                    if(double.TryParse(values[7], out posessionPercent))
                    {
                        gameResult.PosessionPercent = posessionPercent;
                    }
                    //gameResult.ConversionRate = (double)gameResult.Goals / (double)gameResult.GoalAttempts;
                    soccerResults.Add(gameResult);
                }
                
            }
            
            return soccerResults;
        }

        public static List<Player> DeserializePlayers(string fileName)
        {
            var players = new List<Player>();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using(var jsonReader = new JsonTextReader(reader))
            {
                players = serializer.Deserialize<List<Player>>(jsonReader);
            }
            return players;
        }

        public static List<Player> GetTopTenPlayers(List<Player> players)
        {
            var toptenPlayers = new List<Player>();
            players.Sort(new PlayerComparer());
            int counter = 0; 
            foreach(var player in players)
            {
                toptenPlayers.Add(player);
                counter++;
                if (counter == 10)
                    break;
            }
            return toptenPlayers;
        }

        public static void serializePlayerToFile(List<Player> Players , string fileName )
        {
            
            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(fileName))
            using (var JsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(JsonWriter,Players);
            }
        }


        public static string GetGoogleHomePage()
        {
            var webClient = new WebClient();
            byte[] googleHome = webClient.DownloadData("https://www.google.com");

            using (var stream = new MemoryStream(googleHome))
            using(var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string GetNewsForPlayer(string playerName)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("Ocp-Apim-Subscription-Key", "17b524");
            byte[] SearchResult = webClient.DownloadData(string.Format("https://api.cognitive.microsoft.com/bing/v7.0/search?q={0}&mkt=en-us", playerName));

            using (var stream = new MemoryStream(SearchResult))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string[] readLinebyLine( string fileconent)
        {
            string[] file = fileconent.Split(new char[] { '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return file;
        }
    }
}
