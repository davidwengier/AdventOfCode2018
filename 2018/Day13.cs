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

                //Output(state, one, two);
                if (state.Count % 100 ==0)
                {
                    Console.WriteLine(state.Count);
                }
            }
            return string.Join("", state.GetRange(_input, 10));
        }

        private void Output(List<int> state, int one, int two)
        {
            for (int i = 0; i < state.Count; i++)
            {
                if (one == i)
                {
                    Console.Write('(');
                }
                else if (two == i)
                {
                    Console.Write('[');
                }
                else
                {
                    Console.Write(' ');
                }
                Console.Write(state[i]);
                if (one == i)
                {
                    Console.Write(')');
                }
                else if (two == i)
                {
                    Console.Write(']');
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine();
        }

        private int Move(int start, int offset, int length)
        {
            int newIndex = start + offset;
            return (newIndex + length) % length;
        }

        public string Part2()
        {
            throw new NotImplementedException();
        }
    }
}
