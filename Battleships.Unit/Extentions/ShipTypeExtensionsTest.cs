using Battleships.Extentions;
using Battleships.Models;

namespace Battleships.Unit.Extentions;

public sealed class ShipTypeExtensionsTest
{
    [Theory]
    [InlineData(ShipType.Battleship, 5)]
    [InlineData(ShipType.Destroyer, 4)]
    private void ShipLength(ShipType type, int length)
    {
        // Arrange
        // Act
        var result = type.ShipLength();

        // Assert
        Assert.Equal(length, result);
    }
}
