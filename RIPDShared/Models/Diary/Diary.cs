using Microsoft.EntityFrameworkCore;

namespace RIPDShared.Models;

public class Diary
{
  public Guid OwnerId { get; set; }
  public AppUser? Owner { get; set; }
  public ICollection<DiaryEntry_Food> FoodEntries = [];
  public ICollection<DiaryEntry_Workout> WorkoutEntries = [];
  public ICollection<DiaryEntry_Run> RunEntries = [];

  public ICollection<BodyMetric> BodyMetrics = [];
  public ICollection<FitnessTarget> FitnessTargets = [];
}
