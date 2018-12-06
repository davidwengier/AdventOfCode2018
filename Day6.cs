using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal partial class Day6
    {
        internal static void Part1()
        {
            var input = _input;

            int maxx = input.Max(m => m.x) + 2;
            int maxy = input.Max(m => m.y) + 2;

            int[,] board = new int[maxx, maxy];


            List<int> toRemove = new List<int>();
            var maxes = new Dictionary<int, int>();
            for (int j = 0; j < maxy; j++)
            {
                for (int i = 0; i < maxx; i++)
                {
                    var num = FindClosest(i, j, input);

                    if (num == -1)
                    {
                        continue;
                    }
                    if (i == 0 || j == 0 || i == maxx - 1 || j == maxy - 1)
                    {
                        toRemove.Add(num);
                    }
                    if (toRemove.Contains(num))
                    {
                        continue;
                    }
                    if (!maxes.ContainsKey(num))
                    {
                        maxes.Add(num, 1);
                    }
                    else
                    {
                        maxes[num]++;
                    }
                }
            }

            int max = maxes.Values.Max();
            Console.WriteLine(max);
        }
        internal static void Part2()
        {
            var input = _input;
            var threshold = 10000;

            int maxx = input.Max(m => m.x) + 2;
            int maxy = input.Max(m => m.y) + 2;

            int[,] board = new int[maxx, maxy];

            int count = 0;
            for (int j = 0; j < maxy; j++)
            {
                for (int i = 0; i < maxx; i++)
                {
                    int dist = input.Sum(s => ManhattenDistance(i, j, s));
                    if (dist < threshold)
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine(count);
        }

        private static int FindClosest(int i, int j, (int x, int y)[] input)
        {
            int min = int.MaxValue;
            var cur = 0;
            int c = 0;
            var points = new Dictionary<int, int>();
            foreach (var coord in input)
            {
                c++;
                int dist = ManhattenDistance(i, j, coord);
                points.Add(c, dist);
                if (min > dist)
                {
                    min = dist;
                    cur = c;
                }
            }
            if (points.Values.Count(v => v == points[cur]) > 1)
            {
                return -1;
            }
            return cur;
        }

        private static int ManhattenDistance(int i, int j, (int x, int y) coord)
        {
            return Math.Abs(i - coord.x) + Math.Abs(j - coord.y);
        }
    }
}