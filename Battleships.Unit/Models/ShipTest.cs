using Battleships.Extentions;
using Battleships.Models;

namespace Battleships.Unit.Models;

public sealed class ShipTest
{
    public sealed class IsSunk
    {
        [Fact]
        private void ShouldBeSunk()
        {
            // Arrange
            var ship = new Ship(ShipType.Battleship);

            // Act
            for (var i = 0; i < ship.Type.ShipLength(); i++)
                ship.Hit();

            // Assert
            Assert.True(ship.IsSunk);
        }


        [Fact]
        private void ShouldNotBeSunk()
        {
            // Arrange
            var ship = new Ship(ShipType.Battleship);

            // Act
            for (var i = 0; i < ship.Type.ShipLength() - 1; i++)
                ship.Hit();

            // Assert
            Assert.False(ship.IsSunk);
        }
    }


    public sealed class Hit
    {
        [Fact]
        private void ShouldUpdateHits()
        {
            // Arrange
            var ship = new Ship(ShipType.Battleship);

            // Act
            ship.Hit();

            // Assert
            Assert.Equal(1, ship.Hits);
        }


        [Fact]
        private void ShouldNotAllowTooManyHits()
        {
            // Arrange
            var ship = new Ship(ShipType.Battleship);

            // Act
            for (var i = 0; i <= ship.Type.ShipLength(); i++)
                ship.Hit();

            // Assert
            Assert.Equal(ship.Type.ShipLength(), ship.Hits);
        }
    }
}
