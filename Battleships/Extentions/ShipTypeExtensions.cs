using Battleships.Models;

namespace Battleships.Extentions;

public static class ShipTypeExtensions
{
    public static int ShipLength(this ShipType shipType)
    {
        return shipType switch
        {
            ShipType.Battleship => 5,
            ShipType.Destroyer => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(shipType), shipType, null)
        };
    }
}
