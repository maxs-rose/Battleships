namespace Battleships.Services;

public interface IGame
{
    void CreateGameBoard(int size);
    void PlaceShips(int battleshipCount, int destroyerCount);
    void Play();
}
