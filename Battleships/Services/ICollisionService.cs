using Battleships.Models;

namespace Battleships.Services;

public interface ICollisionService
{
    bool IsCollision(Tile[,] tiles, Position position);
    bool CanPlaceShip(Tile[,] tiles, ShipType shipType, Direction direction, int x, int y);
}
