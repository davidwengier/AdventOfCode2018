using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode._2018
{
    internal partial class Day8 : IAdventChallenge
    {
        public string Name => "Initial attempt";

        public int Year => 2018;

        public int Day => 8;

        public string Part1()
        {
            var root = new Node(_input);
            int sum = root.SumMetadata();
            return sum.ToString();
        }


        public string Part2()
        {
            var roo = new Node(_input);
            return roo.GetValue().ToString();
        }

        private class Node
        {
            public List<Node> Children { get; } = new List<Node>();
            public List<int> Metadata { get; } = new List<int>();
            public Node(string input)
                : this(input.GetEnumerator())
            {

            }

            public Node(CharEnumerator charEnumerator)
            {
                int children = ReadNext(charEnumerator);
                int metaDataCount = ReadNext(charEnumerator);
                for (int j = 0; j < children; j++)
                {
                    Children.Add(new Node(charEnumerator));
                }
                for (int j = 0; j < metaDataCount; j++)
                {
                    Metadata.Add(ReadNext(charEnumerator));
                }
            }

            private int ReadNext(CharEnumerator charEnumerator)
            {
                var sb = new StringBuilder();
                while (charEnumerator.MoveNext())
                {
                    if (charEnumerator.Current == ' ')
                    {
                        break;
                    }
                    sb.Append(charEnumerator.Current);
                }
                return int.Parse(sb.ToString());
            }

            internal int SumMetadata()
            {
                int sum = 0;
                foreach (var n in Metadata)
                {
                    sum += n;
                }
                foreach (var n in Children)
                {
                    sum += n.SumMetadata();
                }
                return sum;
            }

            internal int GetValue()
            {
                if (Children.Count == 0)
                {
                    int sum = 0;
                    foreach (var n in Metadata)
                    {
                        sum += n;
                    }
                    return sum;
                }
                else
                {
                    int sum = 0;
                    foreach (var n in Metadata)
                    {
                        if (n == 0) continue;
                        if (n > Children.Count) continue;
                        sum += Children[n - 1].GetValue();
                    }
                    return sum;
                }
            }
        }
    }
}
