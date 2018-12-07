using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode
{
    internal partial class Day4 : IAdventChallenge
    {
        public string Name => "First Attempt";
        public int Year => 2018;
        public int Day => 4;
        public string Part1()
        {
            var events = _input.Select(s => new Event(s)).OrderBy(s => s.EventDate).ToArray();

            string curGuard = "";
            var guards = new Dictionary<string, List<(DateTime, DateTime)>>();
            DateTime sleep = DateTime.MinValue;
            foreach (var evt in events)
            {
                if (evt.IsSignOn)
                {
                    curGuard = evt.GuardId;
                    if (!guards.ContainsKey(evt.GuardId))
                    {
                        guards.Add(evt.GuardId, new List<(DateTime, DateTime)>());
                    }
                }
                else if (evt.IsWakeUp)
                {
                    guards[curGuard].Add((sleep, evt.EventDate));
                }
                else if (evt.IsFallAsleep)
                {
                    sleep = evt.EventDate;
                }
            }

            var maxes = new Dictionary<string, int>();
            foreach (var g in guards)
            {
                if (!maxes.ContainsKey(g.Key))
                {
                    maxes.Add(g.Key, 0);
                }
                foreach (var time in g.Value)
                {
                    maxes[g.Key] += time.Item2.Subtract(time.Item1).Minutes;
                }
            }

            var max = 0;
            var currBest = "";
            foreach (var g in maxes)
            {
                if (g.Value > max)
                {
                    max = g.Value;
                    currBest = g.Key;
                }
            }

            var mins = new Dictionary<int, int>();
            foreach (var g in guards)
            {
                if (g.Key.Equals(currBest))
                {
                    foreach (var time in g.Value)
                    {
                        for (int i = time.Item1.Minute; i < time.Item2.Minute; i++)
                        {
                            if (!mins.ContainsKey(i))
                            {
                                mins.Add(i, 0);
                            }
                            mins[i]++;
                        }
                    }
                }
            }

            int curMax = 0;
            int curMin = 0;
            foreach (var m in mins)
            {
                if (m.Value > curMax)
                {
                    curMax = m.Value;
                    curMin = m.Key;
                }
            }
            return (int.Parse(currBest) * curMin).ToString();
        }

        public string Part2()
        {
            var events = _input.Select(s => new Event(s)).OrderBy(s => s.EventDate).ToArray();

            string curGuard = "";
            var guards = new Dictionary<string, List<(DateTime, DateTime)>>();
            DateTime sleep = DateTime.MinValue;
            foreach (var evt in events)
            {
                if (evt.IsSignOn)
                {
                    curGuard = evt.GuardId;
                    if (!guards.ContainsKey(evt.GuardId))
                    {
                        guards.Add(evt.GuardId, new List<(DateTime, DateTime)>());
                    }
                }
                else if (evt.IsWakeUp)
                {
                    guards[curGuard].Add((sleep, evt.EventDate));
                }
                else if (evt.IsFallAsleep)
                {
                    sleep = evt.EventDate;
                }
            }

            var maxes = new Dictionary<string, (int, int)>();
            foreach (var g in guards)
            {
                var mins = new Dictionary<int, int>();
                foreach (var time in g.Value)
                {
                    for (int i = time.Item1.Minute; i < time.Item2.Minute; i++)
                    {
                        if (!mins.ContainsKey(i))
                        {
                            mins.Add(i, 0);
                        }
                        mins[i]++;
                    }
                }
                int curMax = 0;
                int curMin = 0;
                foreach (var m in mins)
                {
                    if (m.Value > curMax)
                    {
                        curMax = m.Value;
                        curMin = m.Key;
                    }
                }
                maxes.Add(g.Key, (curMax, curMin));
            }

            var max = 0;
            var currBest = "";
            var currBestMin = 0;
            foreach (var g in maxes)
            {
                if (g.Value.Item1 > max)
                {
                    max = g.Value.Item1;
                    currBest = g.Key;
                    currBestMin = g.Value.Item2;
                }
            }
            return (int.Parse(currBest) * currBestMin).ToString();
        }

        private class Event
        {
            public Event(string logEntry)
            {
                string value = "";
                foreach (char c in logEntry)
                {
                    if (c == '[')
                    {
                        value = "";
                    }
                    else if (c == ']')
                    {
                        this.EventDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture.DateTimeFormat);
                    }
                    else if (c == '#')
                    {
                        value = "";
                    }
                    else if (c == 'b')
                    {
                        this.GuardId = value.Trim();
                        this.IsSignOn = true;
                    }
                    else if (c == 'f')
                    {
                        this.IsFallAsleep = true;
                        break;
                    }
                    else if (c == 'w')
                    {
                        this.IsWakeUp = true;
                        break;
                    }
                    else
                    {
                        value += c;
                    }
                }
            }

            public DateTime EventDate { get; }
            public string GuardId { get; }
            public bool IsSignOn { get; }
            public bool IsFallAsleep { get; }
            public bool IsWakeUp { get; }
        }
    }
}