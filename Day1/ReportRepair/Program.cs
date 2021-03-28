using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ReportRepair
{
    class Program
    {
        private static List<int> _entries;
        static void Main(string[] args)
        {
            _entries = ProcessInputFile();
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            Console.WriteLine("--- Part One ---");
            var sw = Stopwatch.StartNew();
            var result = SearchValue(2);
            Console.WriteLine($"Solution : {result}");
            sw.Stop();
            Console.WriteLine($"Time to calculate Solution : {sw.ElapsedMilliseconds}");
        }

        private static void PartTwo()
        {
            Console.WriteLine("--- Part Two ---");
            var sw = Stopwatch.StartNew();
            var result = SearchValue(3);
            Console.WriteLine($"Solution : {result}");
            sw.Stop();
            Console.WriteLine($"Time to calculate Solution : {sw.ElapsedMilliseconds}");
        }

        private static int SearchValue(int length)
        {
            var value = 2020;
            var entries = GetPossibleCombinations(_entries, length);
            Console.WriteLine($"Possible combinations found : {entries.Count()}");
            var values = entries.Single(i => i.Aggregate((i1, i2) => i1 + i2) == value);
            return values.Aggregate((e1, e2) => e1 * e2);
        }

        private static IEnumerable<IEnumerable<int>> GetPossibleCombinations(IEnumerable<int> entries, int length) 
        {
            if (length == 1)
            {
                return entries.Select(t => new [] { t });
            }
            return GetPossibleCombinations(entries, length - 1)
                .SelectMany(t => entries.Where(e => e.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new [] { t2 }));
        }
        
        private static List<int> ProcessInputFile()
        {
            Console.WriteLine("--- Processing Input file ---");
            var result = new List<int>();
            string line;
            var file = new StreamReader(@$"{AppDomain.CurrentDomain.BaseDirectory}input.txt");
            while ((line = file.ReadLine()) != null)
            {
                result.Add(int.Parse(line));
            }

            file.Close();
            Console.WriteLine($"{result.Count} lines processed");
            return result;
        }
    }
}
