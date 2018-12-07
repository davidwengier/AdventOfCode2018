using System;
using System.Linq;

namespace AdventOfCode
{
    internal partial class Day5 : IAdventChallenge
    {
        public string Name => "First Attempt";
        public int Year => 2018;
        public int Day => 5;
        public string Part1()
        {
            int length = GetReducedLength(_input);
            return length.ToString();
        }

        public string Part2()
        {
            int min = int.MaxValue;
            string curr = null;

            foreach (string c in _input.Select(ch => ch.ToString()).Distinct(StringComparer.OrdinalIgnoreCase))
            {
                int len = GetReducedLength(_input.Replace(c, "", StringComparison.OrdinalIgnoreCase));
                if (len < min)
                {
                    min = len;
                    curr = c;
                }
            }
           return curr + " (" + min + ")";
        }

        private int GetReducedLength(string input)
        {
            bool changed = true;
            while (changed)
            {
                changed = false;

                char last = ' ';
                string new_input = "";
                foreach (char c in input)
                {
                    if (last == ' ')
                    {
                        last = c;
                    }
                    else if (char.ToUpper(c) == char.ToUpper(last) && c != last)
                    {
                        changed = true;
                        last = ' ';
                    }
                    else
                    {
                        new_input += last;
                        last = c;
                    }
                }
                new_input += last;
                input = new_input;
            }
            return input.Length;
        }
    }
}