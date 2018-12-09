using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var challenges = from t in typeof(Program).Assembly.GetTypes()
                             where !t.IsInterface
                             where typeof(IAdventChallenge).IsAssignableFrom(t)
                             select Activator.CreateInstance(t) as IAdventChallenge;

            if (args.Length == 0)
            {
                int year = challenges.Max(c => c.Year);
                int day = challenges.Where(c => c.Year == year).Max(c => c.Day);
                WriteChallenges(challenges.Where(c => c.Year == year && c.Day == day));
            }
            else
            {
                foreach (int year in challenges.Select(c => c.Year).Distinct().OrderBy(c => c))
                {
                    var thisYear = challenges.Where(c => c.Year == year);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(year);
                    foreach (int day in thisYear.Select(c => c.Day).Distinct().OrderBy(c => c))
                    {
                        WriteChallenges(thisYear.Where(c => c.Day == day));
                    }
                }
            }
        }

        private static void WriteChallenges(IEnumerable<IAdventChallenge> challenges)
        {
            string lastPart1 = null;
            string lastPart2 = null;
            foreach (var challenge in challenges)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Day {challenge.Day}\n{challenge.Name}:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Part 1:");
                Console.ResetColor();
                string part1 = challenge.Part1();
                if (lastPart1 == null)
                {
                    lastPart1 = part1;
                }

                WritePartOutput(lastPart1, part1);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Part 2:");
                Console.ResetColor();
                string part2 = challenge.Part2();
                if (lastPart2 == null)
                {
                    lastPart2 = part2;
                }
                WritePartOutput(lastPart2, part2);
            }

            void WritePartOutput(string lastPart, string part)
            {
                if (part == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("No value was returned.");
                }
                if (lastPart.Equals(part, StringComparison.Ordinal))
                {
                    Console.ResetColor();
                    Console.WriteLine(part);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mismatch:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Result ");
                    Console.ResetColor();
                    Console.Write(part);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" does not match previous output ");
                    Console.ResetColor();
                    Console.WriteLine(lastPart);
                }
            }
        }

    }
}
