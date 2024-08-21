using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RIPDShared.Models;

public class AppUser : IdentityUser<Guid>, INotifyPropertyChanged
{
  public event PropertyChangedEventHandler? PropertyChanged;
  private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  public override string? UserName
  {
    get => base.UserName;
    set
    {
      if (base.UserName != value)
      {
        base.UserName = value;
        NotifyPropertyChanged();
      }
    }
  }

  public override string? Email
  {
    get => base.Email;
    set
    {
      if (base.Email != value)
      {
        base.Email = value;
        NotifyPropertyChanged();
      }
    }
  }

  public ICollection<Food>? ContributedFoods = new ObservableCollection<Food>();
  public ICollection<Food>? ManufacturedFoods = new ObservableCollection<Food>();
  public ICollection<Workout>? ContributedWorkouts = new ObservableCollection<Workout>();
  public ICollection<AppUser>? Following = new ObservableCollection<AppUser>();
  public ICollection<AppUser>? Followers = new ObservableCollection<AppUser>();
  public Guid DiaryId { get; set; }
  public Diary? Diary { get; set; } = new();
}
