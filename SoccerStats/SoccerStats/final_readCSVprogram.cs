﻿/*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            Console.WriteLine("Golas,TeamName");
            foreach (var t in filecontents)
            {
                Console.WriteLine(t.Goals + "," + t.TeamName);
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
                    if (double.TryParse(values[7], out posessionPercent))
                    {
                        gameResult.PosessionPercent = posessionPercent;
                    }
                    //gameResult.ConversionRate = (double)gameResult.Goals / (double)gameResult.GoalAttempts;
                    soccerResults.Add(gameResult);
                }

            }

            return soccerResults;
        }
    }
}
*/