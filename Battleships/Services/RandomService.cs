namespace Battleships.Services;

public sealed class RandomService : IRandomService
{
    private readonly Random _random = new();

    public int Next(int min, int max)
    {
        return _random.Next(min, max);
    }
}
