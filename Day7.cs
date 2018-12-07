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
            var nodes = GetNodes();

            var toProcess = new SortedSet<RuleNode>();
            var done = new HashSet<char>();
            foreach (var n in nodes.Values.Where(n => n.Previous.Count == 0))
            {
                toProcess.Add(n);
            }
            int i = 0;
            while (toProcess.Count > 0)
            {
                RuleNode next = toProcess.ElementAt(i);
                if (next.Previous.All(n => done.Contains(n.Name)))
                {
                    done.Add(next.Name);
                    toProcess.Remove(next);
                    i = 0;
                }
                else
                {
                    i++;
                    toProcess.Add(next);
                }
                foreach (var n in next.Next.OrderBy(n => n.Name))
                {
                    toProcess.Add(n);
                }
            }

            Console.WriteLine(string.Join("", done));
        }

        private static Dictionary<char, RuleNode> GetNodes()
        {
            var nodes = new Dictionary<char, RuleNode>();
            foreach (string rule in _input)
            {
                var r = new Rule(rule);
                if (!nodes.ContainsKey(r.StepName))
                {
                    nodes.Add(r.StepName, new RuleNode(r.StepName));
                }
                if (!nodes.ContainsKey(r.PreceedingStepName))
                {
                    nodes.Add(r.PreceedingStepName, new RuleNode(r.PreceedingStepName));
                }
                nodes[r.StepName].Previous.Add(nodes[r.PreceedingStepName]);
                nodes[r.PreceedingStepName].Next.Add(nodes[r.StepName]);
            }

            return nodes;
        }

        internal static void Part2()
        {
            var nodes = GetNodes();

            int numWorkers = 5;
            int extraTime = 60;

            int secs = 0;
            var workers = new WorkerInfo[numWorkers];
            for (int j = 0; j < numWorkers; j++)
            {
                workers[j] = new WorkerInfo();
            }


            var toProcess = new SortedSet<RuleNode>();
            var done = new HashSet<char>();
            foreach (var n in nodes.Values.Where(n => n.Previous.Count == 0))
            {
                toProcess.Add(n);
            }
            while (toProcess.Count > 0 || done.Count != nodes.Count)
            {
                // see if any have finished
                foreach (var worker in workers)
                {
                    if (worker.Working)
                    {
                        if (worker.FinishTime == secs)
                        {
                            done.Add(worker.Job);
                            worker.Working = false;
                        }
                    }
                }


                RuleNode next = toProcess.FirstOrDefault(n => n.Previous.All(n2 => done.Contains(n2.Name)));
                while (next != null && workers.Any(w => !w.Working))
                {
                    // find a worker to do this
                    foreach (var worker in workers)
                    {
                        if (!worker.Working)
                        {
                            worker.Working = true;
                            worker.Job = next.Name;
                            worker.FinishTime = secs + extraTime + (worker.Job - 'A' + 1);
                            break;
                        }
                    }
                    toProcess.Remove(next);
                    foreach (var n in next.Next.OrderBy(n => n.Name))
                    {
                        toProcess.Add(n);
                    }
                    next = toProcess.FirstOrDefault(n => n.Previous.All(n2 => done.Contains(n2.Name)));
                }

                secs++;
            }

            Console.WriteLine(string.Join("", done));
            Console.WriteLine(secs-1);
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
        private class RuleNode : IComparable<RuleNode>
        {
            public RuleNode(char name)
            {
                Name = name;
            }
            public char Name { get; }
            public HashSet<RuleNode> Previous { get; } = new HashSet<RuleNode>();
            public HashSet<RuleNode> Next { get; } = new HashSet<RuleNode>();

            int IComparable<RuleNode>.CompareTo(RuleNode other)
            {
                return Name.CompareTo(other.Name);
            }
        }

        private class WorkerInfo
        {
            public bool Working { get; internal set; }
            public int FinishTime { get; internal set; }
            public char Job { get; internal set; }
        }
    }
}
