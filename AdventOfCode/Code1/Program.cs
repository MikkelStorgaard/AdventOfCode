using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Code1
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = 1018;
            int[] inputs = new int[max];
            HashSet<int> freqs = new HashSet<int>();

            var stream = File.Open("input", FileMode.Open);
            var sr = new StreamReader(stream);
            string line;
            var regex = new Regex(@"(\+|\-)(\d+)");
            Match regexMatch;
            var currentValue = 0;
            var sum = 0;
            var duplicateFound = false;

            for (int i = 0; i < max; i++)
            {
                line = sr.ReadLine();
                regexMatch = regex.Match(line);
                currentValue = int.Parse(regexMatch.Value);
                inputs[i] = currentValue;

                sum += currentValue;
                if (freqs.Contains(sum) && (!duplicateFound))
                {
                    Console.WriteLine($"First duplicate is {sum}");
                    duplicateFound = true;
                }
                freqs.Add(sum);
            }
            Console.WriteLine($"Freq. sum is {sum}");

            while (!duplicateFound)
            {
                for (int i = 0; i < max; i++)
                {
                    sum += inputs[i];
                    if (freqs.Contains(sum) && (!duplicateFound))
                    {
                        Console.WriteLine($"First duplicate is {sum}");
                        duplicateFound = true;
                        break;
                    }
                    freqs.Add(sum);
                }
            }
        }
    }
}