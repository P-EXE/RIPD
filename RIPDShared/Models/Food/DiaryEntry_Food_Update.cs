using Microsoft.EntityFrameworkCore;

namespace RIPDShared.Models;

public class DiaryEntry_Food_Update
{
  public double Amount { get; set; }
  public DateTime Consumed { get; set; }
  public DateTime Added { get; set; }
}
