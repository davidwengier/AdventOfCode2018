using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    internal partial class Day7
    {
        internal static void Part1()
        {
            var nodes = new Dictionary<char, RuleNode>();
            var rules = new List<Rule>();
            foreach (string rule in _input)
            {
                var r = new Rule(rule);
                nodes[r.StepName] = new RuleNode(r.StepName);
                nodes[r.PreceedingStepName] = new RuleNode(r.PreceedingStepName);
                rules.Add(r);
            }

            foreach (var rule in rules)
            {
                var node = nodes[rule.StepName];
                foreach (var r in rules)
                {
                    if (r.StepName == rule.StepName && rule.PreceedingStepName > 0)
                    {
                        var otherNode = nodes[r.PreceedingStepName];
                        node.Previous.Add(otherNode);
                        otherNode.Next.Add(node);

                    }
                }
                foreach (var r in rules)
                {
                    if (r.PreceedingStepName == rule.StepName)
                    {
                        var otherNode = nodes[r.StepName];
                        node.Next.Add(otherNode);
                        otherNode.Previous.Add(node);
                    }
                }
            }

            var result = new HashSet<char>();
            GetNode(nodes, r => r.Previous.Count == 0, result);
            
            Console.WriteLine(string.Join("", result));

        }

        private static void GetNode(Dictionary<char, RuleNode> nodes, Func<RuleNode, bool> p, HashSet<char> storySoFar)
        {
            foreach (var node in nodes.Values.Where(p).OrderBy(r => r.Name))
            {
                storySoFar.Add(node.Name);
                GetNextNodes(node, storySoFar);
            }
        }

        private static void GetNextNodes(RuleNode node, HashSet<char> storySoFar)
        {
            foreach (var n in node.Next.OrderBy(n => n.Name))
            {
                if (n.Previous.All(n2 => storySoFar.Contains(n2.Name)))
                {
                    storySoFar.Add(n.Name);
                    GetNextNodes(n, storySoFar);
                }
            }
        }

        internal static void Part2()
        {

        }

        [DebuggerDisplay("{PreceedingStepName} -> {StepName}")]
        private class Rule
        {
            public Rule(string rule)
            {
                //           111111111122222222223333333333
                // 0123456789012345678901234567890123456789
                // Step C must be finished before step A can begin
                this.PreceedingStepName = rule[5];
                this.StepName = rule[36];
            }

            public char PreceedingStepName { get; }
            public char StepName { get; }
        }

        [DebuggerDisplay("{Name}")]
        private class RuleNode
        {
            public RuleNode(char name)
            {
                Name = name;
            }
            public char Name { get; }
            public HashSet<RuleNode> Previous { get; } = new HashSet<RuleNode>();
            public HashSet<RuleNode> Next { get; } = new HashSet<RuleNode>();
        }
    }
}
