using System.Reflection;
using System.Runtime.CompilerServices;

namespace RIPDApp.Tools;

#region Prop

/// <summary>
/// Deconstructs an object of the specified typ into a List of the target Prop.
/// Helps with dynamic creation of XAML CollectionView ItemsSources.
/// </summary>
public class Props
{
  // Type the Properties belong to.
  public Type objT { get; private set; }
  // List of all Property members of the Type.
  public List<Prop> Members { get; private set; } = [];

  // Get all Props of a Type
  public Props(Type type)
  {
    objT = type;
    foreach (PropertyInfo propInfo in type.GetProperties())
    {
      Members.Add(new(propInfo.Name, ""));
    }
  }

  // Get all Props of a Type with a String mask
  public Props(Type type, List<string> omitt)
  {
    objT = type;
    foreach (PropertyInfo propInfo in type.GetProperties())
    {
      if (omitt.Contains(propInfo.Name))
        continue;
      Members.Add(new(propInfo.Name, ""));
    }
  }

  // Get all Props of a Type with a null selector
  public Props(object target, bool allowNullValues = true)
  {
    objT = target.GetType();
    foreach (PropertyInfo propInfo in objT.GetProperties())
    {
      if (!allowNullValues)
      {
        if (propInfo.GetValue(target) == null)
          continue;
      }
      Members.Add(new(propInfo.Name, propInfo.GetValue(target)?.ToString()));
    }
  }

  // Get all Props of an Object with a Type as a mask with a null selector
  public Props(object target, Type maskType, bool allowNullValues = true)
  {
    objT = target.GetType();
    List<PropertyInfo> maskList = maskType.GetProperties().ToList();
    foreach (PropertyInfo propInfo in objT.GetProperties())
    {
      if (maskList.Contains(propInfo))
        continue;
      if (!allowNullValues)
      {
        if (propInfo.GetValue(target) == null)
          continue;
      }
      Members.Add(new(propInfo.Name, propInfo.GetValue(target)?.ToString()));
    }
  }

  public T Recombine<T>()
  {
    Dictionary<string, object> props = [];
    foreach (Prop prop in Members)
    {
      props.Add(prop.Name, prop.Value);
    }
    dynamic? combined = Activator.CreateInstance(objT, [props]);
    return (T)combined;
  }
}

/// <summary>
/// Represents a Property of a Class.
/// </summary>
public class Prop
{
  public string Name { get; private set; }
  public string? Value { get; private set; }
  public Prop(string name, string? value)
  {
    Name = name;
    Value = value;
  }
}

#endregion Prop