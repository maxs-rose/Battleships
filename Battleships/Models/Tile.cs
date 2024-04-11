using Spectre.Console;

namespace Battleships.Models;

public sealed record Tile(Position Position, bool IsHit, Ship? Ship)
{
    public bool IsShip => Ship is not null;

    public Markup Render(bool debug = false)
    {
        var text = this switch
        {
            { IsHit: true, Ship: not null } => "[bold red]![/]",
            { IsHit: true } => "[maroon]X[/]",
            { IsShip: true, Ship: not null } => debug
                ? $"[green]{Ship.Type.ToString()[0]}[/]"
                : "[navyblue]\u2248[/]",
            _ => "[navyblue]\u2248[/]"
        };

        return new Markup(text);
    }

    public Tile Hit()
    {
        Ship?.Hit();
        return this with { IsHit = true };
    }
}
