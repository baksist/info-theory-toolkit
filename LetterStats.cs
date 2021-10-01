using System;
using System.Collections.Generic;
using ConsoleTables;

namespace info_theory_toolkit
{
    public class LetterStats
    {
        public string Message;
        public SortedDictionary<char, int> frequencyDict;
        public SortedDictionary<char, double> probabilityDict;

        public LetterStats(string input)
        {
            Message = input;
        }

        public void Calculate()
        {
            frequencyDict = new SortedDictionary<char, int>();
            probabilityDict = new SortedDictionary<char, double>();

            var msg = StripText();
            foreach (var c in msg)
            {
                if (frequencyDict.ContainsKey(c))
                {
                    frequencyDict[c]++;
                }
                else
                {
                    frequencyDict.Add(c, 1);
                }
            }

            foreach (var item in frequencyDict)
            {
                probabilityDict.Add(item.Key, (double) item.Value / msg.Length);
            }
        }

        public void Print()
        {
            var table = new ConsoleTable("символ", "f", "P(x)");
            foreach (var item in frequencyDict)
            {
                table.AddRow(item.Key, item.Value, Math.Round(probabilityDict[item.Key], 3));
            }
            table.Write(Format.Alternative);
        }

        public SortedDictionary<char, double> GetProbInfo()
        {
            Calculate();
            return probabilityDict;
        }

        public string StripText()
        {
            string result = "";
            foreach (var c in Message.ToLower())
            {
                if (char.IsLetter(c))
                    result += c;
            }

            return result;
        }
    }
}