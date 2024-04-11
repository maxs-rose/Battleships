using Battleships.Models;
using Battleships.Services;

namespace Battleships.Unit.Services;

public sealed class CollisionServiceTest
{
    public sealed class CanPlaceShip
    {
        [Theory]
        [InlineData(ShipType.Battleship, Direction.Up)]
        [InlineData(ShipType.Battleship, Direction.Down)]
        [InlineData(ShipType.Battleship, Direction.Left)]
        [InlineData(ShipType.Battleship, Direction.Right)]
        [InlineData(ShipType.Destroyer, Direction.Up)]
        [InlineData(ShipType.Destroyer, Direction.Down)]
        [InlineData(ShipType.Destroyer, Direction.Left)]
        [InlineData(ShipType.Destroyer, Direction.Right)]
        private void ShouldBeAbleToPlaceAShip(ShipType type, Direction direction)
        {
            // Arrange
            var tiles = CreateBoard(10);

            var sut = new CollisionService();

            // Act
            var canPlace = sut.CanPlaceShip(tiles, type, direction, 5, 5);

            // Assert
            Assert.True(canPlace);
        }

        [Fact]
        private void ShouldNotBeAbleToPlaceShipsOverlapping()
        {
            // Arrange
            var tiles = CreateBoard(10);
            tiles[0, 4] = new Tile(new Position(0, 4), false, new Ship(ShipType.Battleship));

            var sut = new CollisionService();

            // Act
            var canPlace = sut.CanPlaceShip(tiles, ShipType.Battleship, Direction.Down, 0, 3);

            // Assert
            Assert.False(canPlace);
        }

        [Theory]
        [InlineData(ShipType.Battleship, Direction.Up, 0, 0)]
        [InlineData(ShipType.Battleship, Direction.Down, 9, 9)]
        [InlineData(ShipType.Battleship, Direction.Left, 0, 1)]
        [InlineData(ShipType.Battleship, Direction.Right, 9, 0)]
        [InlineData(ShipType.Destroyer, Direction.Up, 0, 0)]
        [InlineData(ShipType.Destroyer, Direction.Down, 9, 9)]
        [InlineData(ShipType.Destroyer, Direction.Left, 0, 1)]
        [InlineData(ShipType.Destroyer, Direction.Right, 9, 0)]
        private void ShouldNotBeAbleToPlaceShipOffTheBoard(ShipType type, Direction direction, int x, int y)
        {
            // Arrange
            var tiles = CreateBoard(10);

            var sut = new CollisionService();

            // Act
            var canPlace = sut.CanPlaceShip(tiles, type, direction, x, y);

            // Assert
            Assert.False(canPlace);
        }

        private static Tile[,] CreateBoard(int size)
        {
            var tiles = new Tile[size, size];

            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
                tiles[x, y] = new Tile(new Position(x, y), false, null);

            return tiles;
        }
    }
}
