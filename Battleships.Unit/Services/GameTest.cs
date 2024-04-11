using Battleships.Models;
using Battleships.Services;
using NSubstitute;

namespace Battleships.Unit.Services;

public sealed class GameTest
{
    [Fact]
    private void PlaceShips()
    {
        // Arrange
        var mockIOService = Substitute.For<IIOService>();
        var mockGameDirectorService = Substitute.For<IGameDirectorService>();

        var sut = new Game(mockIOService, mockGameDirectorService);
        sut.CreateGameBoard(10);

        // Act
        sut.PlaceShips(1, 2);


        // Assert
        mockGameDirectorService.Received(1)
            .PlaceShip(ref Arg.Any<Tile[,]>(), ShipType.Battleship);

        mockGameDirectorService.Received(2)
            .PlaceShip(ref Arg.Any<Tile[,]>(), ShipType.Destroyer);

        mockIOService.Received().Refresh(Arg.Any<Tile[,]>());
    }
}
