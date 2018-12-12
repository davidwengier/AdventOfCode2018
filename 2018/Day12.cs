using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode._2018
{
    internal partial class Day12 : IAdventChallenge
    {
        public string Name => "Initial Attempt";

        public int Year => 2018;

        public int Day => 12;

        public string Part1()
        {
            var input = _input;

            var steps = new Steps(input);

            string state = steps.InitialState;
            for (int i = 0; i < 20; i++)
            {
                state = Process(state, steps);
            }

            int sum = 0;
            for (int i = 0; i < state.Length; i++)
            {
                if (state[i] == '#')
                {
                    sum += (i - steps.Start);
                }
            }
            return sum.ToString();
        }

        private string Process(string state, Steps steps)
        {
            var firstHash = state.IndexOf('#');
            if (firstHash < 5)
            {
                state = "....." + state;
                steps.Start += 5;
            }
            else
            {
                state = state.Substring(firstHash - 5);
                steps.Start = steps.Start - (firstHash - 5);
            }
            if (state.LastIndexOf('#') > (state.Length - 5))
            {
                state += ".....";
            }

            char[] chs = state.ToCharArray();

            for (int i = 0; i < chs.Length - 4; i++)
            {
                bool match = false;
                foreach (string t in steps.Transitions)
                {
                    if (t.Equals(state.Substring(i, 5), StringComparison.OrdinalIgnoreCase))
                    {
                        chs[i + 2] = '#';
                        match = true;
                        break;
                    }
                }
                if (!match)
                {
                    chs[i + 2] = '.';
                }
            }
            return new string(chs);
        }

        public string Part2()
        {
            var input = _input;

            var steps = new Steps(input);
            string state = steps.InitialState;
            for (long i = 0; i < 50000000000; i++)
            {
                state = Process(state, steps);
            }

            int sum = 0;
            for (int i = 0; i < state.Length; i++)
            {
                if (state[i] == '#')
                {
                    sum += (i - steps.Start);
                }
            }
            return sum.ToString();
        }

        private class Steps
        {
            public Steps(string[] input)
            {
                InitialState = input[0].Substring(15);

                for (int i = 2; i < input.Length; i++)
                {
                    if (input[i][9] == '#')
                    {
                        this.Transitions.Add(input[i].Substring(0, 5));
                    }
                }
            }

            public int Start = 0;
            public List<string> Transitions { get; } = new List<string>();
            public string InitialState { get; }
        }
    }
}
