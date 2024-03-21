using RIPD.Models;
using Microsoft.EntityFrameworkCore;
using RIPD.Models.ApiConnection;

namespace RIPD.DataBase;

public class LocalDBContext : DbContext
{
    public DbSet<ApiConnection> ApiConnections { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Owner> Owner { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string db = "local.db";
        string connection = $"Filename={Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}{db}";
        optionsBuilder.UseSqlite(connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region User Owner
        modelBuilder.Entity<User>()
          .HasOne(e => e.Diary)
          .WithOne(e => e.User);
        modelBuilder.Entity<User>()
          .HasMany(e => e.Following)
          .WithMany(e => e.Followers);
        modelBuilder.Entity<User>()
          .HasMany(e => e.Followers)
          .WithMany(e => e.Following);

        modelBuilder.Entity<Owner>()
          .HasOne(e => e.Diary)
          .WithOne(e => e.User as Owner);
        modelBuilder.Entity<Owner>()
          .HasMany(e => e.Following);
        modelBuilder.Entity<Owner>()
          .HasMany(e => e.Followers);
        #endregion User Owner
    }
}
