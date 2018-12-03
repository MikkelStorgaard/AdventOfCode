using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        static (int, int, int, int, int) tupleFromMatch(Match match)
        {

            var ints = match.Groups.Skip(1).Select(group => int.Parse(group.Value)).ToList();
            return (ints[0], ints[1], ints[2], ints[3], ints[4]);
        }

        static int AdventOfCode3(List<(int, int, int, int, int)> inputs)
        {
            int[] fabric = new int[1000*1000];
            HashSet<int> goodClaims = new HashSet<int>();
            int overlap = 0;

            foreach (var (claim, x, y, width, height) in inputs)
            {
                goodClaims.Add(claim);
                for (int i = x; i < x+width; i++)
                {
                    for (int j = y; j < y + height; j++)
                    {
                        if (fabric[i * 1000 + j] != 0)
                        {
                            goodClaims.Remove(fabric[i * 1000 + j]);
                            goodClaims.Remove(claim);
                            fabric[i * 1000 + j] = -1;
                        }

                        if (fabric[i * 1000 + j] == 0)
                        {
                            fabric[i * 1000 + j] = claim;
                        }
                    }
                }
            }

            Console.WriteLine($"Only fabric claim without overlaps is {goodClaims.First()}");
            return fabric.Count(value => value == -1);
        }

        static void Main(string[] args)
        {
            List<(int, int, int, int, int)> inputs = new List<(int, int, int, int, int)>();

            var stream = File.Open("input3.txt", FileMode.Open);
            var sr = new StreamReader(stream);
            string line;
            var regex = new Regex(@"\#(\d+) @ (\d+),(\d+): (\d+)x(\d+)");
            Match regexMatch;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                regexMatch = regex.Match(line);
                inputs.Add(tupleFromMatch(regexMatch));
            }

            var result = AdventOfCode3(inputs);
            Console.WriteLine($"Number over overlapped fabric cells is {result}");
        }
    }
}