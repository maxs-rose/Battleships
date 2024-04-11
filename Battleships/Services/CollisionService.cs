using Battleships.Extentions;
using Battleships.Models;

namespace Battleships.Services;

public sealed class CollisionService : ICollisionService
{
    public bool IsCollision(Tile[,] tiles, Position position)
    {
        return tiles[position.X, position.Y].IsShip;
    }

    public bool CanPlaceShip(Tile[,] tiles, ShipType shipType, Direction direction, int x, int y)
    {
        var clearTiles = 0;
        var maxX = tiles.GetLength(0);
        var maxY = tiles.GetLength(1);
        var position = new Position(x, y);

        while (!IsCollision(tiles, position))
        {
            clearTiles++;

            if (clearTiles == shipType.ShipLength())
                break;

            position = position.Move(direction);

            if (position.X >= 0 && position.X < maxX && position.Y >= 0 && position.Y < maxY)
                continue;

            clearTiles--;
            break;
        }

        return clearTiles == shipType.ShipLength();
    }
}
