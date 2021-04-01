using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TobogganTrajectory
{
    class Program
    {
        private static List<Line> _lines;
        static void Main(string[] args)
        {
            _lines = ProcessInputFile();
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            Console.WriteLine("--- Part One ---");
            var sw = Stopwatch.StartNew();
            var trees = Slope(1, 3);
            Console.WriteLine($"Solution : Passed along {trees} Trees");
            sw.Stop();
            Console.WriteLine($"Time to calculate Solution : {sw.ElapsedMilliseconds}");
        }

        private static void PartTwo()
        {
            Console.WriteLine("--- Part Two ---");
            var sw = Stopwatch.StartNew();
            var trees = new[]
            {
                new { Down = 1, Right = 1 },
                new { Down = 1, Right = 3 },
                new { Down = 1, Right = 5 },
                new { Down = 1, Right = 7 },
                new { Down = 2, Right = 1 }
            }
                .Select(i => Slope(i.Down, i.Right))
                .Aggregate((i1, i2) => i1 * i2);
            Console.WriteLine($"Solution : Passed along {trees} Trees");
            sw.Stop();
            Console.WriteLine($"Time to calculate Solution : {sw.ElapsedMilliseconds}");
        }

        private static double Slope(int down, int right)
        {
            var trees = 0;
            var coordinate = new Coordinate { Row = 0, Col = 0 };
            var line = GetLine(coordinate);

            while (line != null)
            {
                coordinate = Step(coordinate, down, right);
                line = GetLine(coordinate);
                if (line != null && line.IsTree)
                {
                    trees++;
                }
            }

            return trees;
        }

        private static Coordinate Step(Coordinate coordinate, int row, int col) => new()
        {
            Row = coordinate.Row + row,
            Col = coordinate.Col + col
        };

        private static Line GetLine(Coordinate coordinate)
        {
            var row = _lines.Where(i => i.Coordinate.Row.Equals(coordinate.Row)).ToList();
            return row.Any() 
                ? row.Single(i => i.Coordinate.Col.Equals(GetColumn(row.Count, coordinate.Col))) 
                : null;
        }

        private static int GetColumn(int columns, int col)
        {
            return col >= columns ? GetColumn(columns, col - columns) : col;
        }

        private static List<Line> ProcessInputFile()
        {
            Console.WriteLine("--- Processing Input file ---");
            var result = new List<Line>();
            string line;
            var file = new StreamReader(@$"{AppDomain.CurrentDomain.BaseDirectory}input.txt");
            var row = 0;
            while ((line = file.ReadLine()) != null)
            {
                var col = 0;
                foreach (var c in line)
                {
                    result.Add(new()
                    {
                        IsTree = c.Equals('#'),
                        Coordinate = new Coordinate()
                        {
                            Col = col,
                            Row = row
                        }
                    });
                    col++;
                }
                row++;
            }

            file.Close();
            return result;
        }
    }

    public class Line
    {
        public Coordinate Coordinate { get; set; }
        public bool IsTree { get; set; }
    }

    public class Coordinate
    {
        public int Col { get; set; }
        public int Row { get; set; }
    }
}
