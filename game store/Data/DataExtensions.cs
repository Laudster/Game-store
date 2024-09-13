using System;
using Microsoft.EntityFrameworkCore;

namespace game_store.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbcontext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbcontext.Database.Migrate();
    }
}
