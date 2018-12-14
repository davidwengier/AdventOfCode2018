using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode._2018
{
    internal partial class Day13 : IAdventChallenge
    {
        public string Name => "Initial Attempt";

        public int Year => 2018;

        public int Day => 13;

        public string Part1()
        {
            List<int> state = new List<int>
            {
                3, 7
            };
            int one = 0;
            int two = 1;

            while (state.Count < _input + 11)
            {
                int next = state[one] + state[two];
                if (next > 9)
                {
                    state.Add(1);
                }
                state.Add(next % 10);

                one = Move(one, state[one] + 1, state.Count);
                two = Move(two, state[two] + 1, state.Count);
            }
            return string.Join("", state.GetRange(_input, 10));
        }

        private int Move(int start, int offset, int length)
        {
            int newIndex = start + offset;
            return (newIndex + length) % length;
        }

        public string Part2()
        {
            string input = _input.ToString();

            List<int> state = new List<int>
            {
                3, 7
            };
            int one = 0;
            int two = 1;

            while (true)
            {
                int next = state[one] + state[two];
                if (next > 9)
                {
                    state.Add(1);

                    if (state.Count > input.Length)
                    {
                        var match = IsMatch(state, input);
                        if (match)
                        {
                            return (state.Count - input.Length).ToString();
                        }
                    }
                }
                state.Add(next % 10);

                one = Move(one, state[one] + 1, state.Count);
                two = Move(two, state[two] + 1, state.Count);

                if (state.Count > input.Length)
                {
                    var match = IsMatch(state, input);
                    if (match)
                    {
                        return (state.Count - input.Length).ToString();
                    }
                }
            }
        }

        private static bool IsMatch(List<int> state, string input)
        {
            bool match = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (((int)(input[i] - '0')) != state[state.Count - (input.Length - i)])
                {
                    match = false;
                    break;
                }
            }

            return match;
        }
    }
}
