using System;
using game_store.Data;
using game_store.Dtos;
using game_store.Entities;

namespace game_store.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List <GameDto> games = [new(
        1,
        "Dishonored",
        "Stealth",
        21.0M,
        new DateOnly(2013, 1, 1)),
        new(
        2,
        "Shrek",
        "Adventure",
        15.0M,
        new DateOnly(2006, 9, 10)),
        new(
        3,
        "Hearts of iron",
        "Strategy",
        20.0M,
        new DateOnly(2015, 5, 11))];

        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {

            var group = app.MapGroup("games")
                    .WithParameterValidation();

            // Get games
            group.MapGet("/", () => games);

            // Get game
            group.MapGet("/{Id}", (int Id) => 
            {
                var game = games.Find(game => game.Id == Id);

                return game is null ? Results.NotFound() : Results.Ok(game);
            })
                .WithName(GetGameEndpointName);

            // POST games
            group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => 
            {
                Game game = new()
                {
                    Name = newGame.Name,
                    Genre = dbContext.Genres.Find(newGame.GenreId),
                    GenreId = newGame.GenreId,
                    Price = newGame.Price,
                    Release = newGame.Release
                };

                dbContext.Games.Add(game);
                dbContext.SaveChanges();

                GameDto gameDto = new(
                    game.Id,
                    game.Name,
                    game.Genre!.Name,
                    game.Price,
                    game.Release
                );

                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDto);
            });

            // Put games
            group.MapPut("/{Id}", (int Id, UpdateGameDto updatedGame) =>
            {
                var index = games.FindIndex(game => game.Id == Id);

                if (index ==-1)
                {
                    return Results.NotFound();
                }

                games[index] = new GameDto
                (
                    Id,
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.Release
                );

                return Results.NoContent();
            });

            // Delte
            group.MapDelete("/{id}", (int Id) => 
            {
                games.RemoveAll(game => game.Id == Id);

                return Results.NoContent();
            });

            return group;
        }
}
