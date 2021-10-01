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
            double sum = 0;
            var sum_bin = "";
            foreach (var item in alphabetInfo)
            {
                var log = -1 * Math.Log2(item.Value);
                var length = Math.Round(log);
                /*long m = BitConverter.DoubleToInt64Bits(sum);
                sum_bin = Convert.ToString(m, 2);*/
                ResultTable.AddRow(item.Key, Math.Round(item.Value, 3), Math.Round(log,3), 
                    length, Math.Round(sum,3), sum_bin, 0);
                sum += item.Value;
            }
            ResultTable.Write();
        }

    }
}