using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            var results = new Dictionary<string, int>();

            int gridSize = 3;

            CalculateMaxes(serialNumber, results, gridSize, "");
            return GetMax(results);
        }

        private static string GetMax(Dictionary<string, int> results)
        {
            int max = 0;
            string cur = "";
            foreach (var kvp in results)
            {
                if (kvp.Value > max)
                {
                    max = kvp.Value;
                    cur = kvp.Key;
                }
            }
            return cur;
        }

        private static void CalculateMaxes(int serialNumber, Dictionary<string, int> results, int gridSize, string keySuffix)
        {
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
                    results[$"{xStart},{yStart}{keySuffix}"] = powerLevel;
                }
            }
        }

        public string Part2()
        {
            int serialNumber = _input_test;

            var results = new Dictionary<string, int>();

            for (int gridSize = 1; gridSize <= 300; gridSize++)
            {

                CalculateMaxes(serialNumber, results, gridSize, "," + gridSize);

            }
            return GetMax(results);
        }
    }
}
