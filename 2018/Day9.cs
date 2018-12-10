using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    internal partial class Day9 : IAdventChallenge
    {
        public string Name => "Initial Attempt";

        public int Year => 2018;

        public int Day => 9;

        public string Part1()
        {
            string input = _input;

            int numPlayers = Convert.ToInt32(input.Substring(0, input.IndexOf(' ')));
            var hPos = input.IndexOf('h') + 2;
            int maxValue = Convert.ToInt32(input.Substring(hPos, input.IndexOf("poi") - hPos));
            return GetBestScore(numPlayers, maxValue);
        }

        private string GetBestScore(int numPlayers, int maxValue)
        {
            var players = new int[numPlayers];

            var board = new List<int>
            {
                0
            };
            int p = 0;
            int m = 0;

            for (int i = 1; i < maxValue + 1; i++)
            {
                if (board.Count < 2)
                {
                    board.Add(i);
                    m = board.Count - 1;
                }
                else if (i % 23 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        m = SafeLeft(m, board);
                    }
                    players[p] += i;
                    players[p] += board[m];
                    board.RemoveAt(m);
                }
                else
                {
                    m = SafeRight(m, board);
                    m = SafeRight(m, board);
                    if (m == 0)
                    {
                        board.Add(i);
                        m = board.Count - 1;
                    }
                    else
                    {
                        board.Insert(m, i);
                    }
                }
                p++;
                if (p == players.Length)
                {
                    p = 0;
                }
            }
            return players.Max().ToString();
        }


        private int SafeLeft(int m, List<int> board)
        {
            m = m - 1;
            if (m < 0)
            {
                m = board.Count - 1;
            }
            return m;
        }

        private int SafeRight(int m, List<int> board)
        {
            m = m + 1;
            if (m >= board.Count)
            {
                m = 0;
            }
            return m;
        }

        public string Part2()
        {
            string input = _input;

            int numPlayers = Convert.ToInt32(input.Substring(0, input.IndexOf(' ')));
            var hPos = input.IndexOf('h') + 2;
            int maxValue = Convert.ToInt32(input.Substring(hPos, input.IndexOf("poi") - hPos));
            return GetBestScore(numPlayers, maxValue * 100);
        }

        private class Player
        {
        }
    }
}
