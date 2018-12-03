using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Code2
{
    class Program
    {

        static (bool, bool) thing (char[] prut, Dictionary<char, int> charMap)
        {
            charMap.Clear();
            var count = 0;
            foreach (var c in prut)
            {

                charMap[c] = charMap.GetValueOrDefault(c, 0) + 1;
            }

            return (charMap.ContainsValue(2), charMap.ContainsValue(3));
        }


        static void Main(string[] args)
        {
            Dictionary<char, int> charMap = new Dictionary<char, int>();
            int max = 1018;
            List<string> inputs = new List<string>();
            HashSet<int> freqs = new HashSet<int>();

            var stream = File.Open("input", FileMode.Open);
            var sr = new StreamReader(stream);
            string line;
            var regex = new Regex(@"(\w+)");
            Match regexMatch;
            var sum = 0;
            var duplicateFound = false;
            char[] v;
            (bool, bool) tuple;

            int twos = 0;
            int threes = 0;

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                regexMatch = regex.Match(line);
                v = regexMatch.Value.ToCharArray();
                var (hasTwo, hasThree) = thing(v, charMap);

                if (hasTwo)
                {
                    twos += 1;
                }
                if (hasThree)
                {
                    threes += 1;
                }
                inputs.Add(regexMatch.Value);
            }
            var stringArr = inputs.ToArray();
            for (int i = 0; i < stringArr.Length-1; i++)
            {
                for (int j = i+1; j < stringArr.Length; j++)
                {
                    if (stringDiff(stringArr[i], stringArr[j]) == 1)
                    {
                        Console.WriteLine("The good ID is");
                        for (int k = 0; k < stringArr[i].Length; k++)
                        {
                            if(stringArr[i][k] == stringArr[j][k])
                            {
                                Console.Write(stringArr[i][k]);
                            }

                        }
                        Console.Write("\n");
                    }
                }
            }

            Console.WriteLine($"Checksum is {twos * threes}");
        }
        static int stringDiff(string a, string b)
        {
            var diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    diff++;
                }
            }
            return diff;
        }
    }
}