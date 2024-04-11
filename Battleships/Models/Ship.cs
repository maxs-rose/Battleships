using Battleships.Extentions;

namespace Battleships.Models;

public sealed class Ship(ShipType type)
{
    public ShipType Type { get; } = type;
    public int Hits { get; private set; }
    public bool IsSunk => Hits == Type.ShipLength();

    public void Hit()
    {
        if (Hits == Type.ShipLength())
            return;

        Hits++;
    }
}
