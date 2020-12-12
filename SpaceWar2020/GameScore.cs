/* 
 * GameScore.cs
 * Final Project: SpaceWar2020
 *                Manage Game Score Data (save and read a File)
 * Revision History:
 *      Yiphyo Hong, 2020.12.03: Version 1.0
 *      
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    /// <summary>
    /// Score data struct 
    /// </summary>
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

    /// <summary>
    /// Class for manipulating high score date (file)
    /// </summary>
    static class GameScore
    {
        /// <summary>
        /// Maximum number of scores
        /// </summary>
        const int MAX_NUM_OF_SCORE = 5;

        /// <summary>
        /// Name of a high score file
        /// </summary>
        const string SCORE_FILE_NAME = "scores.txt";

        /// <summary>
        /// Read high score list from a file
        /// Format {name, score} per line
        /// </summary>
        /// <returns>score list</returns>
        public static List<ScoreData> ReadScoresFromFile()
        {
            List<ScoreData> scores = new List<ScoreData>();

            string fileName = SCORE_FILE_NAME;

            // If score file does not exist, set default score to a new file
            if (File.Exists(fileName))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        for (int i = 0; i < MAX_NUM_OF_SCORE; i++)
                        {
                            string line = reader.ReadLine();
                            string[] fields = line.Split(',');
                            int scoreValue = int.Parse(fields[1]);
                            scores.Add(new ScoreData(fields[0], scoreValue));
                        }
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

        /// <summary>
        /// Save default scores to a file name = "-----", score = 0
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Save score list to a file
        /// </summary>
        /// <param name="scores">score list</param>
        public static void SaveScores(List<ScoreData> scores)
        {
            // Descending Order
            scores.Sort((first, second) => second.Value.CompareTo(first.Value));

            // Save scores to a file, one {name, score} per line
            using (StreamWriter writer = new StreamWriter(SCORE_FILE_NAME, false))
            {
                for (int i = 0; i < MAX_NUM_OF_SCORE; i++)
                {
                    writer.Write($"{scores[i].Name}, {scores[i].Value}{Environment.NewLine}");
                }
                writer.Flush();
            }
        }

        /// <summary>
        /// Check input score is in 5th in the high score board
        /// </summary>
        /// <param name="score">new score</param>
        /// <returns>result flag</returns>
        public static bool CheckHighScore(int score)
        {
            List<ScoreData> scores = ReadScoresFromFile();

            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i].Value < score)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Update score list, keep max number of list (5)
        /// </summary>
        /// <param name="score">new score</param>
        public static void WriteScore(ScoreData score)
        {
            // If no name, insert default name
            if (string.IsNullOrWhiteSpace(score.Name))
            {
                score.Name = "-----";
            }

            // Read score list from a file
            List<ScoreData> scores = ReadScoresFromFile();

            // Add new score
            scores.Add(score);

            // Descending Order
            scores.Sort((first, second) => second.Value.CompareTo(first.Value));

            // Remove last score
            scores.RemoveAt(scores.Count - 1);

            // Write scores to a file
            using (StreamWriter writer = new StreamWriter(SCORE_FILE_NAME, false))
            {
                for (int i = 0; i < MAX_NUM_OF_SCORE; i++)
                {
                    writer.Write($"{scores[i].Name}, {scores[i].Value}{Environment.NewLine}");
                }
                writer.Flush();
            }
        }
    }
}
