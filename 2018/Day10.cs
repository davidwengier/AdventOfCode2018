using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode._2018
{
    internal partial class Day10 : IAdventChallenge
    {
        public string Name => "Initial Attempt";

        public int Year => 2018;

        public int Day => 10;

        public string Part1()
        {
            var input = _input.Select(Parse).ToList();

            while (PrintBoard(input))
            {
                IncrementBoard(input);
            }

            return "";
        }

        private bool PrintBoard(List<PointInfo> input)
        {
            const int offsetX = 100;
            const int offsetY = 100;
            const int maxX = 200 + offsetX;
            const int maxY = 100 + offsetY;
           /* if (input.Any(p => (p.X >= offsetX && p.Y >= offsetY && p.X <= maxX && p.Y <= maxY)))
            {
                Console.Clear();
                foreach (var point in input)
                {
                    if (point.X >= offsetX && point.Y >= offsetY && point.X <= maxX && point.Y <= maxY)
                    {
                        Console.SetCursorPosition(point.X - offsetX, point.Y - offsetY);
                        Console.Write("#");
                    }
                }
                if (input[0].X == 210 && input[0].Y == 191)
                {
                    return false;
                }
            }
            */
            return true;
        }

        private void IncrementBoard(List<PointInfo> input)
        {
            foreach (var point in input)
            {
                point.X += point.VX;
                point.Y += point.VY;
            }
        }

        private PointInfo Parse(string input)
        {
            return new PointInfo(input);
        }

        public string Part2()
        {
            var input = _input.Select(Parse).ToList();

            int i = 0;
            while (PrintBoard(input))
            {
                IncrementBoard(input);
                i++;
            }

            return i.ToString();
        }

        private class PointInfo
        {

            public PointInfo(string input)
            {
                X = Convert.ToInt32(input.Substring(10, 6));
                Y = Convert.ToInt32(input.Substring(17, 7));

                VX = Convert.ToInt32(input.Substring(36, 2));
                VY = Convert.ToInt32(input.Substring(39, 3));
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int VX { get; }
            public int VY { get; }
        }
    }
}
