using Battleships.Models;

namespace Battleships.Services;

public interface IGameDirectorService
{
    Ship PlaceShip(ref Tile[,] tiles, ShipType type);
    Position GetPlayerMove(int maxX, int maxY, List<Position> previousMoves);
}
