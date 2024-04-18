using Microsoft.EntityFrameworkCore;
using RIPDApp.Models.ApiConnection;
using RIPDShared.Models;

namespace RIPDApp.DataBase;

public class LocalDBContext : DbContext
{
  public DbSet<ApiConnection> ApiConnections { get; set; }

  public DbSet<AppUser> Users { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    #region User Owner
    modelBuilder.Entity<AppUser>()
      .HasOne(e => e.Diary)
      .WithOne(e => e.Owner);
    modelBuilder.Entity<AppUser>()
      .HasMany(e => e.Following)
      .WithMany(e => e.Followers);
    modelBuilder.Entity<AppUser>()
      .HasMany(e => e.Followers)
      .WithMany(e => e.Following);
    #endregion User Owner
  }
}
