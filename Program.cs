using System;
using System.Linq;
using ConsoleTables;

namespace info_theory_toolkit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите сообщение:");
            var msg = Console.ReadLine();
            var stat = new LetterStats(msg);
            stat.Calculate();
            stat.Print();

            var table = new ConsoleTable(InfoSourceStat.headers);
            for (var i = 0; i < 3; i++)
            {
                Console.WriteLine($"Введите сообщение №{i+1}");
                var text= Console.ReadLine();
                var st = new InfoSourceStat(text);
                st.Calculate();
                table.AddRow(st.Stats[0], st.Stats[1], st.Stats[2], st.Stats[3], st.Stats[4]);
            }
            table.Write();
            
            Console.WriteLine("Введите сообщение:");
            var msg_coding = Console.ReadLine();
            var enc = new ShannonEncoder(msg_coding);
            enc.Calculate();
        }
    }
}