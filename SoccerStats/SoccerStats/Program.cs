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
            //var files = directory.GetFiles("*.txt");
            //foreach(var file in files)
            //{
            //    Console.WriteLine(file.Name);
            //}
            //var fileName = Path.Combine(directory.FullName, "data.txt");
            var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");
            var filecontents = RedFile(fileName);
            string[] fileLines = filecontents.Split(new char[] { '\r', '\n' },StringSplitOptions.RemoveEmptyEntries);
            foreach(var line in fileLines)
            {
                Console.WriteLine(line);
            }
            //string[] ss = filecontents.Split(',');

            //Console.WriteLine(filecontents);
            //var file = new FileInfo(fileName);
            //if (file.Exists)
            //{
            //    using (var reader = new StreamReader(file.FullName))
            //    {
            //        Console.SetIn(reader);
            //        Console.WriteLine(Console.ReadLine());
            //    }

            //    //    var reader = new StreamReader(file.FullName);
            //    //try
            //    //{
            //    //    Console.SetIn(reader);
            //    //    Console.WriteLine(Console.ReadLine());
            //    //}
            //    //finally
            //    //{
            //    //    reader.Close();
            //    //}
            //}
            Console.ReadLine();
        }

        public static string RedFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }
        public static List<string> ReadSoccerResult(string fileName)
        {
            var soccerResults = new List<string>();
            using (var reader = new StreamReader(fileName))
            {
                while(reader.Peek() > -1)
                {
                    string line = reader.ReadLine();
                    soccerResults.Add(line);
                }
            }
            return soccerResults;
        }
    }
}
