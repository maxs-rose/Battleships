using Battleships.Models;

namespace Battleships.Services;

public interface IIOService
{
    void Init(int size, Tile[,] tiles);
    void Refresh(Tile[,] tiles);
    void Clear();
    void Render(string message);
    void Render(List<Position> previousMoves, int hits, bool sunkShip);
    string AskInput(string message);
    public void WinMessage();
}
