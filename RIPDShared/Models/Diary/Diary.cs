using Microsoft.EntityFrameworkCore;

namespace RIPDShared.Models;

[PrimaryKey(nameof(OwnerId))]
public class Diary
{
  public Guid OwnerId { get; set; }
  public AppUser Owner { get; set; }

  public ICollection<Food_DiaryEntry> FoodEntries = new HashSet<Food_DiaryEntry>();
  public ICollection<Workout_DiaryEntry> WorkoutEntries = new HashSet<Workout_DiaryEntry>();
  public ICollection<Run_DiaryEntry> RunEntries = new HashSet<Run_DiaryEntry>();
  
  public ICollection<BodyMetric> BodyMetrics = new HashSet<BodyMetric>();
  public ICollection<FitnessTarget> FitnessTargets = new HashSet<FitnessTarget>();
}
