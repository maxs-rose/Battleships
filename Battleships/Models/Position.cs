namespace Battleships.Models;

public sealed record Position(int X, int Y)
{
    public Position Move(Direction direction)
    {
        return direction switch
        {
            Direction.Up => this with { Y = Y - 1 },
            Direction.Down => this with { Y = Y + 1 },
            Direction.Left => this with { X = X - 1 },
            Direction.Right => this with { X = X + 1 },
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static Position? FromCoordinate(string coordinate, int maxX, int maxY)
    {
        var normalizedCoordinate = coordinate.ToUpperInvariant().Trim();

        if (normalizedCoordinate.Length != 2)
            return default;

        var x = normalizedCoordinate[1] - '0';
        var y = normalizedCoordinate[0] - 'A';

        if (x > maxX || x < 0 || y > maxY || y < 0)
            return default;

        return new Position(x, y);
    }

    public (string X, string Y) ToCoordinate()
    {
        return (X.ToString(), char.ConvertFromUtf32('A' + Y));
    }
}
