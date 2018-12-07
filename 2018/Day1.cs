using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    internal partial class Day1
    {
        internal static void Part1()
        {
            long cum = 0;
            foreach (int i in _input)
            {
                cum += i;
            }
            Console.WriteLine(cum);
        }

        internal static void Part2()
        {
            int cum = 0;

            // Just use a hashset to keep track of what we've seen before
            // Since these are only integers we could in theory use an array, but we don't know the max that cum will get to (i guess we could have kept track above??)
            // and it might be so big that we run out of memory anyway. The cumulator is not going to have every value so the array could be quite sparse, making it
            // not efficient storage
            var seen = new HashSet<int>();
            while (true)
            {
                foreach (int i in _input)
                {
                    cum += i;
                    if (!seen.Add(cum))
                    {
                        Console.WriteLine(cum);
                        return;
                    }
                }
            }
        }
    }
}