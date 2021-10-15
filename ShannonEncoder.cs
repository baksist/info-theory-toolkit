using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using ConsoleTables;

namespace info_theory_toolkit
{
    public class ShannonEncoder
    {
        public string Message;

        public ShannonEncoder(string input)
        {
            Message = input;
        }

        public static string[] Headers = new string[]
        {
            "символ", "p(x)", "-log_2 p(x)", "l_i", "sum p_i", "sum p(a)", "код"
        };

        public Dictionary<char, string> CodeDict;
        public ConsoleTable ResultTable;

        public void Calculate()
        {
            var alphabetInfo = new LetterStats(Message).GetProbInfo().OrderBy(x => x.Value);
            ResultTable = new ConsoleTable(Headers);
            CodeDict = new Dictionary<char, string>();
            double sum = 0;
            var sum_bin = "";
            foreach (var item in alphabetInfo)
            {
                var log = -1 * Math.Log2(item.Value);
                var length = (int) Math.Round(log);
                var sum_round = Math.Round(sum, 3);
                sum_bin = FloatToBin(sum_round);
                var sum_trunc = sum_bin.Substring(0, 2 + length);
                var code = sum_bin.Substring(2, length);
                ResultTable.AddRow(item.Key, Math.Round(item.Value, 3), Math.Round(log,3), 
                    length, sum_round, sum_trunc, code);
                sum += item.Value;
                CodeDict.Add(item.Key, code);
            }
            ResultTable.Write();
            PrintEncMessage();
        }

        public void PrintEncMessage()
        {
            foreach (var c in Message)
            {
                if (CodeDict.Keys.Contains(c))
                    Console.Write(CodeDict[c] + " ");
            }
            Console.WriteLine();
        }

        private string FloatToBin(double x)
        {
            string result = "0.";
            for (var i = 0; i < 23; i++)
            {
                x = x * 2;
                var whole = Math.Truncate(x);
                result += whole.ToString();
                x = x - whole;
            }
            return result;
        }

    }
}