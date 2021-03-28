using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PasswordPhilosophy
{
    class Program
    {
        private static List<Password> _passwords;
        static void Main(string[] args)
        {
            _passwords = ProcessInputFile();
            PartOne();
            PartTwo();
        }


        private static void PartOne()
        {
            Console.WriteLine("--- Part One ---");
            var sw = Stopwatch.StartNew();
            Console.WriteLine($"Solution : {_passwords.Count(i => i.IsValid)}");
            sw.Stop();
            Console.WriteLine($"Time to calculate Solution : {sw.ElapsedMilliseconds}");
        }

        private static void PartTwo()
        {
            Console.WriteLine("--- Part Two ---");
            var sw = Stopwatch.StartNew();
            Console.WriteLine($"Solution : {_passwords.Count(i => i.TobogganIsValid)}");
            sw.Stop();
            Console.WriteLine($"Time to calculate Solution : {sw.ElapsedMilliseconds}");
        }


        private static List<Password> ProcessInputFile()
        {
            Console.WriteLine("--- Processing Input file ---");
            var result = new List<Password>();
            string line;
            var file = new StreamReader(@$"{AppDomain.CurrentDomain.BaseDirectory}input.txt");
            while ((line = file.ReadLine()) != null)
            {
                result.Add(Map(line));
            }

            file.Close();
            return result;
        }

        private static Password Map(string input)
        {
            var values = input.Split(new string[] {"-", " ", ": "}, StringSplitOptions.RemoveEmptyEntries);
            return new()
            {
                PolicyMinimal = int.Parse(values[0]),
                PolicyMaximum = int.Parse(values[1]),
                PolicyChar = char.Parse(values[2]),
                Value = values[3]
            };
        }

    }

    public class Password
    {
        public int PolicyMinimal { get; set; }
        public int PolicyMaximum { get; set; }
        public char PolicyChar { get; set; }
        public string Value { get; set; }
        public bool IsValid
        {
            get
            {
                var count = Value.Count(x => x == PolicyChar);
                return count >= PolicyMinimal && count <= PolicyMaximum;
            }
        }

        public bool TobogganIsValid
        {
            get
            {
                if (Value.Length < PolicyMinimal)
                {
                    return false;
                }

                var firstChar = Value[PolicyMinimal - 1];
                if (Value.Length < PolicyMaximum)
                {
                    return firstChar.Equals(PolicyChar);
                }
                var secondChar = Value[PolicyMaximum-1];

                return firstChar.Equals(PolicyChar) && !secondChar.Equals(PolicyChar) ||
                       !firstChar.Equals(PolicyChar) && secondChar.Equals(PolicyChar);
            }
        }
    }
}
