using Battleships.Models;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Battleships.Services;

public sealed class IOService(
    IAnsiConsole console,
    bool debug = false
) : IIOService
{
    private Table? _table;

    public void Init(int size, Tile[,] tiles)
    {
        _table = new Table();
        _table.AddColumns(Enumerable.Range(0, size + 1).Select(_ => "").ToArray());
        _table.HideHeaders();
        _table.Border = TableBorder.Simple;

        _table.AddRow([
            "",
            ..Enumerable.Range(0, size).Select(i => $"[{i + 1}]{char.ConvertFromUtf32('A' + i)}[/]")
        ]);

        for (var y = 0; y < size; y++)
            _table.AddRow([$"[{y + 1}]{y}[/]", ..Enumerable.Range(0, size).Select(i => "")]);

        Refresh(tiles);
    }

    public void Refresh(Tile[,] tiles)
    {
        if (_table is null)
            throw new InvalidOperationException("Game board has not been created");

        foreach (var tile in tiles)
        {
            var (x, y) = tile.Position;
            _table.UpdateCell(x + 1, y + 1, tile.Render(debug));
        }
    }

    public string AskInput(string message)
    {
        return console.Ask<string>(message);
    }

    public void Clear()
    {
        console.Clear();
    }

    public void WinMessage()
    {
        Render("[bold green]Congratulations! You have sunk all the ships![/]");
    }

    public void Render(string message)
    {
        console.MarkupLine(message);
    }

    public void Render(List<Position> previousMoves, int hits, bool sunkShip)
    {
        if (_table is null)
            throw new InvalidOperationException("Game board has not been created");

        var cols = new Grid();
        cols.AddColumns(2);
        cols.AddRow(
            new Panel(_table)
            {
                Header = new PanelHeader("[bold white]Battleships![/]")
            },
            BuildInfoPanel(previousMoves, hits));

        console.Write(cols);

        if (sunkShip)
            console.MarkupLine("[bold maroon]You sunk my battleship![/]");
    }

    private IRenderable BuildInfoPanel(List<Position> previousMoves, int hits)
    {
        if (previousMoves.Count == 0)
            return new Panel(new Rows([
                new Markup("     "),
                new Markup($"Hits: [bold green]{hits}[/]"),
                new Markup("     ")
            ]))
            {
                Header = new PanelHeader("Moves")
            };

        var moves = previousMoves
            .Select(move =>
            {
                var (x, y) = move.ToCoordinate();
                return $"[{move.Y + 1}]{y}[/][{move.X + 1}]{x}[/]";
            })
            .Reverse()
            .Take(10)
            .Select(move => new Markup(move));

        return new Panel(new Rows([
            new Markup("     "),
            new Markup($"Hits: [bold green]{hits}[/]"),
            ..moves,
            new Markup("     ")
        ]))
        {
            Header = new PanelHeader("Moves")
        };
    }
}
