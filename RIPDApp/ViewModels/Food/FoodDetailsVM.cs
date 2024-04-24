using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDShared.Models;
using System.Reflection;

namespace RIPDApp.ViewModels;

[QueryProperty("Food", "Food")]
public partial class FoodDetailsVM : ObservableObject
{
  public FoodDetailsVM()
  {
  }

  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(FoodProperties))]
  private Food? _food;

  public IEnumerable<FoodProps?>? FoodProperties => GetProperties(Food).Result;

  private async Task<IEnumerable<FoodProps?>?> GetProperties(Food? food)
  {
    List<FoodProps?> props = new();
    if (food == null) return default;
    foreach (PropertyInfo prop in food.GetType().GetProperties())
    {
      props.Add(new FoodProps(prop.Name, prop?.GetValue(food)?.ToString()));
      /*      d.Add(prop.Name, "blank");*/
    }
    return props;
  }
}

public class FoodProps
{
  public string Name { get; set; }
  public string? Value { get; set; }

  public FoodProps(string name, string? value)
  {
    Name = name;
    Value = value;
  }
}