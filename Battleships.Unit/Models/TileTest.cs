using Battleships.Models;
using Spectre.Console.Testing;

namespace Battleships.Unit.Models;

public sealed class TileTest
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    private void IsShip(bool isShip)
    {
        // Arrange
        var tile = new Tile(new Position(0, 0), false, isShip ? new Ship(ShipType.Battleship) : null);

        // Act
        var result = tile.IsShip;

        // Assert
        Assert.Equal(isShip, result);
    }

    [Fact]
    private void Hit()
    {
        // Arrange
        var tile = new Tile(new Position(0, 0), false, default);

        // Act
        var hitTile = tile.Hit();

        // Assert
        Assert.True(hitTile.IsHit);
    }

    public sealed class Render
    {
        private readonly TestConsole _console = new();
        private readonly Position _position = new(0, 0);
        private readonly Ship _ship = new(ShipType.Battleship);

        [Fact]
        private void HitShip()
        {
            // Arrange
            var tile = new Tile(_position, true, _ship);

            // Act
            _console.Write(tile.Render());

            // Assert
            Assert.Equal("!", _console.Output.NormalizeLineEndings());
        }

        [Fact]
        private void HitNotShip()
        {
            // Arrange
            var tile = new Tile(_position, true, default);

            // Act
            _console.Write(tile.Render());

            // Assert
            Assert.Equal("X", _console.Output.NormalizeLineEndings());
        }

        [Fact]
        private void NotHitShipInNormalMode()
        {
            // Arrange
            var tile = new Tile(_position, false, _ship);

            // Act
            _console.Write(tile.Render());

            // Assert
            Assert.Equal("\u2248", _console.Output.NormalizeLineEndings());
        }

        [Fact]
        private void NotHitShipInDebugMode()
        {
            // Arrange
            var tile = new Tile(_position, false, _ship);

            // Act
            _console.Write(tile.Render(true));

            // Assert
            Assert.Equal("B", _console.Output.NormalizeLineEndings());
        }


        [Fact]
        private void Water()
        {
            // Arrange
            var tile = new Tile(_position, false, default);

            // Act
            _console.Write(tile.Render());

            // Assert
            Assert.Equal("\u2248", _console.Output.NormalizeLineEndings());
        }
    }
}
