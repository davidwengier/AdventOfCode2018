namespace AdventOfCode
{
    internal interface IAdventChallenge
    {
        string Name { get; }
        int Year { get; }
        int Day { get; }

        string Part1();
        string Part2();
    }
}