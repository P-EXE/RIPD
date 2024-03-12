using RIPD.Models;
using Microsoft.EntityFrameworkCore;
using RIPD.Models.ApiConnection;
using System;

namespace RIPD.DataServices
{
  public class LocalDBContext : DbContext
  {
    public DbSet<ApiConnection> ApiConnections { get; set; }

/*    public LocalDBContext(DbContextOptions<LocalDBContext> options) : base(options)
    {

    }*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      string db = "local.db";
      string connection = $"Filename={Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}{db}";
      optionsBuilder.UseSqlite(connection);
    }
  }
}
