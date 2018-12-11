using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    internal partial class Day11 : IAdventChallenge
    {
        public string Name => "Initial Attempt";

        public int Year => 2018;

        public int Day => 11;

        public string Part1()
        {
            int serialNumber = _input;

            (string cur, int max) = CalculateMaxes(serialNumber, 3);

            return cur;
        }

        private static (string, int) CalculateMaxes(int serialNumber, int gridSize)
        {
            int max = 0;
            string cur = "";
            for (int xStart = 1; xStart <= (300 - gridSize); xStart++)
            {
                for (int yStart = 1; yStart <= (300 - gridSize); yStart++)
                {
                    int powerLevel = 0;
                    for (int x = xStart; x < xStart + gridSize; x++)
                    {
                        for (int y = yStart; y < yStart + gridSize; y++)
                        {
                            int rackID = x + 10;
                            int powerLevelStart = rackID * y;
                            int plusSerialNumber = powerLevelStart + serialNumber;
                            int multiplied = plusSerialNumber * rackID;

                            int hundreds = ((multiplied % 1000) - (multiplied % 100)) / 100;
                            powerLevel += hundreds - 5;
                        }
                    }
                    if (powerLevel > max)
                    {
                        max = powerLevel;
                        cur = $"{xStart},{yStart}";
                    }
                }
            }
            return (cur, max);
        }

        public string Part2()
        {
            int serialNumber = _input;

            var results = new ConcurrentDictionary<int, (string, int)>();

            Parallel.For(1, 301, (gridSize, _) =>
            {
                results[gridSize] = CalculateMaxes(serialNumber, gridSize);
            });

            int max = 0;
            string cur = "";
            foreach (var kvp in results)
            {
                if (kvp.Value.Item2 > max)
                {
                    max = kvp.Value.Item2;
                    cur = kvp.Value.Item1 + "," + kvp.Key;
                }
            }
            return cur;
        }
    }
}
