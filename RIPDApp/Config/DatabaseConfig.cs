using Microsoft.EntityFrameworkCore;
using RIPDApp.DataBase;

namespace RIPDApp.Config;

public static class DatabaseConfig
{
  public static Task RegisterDatabases(this MauiAppBuilder builder)
  {
    RegisterSQLiteDBContext(builder);
    return Task.CompletedTask;
  }

  private static Task RegisterSQLiteDBContext(MauiAppBuilder builder)
  {
    builder.Services.AddDbContext<LocalDBContext>(options =>
      options.UseSqlite(Statics.LocalDB.SQLiteConnection)
    );

    return Task.CompletedTask;
  }
}
