using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal partial class Day2
    {
        internal static void Part1()
        {
            int twos = 0;
            int threes = 0;

            foreach (string id in _input)
            {
                // really lazy solution but they never said perf was an issue :)
                if (id.Any(ch => id.Count(c => c.Equals(ch)) == 2))
                {
                    twos++;
                }
                if (id.Any(ch => id.Count(c => c.Equals(ch)) == 3))
                {
                    threes++;
                }
            }
            Console.WriteLine(twos * threes);
        }

        internal static void Part2()
        {
            foreach (string id in _input)
            {
                // find an id that differs by only one character
                foreach (string other in _input)
                {
                    // skip ourselves
                    if (id.Equals(other)) continue;

                    // We just need to know the index to skip, and whether we ran off the end or not
                    // We could put the logic inside the inner loop, for if we get to the last element
                    // or use a goto to the output logic instead of breaking, but you'll find laziness
                    // is a theme when I do these sort of things.
                    // Either way there is no need to track all of the indexes that differ, is the point.
                    bool tooMany = false;
                    int diffIndex = 0;
                    for (int i = 0; i < other.Length; i++)
                    {
                        if (id[i] != other[i])
                        {
                            if (diffIndex == 0)
                            {
                                diffIndex = i;
                            }
                            else if (diffIndex > 0)
                            {
                                tooMany = true;
                                // it ain't this one
                                break;
                            }
                        }
                    }

                    if (!tooMany)
                    {
                        // found the two matching boxes, so output the diff by skipping the index we found
                        for (int i = 0; i < other.Length; i++)
                        {
                            if (diffIndex == i) continue;
                            Console.Write(other[i]);
                        }
                        Console.WriteLine();
                        return;
                    }
                }
            }
        }
    }
}