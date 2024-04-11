using Battleships.Models;

namespace Battleships.Services;

public sealed class Game(
    IIOService iioService,
    IGameDirectorService gameDirectorService
) : IGame
{
    private readonly List<Position> _previousMoves = new();
    private readonly List<Ship> _ships = new();
    private Tile[,]? _tiles;

    public void CreateGameBoard(int size)
    {
        _tiles = new Tile[size, size];

        for (var y = 0; y < size; y++)
        for (var x = 0; x < size; x++)
            _tiles[x, y] = new Tile(new Position(x, y), false, null);

        iioService.Init(size, _tiles);
    }

    public void PlaceShips(int battleshipCount, int destroyerCount)
    {
        if (_tiles is null)
            throw new InvalidOperationException("Game has not been initialized");

        for (var i = 0; i < battleshipCount; i++)
            _ships.Add(gameDirectorService.PlaceShip(ref _tiles, ShipType.Battleship));

        for (var i = 0; i < destroyerCount; i++)
            _ships.Add(gameDirectorService.PlaceShip(ref _tiles, ShipType.Destroyer));

        iioService.Refresh(_tiles);
    }

    public void Play()
    {
        if (_tiles is null)
            throw new InvalidOperationException("Game has not been initialized");

        iioService.Render(_previousMoves, 0, false);

        while (!_ships.All(ship => ship.IsSunk))
        {
            var position = gameDirectorService.GetPlayerMove(
                _tiles.GetLength(0) - 1,
                _tiles.GetLength(1) - 1,
                _previousMoves);

            _tiles[position.X, position.Y] = _tiles[position.X, position.Y].Hit();
            _previousMoves.Add(position);

            var sunkShip = _tiles[position.X, position.Y].Ship?.IsSunk ?? false;

            iioService.Clear();
            iioService.Refresh(_tiles);
            iioService.Render(_previousMoves, _ships.Sum(s => s.Hits), sunkShip);
        }

        iioService.WinMessage();
    }
}
