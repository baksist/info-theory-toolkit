using System;
using System.Collections.Generic;

namespace info_theory_toolkit
{
    public class InfoSourceStat
    {
        public string Message;
        public double[] Stats;

        public static string[] headers = new string[]
        {
            "H(U)", "H_max (U)", "I_н (U)", "x", "H'(U)"
        };

        public InfoSourceStat(string input)
        {
            Message = input;
            Stats = new double[5];
        }

        public void Calculate()
        {
            var msgStats = new LetterStats(Message);
            var alphabetInfo = msgStats.GetProbInfo();

            double entropy = 0;
            foreach (var c in alphabetInfo.Keys)
            {
                var p = alphabetInfo[c];
                entropy += p * Math.Log2(p);
            }
            entropy *= -1;
            Stats[0] = Math.Round(entropy, 3);

            double max_entropy = Math.Log2(alphabetInfo.Count);
            Stats[1] = Math.Round(max_entropy, 3);

            double saturation = msgStats.StripText().Length * entropy;
            Stats[2] = Math.Round(saturation, 3);

            double redundancy = (max_entropy - entropy) / entropy;
            Stats[3] = Math.Round(redundancy, 3);

            double productivity = 2 * max_entropy;
            Stats[4] = Math.Round(productivity, 3);
        }

        public double GetEntropy()
        {
            var alphabetInfo = new LetterStats(Message).GetProbInfo();
            double entropy = 0;
            foreach (var c in alphabetInfo.Keys)
            {
                var p = alphabetInfo[c];
                entropy += p * Math.Log2(p);
            }
            entropy *= -1;
            return entropy;
        }
    }
}