using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public struct ScoreData
    {
        public string Name;
        public int Value;

        public ScoreData(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }

    static class GameScore
    {
        const int MAX_NUM_OF_SCORE = 5;
        const string SCORE_FILE_NAME = "scores.txt";

        public static List<ScoreData> ReadScoresFromFile()
        {
            List<ScoreData> scores = new List<ScoreData>();

            string fileName = SCORE_FILE_NAME;
            if (File.Exists(fileName))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        //while (reader.EndOfStream == false)
                        //{
                        for (int i = 0; i < MAX_NUM_OF_SCORE; i++)
                        {
                            string line = reader.ReadLine();
                            string[] fields = line.Split(',');
                            int scoreValue = int.Parse(fields[1]);
                            scores.Add(new ScoreData(fields[0], scoreValue));
                        }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    scores = SetDefaultScore();
                }
            }
            else
            {
                scores = SetDefaultScore();
            }

            // Descending Order
            scores.Sort((first, second) => second.Value.CompareTo(first.Value));

            return scores;
        }

        private static List<ScoreData> SetDefaultScore()
        {
            List<ScoreData> scores = new List<ScoreData>();

            // Create default Score Data
            scores.Add(new ScoreData("-----", 0));
            scores.Add(new ScoreData("-----", 0));
            scores.Add(new ScoreData("-----", 0));
            scores.Add(new ScoreData("-----", 0));
            scores.Add(new ScoreData("-----", 0));

            // Save Score data
            SaveScores(scores);

            return scores;
        }

        public static void SaveScores(List<ScoreData> scores)
        {
            // Descending Order
            scores.Sort((first, second) => second.Value.CompareTo(first.Value));

            using (StreamWriter writer = new StreamWriter(SCORE_FILE_NAME, false))
            {
                for (int i = 0; i < MAX_NUM_OF_SCORE; i++)
                {
                    writer.Write($"{scores[i].Name}, {scores[i].Value}{Environment.NewLine}");
                }
                writer.Flush();
            }
        }

        public static bool CheckHighScore(ScoreData score)
        {
            List<ScoreData> scores = ReadScoresFromFile();

            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i].Value < score.Value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
