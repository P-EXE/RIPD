using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RIPDShared.Models;

namespace RIPDApi.Data;

public class SQLDataBaseContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
  public DbSet<Diary> Diaries => Set<Diary>();
  public DbSet<Food> Foods => Set<Food>();
  public DbSet<DiaryEntry_Food> DiaryFoods => Set<DiaryEntry_Food>();
  public DbSet<Workout> Workouts => Set<Workout>();
  public DbSet<DiaryEntry_Workout> DiaryWorkouts => Set<DiaryEntry_Workout>();
  public DbSet<DiaryEntry_Run> DiaryRuns => Set<DiaryEntry_Run>();
  public DbSet<BodyMetric> BodyMetrics => Set<BodyMetric>();
  public DbSet<FitnessTarget> FitnessTargets => Set<FitnessTarget>();

  public SQLDataBaseContext(DbContextOptions<SQLDataBaseContext> options) : base(options)
  {
    Database.EnsureCreated();
  }

  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    base.OnConfiguring(options);
    options.EnableSensitiveDataLogging();
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    #region User
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
    #endregion User

    #region Diary
    builder.Entity<Diary>(d =>
    {
      d.HasKey(d => d.OwnerId);

      d.HasOne(d => d.Owner)
      .WithOne(u => u.Diary)
      .HasForeignKey<Diary>(d => d.OwnerId);

      d.HasMany(d => d.FoodEntries)
        .WithOne(f => f.Diary)
        .HasForeignKey(f => f.DiaryId)
        .OnDelete(DeleteBehavior.Cascade);

      d.HasMany(d => d.WorkoutEntries)
        .WithOne(w => w.Diary)
        .HasForeignKey(w => w.DiaryId)
        .OnDelete(DeleteBehavior.Cascade);

      d.HasMany(d => d.RunEntries)
        .WithOne(r => r.Diary)
        .HasForeignKey(r => r.DiaryId)
        .OnDelete(DeleteBehavior.Cascade);

      d.HasMany(d => d.BodyMetrics)
        .WithOne(b => b.Diary)
        .HasForeignKey(b => b.DiaryId)
        .OnDelete(DeleteBehavior.Cascade);

      d.HasMany(d => d.FitnessTargets)
        .WithOne(f => f.Diary)
        .HasForeignKey(f => f.DiaryId)
        .OnDelete(DeleteBehavior.Cascade);
    });
    #endregion Diary

    #region Things in Diary
    builder.Entity<DiaryEntry_Food>(fe =>
    {
      fe.HasKey(f => new { f.DiaryId, f.EntryNr });
      fe.Property(f => f.DiaryId).ValueGeneratedNever();
      fe.Property(f => f.EntryNr).ValueGeneratedNever();

      fe.HasOne(f => f.Food).WithMany()
      .HasForeignKey(f => f.FoodId)
      .OnDelete(DeleteBehavior.NoAction);
    });

    builder.Entity<DiaryEntry_Workout>(we =>
    {
      we.HasKey(w => new { w.DiaryId, w.EntryNr });
      we.Property(w => w.DiaryId).ValueGeneratedNever();
      we.Property(w => w.EntryNr).ValueGeneratedNever();

      we.HasOne(w => w.Workout).WithMany()
      .HasForeignKey(w => w.WorkoutId)
      .OnDelete(DeleteBehavior.NoAction);
    });

    builder.Entity<DiaryEntry_Run>(re =>
    {
      re.HasKey(r => new { r.DiaryId, r.EntryNr });
    });

    builder.Entity<BodyMetric>(b =>
    {
      b.HasKey(b => new { b.DiaryId, b.EntryNr });
      b.Property(b => b.DiaryId).ValueGeneratedNever();
      b.Property(b => b.EntryNr).ValueGeneratedNever();
    });

    builder.Entity<FitnessTarget>(ft =>
    {
      ft.HasKey(ft => new { ft.DiaryId, ft.EntryNr });
      ft.Property(ft => ft.DiaryId).ValueGeneratedNever();
      ft.Property(ft => ft.EntryNr).ValueGeneratedNever();
      ft.HasOne(ft => ft.StartBodyMetric).WithMany()
      .HasForeignKey(ft => new { ft.BodyMetricUser, ft.StartBodyMetricEntryNr })
      .OnDelete(DeleteBehavior.NoAction);
      ft.HasOne(ft => ft.GoalBodyMetric).WithMany()
      .HasForeignKey(ft => new { ft.BodyMetricUser, ft.GoalBodyMetricEntryNr })
      .OnDelete(DeleteBehavior.NoAction);
    });
    #endregion Things in Diary

    builder.Entity<Food>(f =>
    {
      f.HasOne(f => f.Manufacturer).WithMany(u => u.ManufacturedFoods)
      .HasForeignKey(f => f.ManufacturerId)
      .OnDelete(DeleteBehavior.NoAction);
      f.HasOne(f => f.Contributer).WithMany(u => u.ContributedFoods)
      .HasForeignKey(f => f.ContributerId)
      .OnDelete(DeleteBehavior.NoAction);
    });

    builder.Entity<Workout>(w =>
    {
      w.HasOne(w => w.Contributer).WithMany(u => u.ContributedWorkouts)
      .HasForeignKey(w => w.ContributerId)
      .OnDelete(DeleteBehavior.NoAction);
    });

    builder.Ignore<Run>();
  }
}
