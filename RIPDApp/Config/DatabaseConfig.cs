using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RIPDApp.DataBase;

namespace RIPDApp.Config;

public static class DatabaseConfig
{
  public static async Task RegisterSQLiteDatabase(this IServiceCollection services)
  {
    services.AddDbContext<LocalDBContext>(options =>
      options.UseSqlite(Statics.LocalDB.SQLiteConnection)
    );

    var context = services.BuildServiceProvider().GetRequiredService<LocalDBContext>();
    await context.Database.EnsureCreatedAsync();
  }
}
