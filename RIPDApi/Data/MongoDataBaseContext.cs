using Microsoft.EntityFrameworkCore;
using RIPDShared.Models;

namespace RIPDApi.Data;

public class MongoDataBaseContext : DbContext
{
  public DbSet<Run> Runs { get; set; }
  public MongoDataBaseContext(DbContextOptions<MongoDataBaseContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<Run>();
  }
}