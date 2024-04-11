using Battleships.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var services = new ServiceCollection();

services.AddTransient<IRandomService, RandomService>();

services.AddSingleton(AnsiConsole.Console);
services.AddSingleton<IIOService, IOService>();
services.AddSingleton<ICollisionService, CollisionService>();
services.AddSingleton<IGameDirectorService, GameDirectorService>();
services.AddSingleton<IGame, Game>();

using var serviceProvider = services.BuildServiceProvider();

var game = serviceProvider.GetRequiredService<IGame>();

game.CreateGameBoard(10);
game.PlaceShips(1, 2);
game.Play();
