using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDShared.Models;
using System.Reflection;

namespace RIPDApp.ViewModels;

[QueryProperty("Food", "Food")]
public partial class FoodDetailsVM : ObservableObject
{
  private readonly IDiaryService _diaryService;
  public FoodDetailsVM(IDiaryService diaryService)
  {
    _diaryService = diaryService;
  }

  [ObservableProperty]
  private bool _available;
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(FoodProperties))]
  private Food? _food;
  [ObservableProperty]
  private int _ammount;
  [ObservableProperty]
  private DateTime _acted;

  public IEnumerable<FoodProps?>? FoodProperties => GetProperties(Food).Result;

  private async Task<IEnumerable<FoodProps?>?> GetProperties(Food? food)
  {
    List<FoodProps?> props = new();
    if (food == null)
      return default;
    foreach (PropertyInfo prop in food.GetType().GetProperties())
    {
      props.Add(new FoodProps(prop.Name, prop?.GetValue(food)?.ToString()));
      /*      d.Add(prop.Name, "blank");*/
    }
    return props;
  }

  [RelayCommand]
  private async Task AddFoodToDiary()
  {
    bool success = false;
    Available = false;
    try
    {
      DiaryEntry_Food_Create createFoodEntry = new()
      {
        FoodId = Food?.Id,
        Amount = 0,
        Acted = Acted
      };
      success = await _diaryService.AddFoodToDiaryAsync(createFoodEntry);
    }
    catch (Exception ex)
    {

    }
    if (success) await GoBack();
    Available = true;
  }

  [RelayCommand]
  async Task GoBack() => await Shell.Current.GoToAsync("..");
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