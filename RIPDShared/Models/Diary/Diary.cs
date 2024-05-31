using Microsoft.EntityFrameworkCore;

namespace RIPDShared.Models;

[PrimaryKey(nameof(OwnerId))]
public class Diary
{
  public Guid OwnerId { get; set; }
  public AppUser Owner { get; set; }

  public ICollection<DiaryEntry_Food> FoodEntries = new HashSet<DiaryEntry_Food>();
  public ICollection<DiaryEntry_Workout> WorkoutEntries = new HashSet<DiaryEntry_Workout>();
  public ICollection<DiaryEntry_Run> RunEntries = new HashSet<DiaryEntry_Run>();
  
  public ICollection<BodyMetric> BodyMetrics = new HashSet<BodyMetric>();
  public ICollection<FitnessTarget> FitnessTargets = new HashSet<FitnessTarget>();
}
