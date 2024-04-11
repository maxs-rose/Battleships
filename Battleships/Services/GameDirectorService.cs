using Battleships.Extentions;
using Battleships.Models;

namespace Battleships.Services;

public sealed class GameDirectorService(
    ICollisionService collisionService,
    IIOService ioService,
    IRandomService randomService
) : IGameDirectorService
{
    public Ship PlaceShip(ref Tile[,] tiles, ShipType shipType)
    {
        var directions = Enum.GetValues<Direction>();

        var x = randomService.Next(0, tiles.GetLength(0));
        var y = randomService.Next(0, tiles.GetLength(1));
        var direction = directions[randomService.Next(0, directions.Length)];

        while (!collisionService.CanPlaceShip(tiles, shipType, direction, x, y))
        {
            x = randomService.Next(0, tiles.GetLength(0));
            y = randomService.Next(0, tiles.GetLength(1));
            direction = directions[randomService.Next(0, directions.Length)];
        }

        var ship = new Ship(shipType);
        var position = new Position(x, y);

        for (var i = 0; i < shipType.ShipLength(); i++)
        {
            tiles[position.X, position.Y] = new Tile(position, false, ship);
            position = position.Move(direction);
        }

        return ship;
    }

    public Position GetPlayerMove(int maxX, int maxY, List<Position> previousMoves)
    {
        getInput:
        var coordinate = ioService.AskInput("Please enter coordinate to fire at! (e.g. [1]A[/][5]5[/])");

        var position = Position.FromCoordinate(coordinate, maxX, maxY);

        if (position is not null && !previousMoves.Contains(position))
            return position;

        ioService.Render($"{coordinate} is not valid.");

        goto getInput;
    }
}
