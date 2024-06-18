using Microsoft.EntityFrameworkCore;
using RIPDApp.Models.ApiConnection;
using RIPDShared.Models;

namespace RIPDApp.DataBase;

public class LocalDBContext : DbContext
{
  public DbSet<AppUser> Users { get; set; }
  public DbSet<Diary> Diaries { get; set; }
  public DbSet<BearerToken> BearerTokens { get; set; }

  public LocalDBContext(DbContextOptions<LocalDBContext> options) : base(options)
  {
    Database.EnsureCreated();
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    #region User Owner
    builder.Entity<AppUser>(u =>
    {
      u.HasOne(u => u.Diary)
      .WithOne(d => d.Owner)
      .HasForeignKey<AppUser>(u => u.DiaryId);

      u.HasMany(u => u.ManufacturedFoods).WithOne(f => f.Manufacturer)
      .HasForeignKey(f => f.ManufacturerId)
      .OnDelete(DeleteBehavior.NoAction);
      u.HasMany(u => u.ContributedFoods).WithOne(f => f.Contributer)
      .HasForeignKey(f => f.ContributerId)
      .OnDelete(DeleteBehavior.NoAction);
    });

    builder.Entity<BearerToken>(b =>
    {
      b.HasKey(b => b.AccessToken);
    });
    #endregion User Owner

    builder.Ignore<Diary>();
  }
}
