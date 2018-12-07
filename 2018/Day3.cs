using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal partial class Day3
    {
        internal static void Part1()
        {
            int[,] fabric = new int[1001, 1001];
            foreach (string claim in _input)
            {
                var piece = new FabricPiece(claim);

                for (int x = piece.X, i = 0; i < piece.Width; x++, i++)
                {
                    for (int y = piece.Y, j = 0; j < piece.Height; y++, j++)
                    {
                        fabric[x, y]++;
                    }
                }
            }
            int count = 0;
            for (int i = 0; i < 1001; i++)
            {
                for (int j = 0; j < 1001; j++)
                {
                    if (fabric[i, j] >= 2)
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine(count);
        }

        internal static void Part2()
        {
            int[,] fabric = new int[1001, 1001];
            foreach (string claim in _input)
            {
                var piece = new FabricPiece(claim);

                for (int x = piece.X, i = 0; i < piece.Width; x++, i++)
                {
                    for (int y = piece.Y, j = 0; j < piece.Height; y++, j++)
                    {
                        fabric[x, y]++;
                    }
                }
            }
            foreach (string claim in _input)
            {
                bool overlaps = false;
                var piece = new FabricPiece(claim);

                for (int x = piece.X, i = 0; i < piece.Width; x++, i++)
                {
                    for (int y = piece.Y, j = 0; j < piece.Height; y++, j++)
                    {
                        if (fabric[x, y] >= 2)
                        {
                            overlaps = true;
                            break;
                        }
                    }
                    if (overlaps) break;
                }
                if (!overlaps)
                {
                    Console.WriteLine(piece.ID);
                    return;
                }
            }
        }

        private class FabricPiece
        {
            public FabricPiece(string claim)
            {
                string value = "";
                foreach (char c in claim)
                {
                    if (c == '#')
                    {
                        value = "";
                    }
                    else if (c == ' ')
                    {
                        if (string.IsNullOrEmpty(this.ID)) this.ID = value;
                        value = "";
                    }
                    else if (c == ',')
                    {
                        this.X = int.Parse(value);
                        value = "";
                    }
                    else if (c == ':')
                    {
                        this.Y = int.Parse(value);
                        value = "";
                    }
                    else if (c == 'x')
                    {
                        this.Width = int.Parse(value);
                        value = "";
                    }
                    else
                    {
                        value += c;
                    }
                }
                this.Height = int.Parse(value);
            }

            public int X { get; }
            public int Y { get; }
            public int Width { get; }
            public int Height { get; }
            public string ID { get; }
        }
    }
}