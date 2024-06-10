using System.Reflection;
using System.Runtime.CompilerServices;

namespace RIPDApp.Tools;

#region Prop

/// <summary>
/// Deconstructs an object of the specified typ into a List of the type Prop.
/// Helps with dynamic creation of XAML CollectionView ItemsSources.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Props<T>
{
  // Type the Properties belong to.
  public T Type { get; private set; }
  // List of all Property members of the Type.
  public List<Prop> Members { get; private set; } = [];

  public Props(T obj)
  {
    Deconstruct(obj);
  }

  // Deconstructs the Object and only adds it's member Properties to the Members list if their Value is not null.
  public async Task Deconstruct(T obj, [CallerMemberName] string caller = "")
  {
    if (obj == null) return;
    foreach (PropertyInfo prop in obj.GetType().GetProperties())
    {
      if (prop.GetValue(obj) == null) break;
      Members.Add(new(prop.Name, prop.GetValue(obj).ToString()));
    }
  }
}

/// <summary>
/// Represents a Property of a Class.
/// </summary>
public class Prop
{
  public string Name { get; private set; }
  public string Value { get; private set; }
  public Prop(string name, string value)
  {
    Name = name;
    Value = value;
  }
}

#endregion Prop