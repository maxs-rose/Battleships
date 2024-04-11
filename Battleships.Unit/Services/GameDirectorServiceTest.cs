using Battleships.Extentions;
using Battleships.Models;
using Battleships.Services;
using NSubstitute;

namespace Battleships.Unit.Services;

public sealed class GameDirectorServiceTest
{
    [Fact]
    private void PlaceShip()
    {
        // Arrange
        var mockCollisionService = Substitute.For<ICollisionService>();
        var mockRandomService = Substitute.For<IRandomService>();

        mockCollisionService
            .CanPlaceShip(Arg.Any<Tile[,]>(), ShipType.Battleship, Direction.Down, Arg.Any<int>(), Arg.Any<int>())
            .Returns(true);

        mockRandomService
            .Next(Arg.Any<int>(), Arg.Any<int>())
            .ReturnsForAnyArgs(1);

        var size = 10;
        var tiles = new Tile[size, size];

        for (var x = 0; x < size; x++)
        for (var y = 0; y < size; y++)
            tiles[x, y] = new Tile(new Position(x, y), false, null);

        var sut = new GameDirectorService(
            mockCollisionService,
            Substitute.For<IIOService>(),
            mockRandomService);


        // Act
        var placedShip = sut.PlaceShip(ref tiles, ShipType.Battleship);

        // Assert
        Assert.NotNull(placedShip);
        Assert.Equal(ShipType.Battleship.ShipLength(), GetShipTiles(tiles, size));
    }

    private int GetShipTiles(Tile[,] tiles, int size)
    {
        var count = 0;

        foreach (var tile in tiles)
            if (tile.IsShip)
                count++;

        return count;
    }

    [Fact]
    private void GetPlayerMove()
    {
        // Arrange
        var mockIOService = Substitute.For<IIOService>();

        mockIOService
            .AskInput(Arg.Any<string>())
            .ReturnsForAnyArgs("A5");

        var sut = new GameDirectorService(
            Substitute.For<ICollisionService>(),
            mockIOService,
            Substitute.For<IRandomService>());

        // Act
        var positon = sut.GetPlayerMove(5, 5, []);

        // Assert
        Assert.Equal(new Position(5, 0), positon);
    }
}
