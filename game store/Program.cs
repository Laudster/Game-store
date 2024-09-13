using game_store.Data;
using game_store.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var constring = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(constring);

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();