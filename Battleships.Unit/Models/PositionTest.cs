using Battleships.Models;

namespace Battleships.Unit.Models;

public sealed class PositionTest
{
    public sealed class Move
    {
        [Fact]
        private void ShouldMoveUp()
        {
            // Arrange
            var position = new Position(1, 1);

            // Act
            var newPosition = position.Move(Direction.Up);

            // Assert
            Assert.Equal(new Position(1, 0), newPosition);
        }

        [Fact]
        private void ShouldMoveDown()
        {
            // Arrange
            var position = new Position(1, 1);

            // Act
            var newPosition = position.Move(Direction.Down);

            // Assert
            Assert.Equal(new Position(1, 2), newPosition);
        }

        [Fact]
        private void ShouldMoveLeft()
        {
            // Arrange
            var position = new Position(1, 1);

            // Act
            var newPosition = position.Move(Direction.Left);

            // Assert
            Assert.Equal(new Position(0, 1), newPosition);
        }

        [Fact]
        private void ShouldMoveRight()
        {
            // Arrange
            var position = new Position(1, 1);

            // Act
            var newPosition = position.Move(Direction.Right);

            // Assert
            Assert.Equal(new Position(2, 1), newPosition);
        }
    }

    public sealed class FromCoordinate
    {
        [Theory]
        [InlineData("A1", 1, 0)]
        [InlineData("a1", 1, 0)]
        [InlineData(" a1 ", 1, 0)]
        [InlineData(" a1", 1, 0)]
        [InlineData("a1 ", 1, 0)]
        [InlineData("b8", 8, 1)]
        [InlineData("B1", 1, 1)]
        [InlineData("B9", 9, 1)]
        [InlineData("B0", 0, 1)]
        [InlineData("B3", 3, 1)]
        [InlineData("J3", 3, 9)]
        [InlineData("j3", 3, 9)]
        private void ShouldCreateCoordinate(string coordinate, int expectedX, int expectedY)
        {
            // Arrange
            // Act
            var position = Position.FromCoordinate(coordinate, 9, 9);

            // Assert
            Assert.Equal(new Position(expectedX, expectedY), position);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("A-1")]
        [InlineData("K1")]
        private void ShouldNotCreateCoordinate(string coordinate)
        {
            // Arrange
            // Act
            var position = Position.FromCoordinate(coordinate, 9, 9);

            // Assert
            Assert.Null(position);
        }
    }
}
