using AutoMapper;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection;

namespace RIPDApp.Collections;

public class ObservablePropertyCollection<TObj> : ObservableCollection<Property> where TObj : class
{
  private readonly IMapper _mapper = new Mapper(_mapperConfig);
  private static readonly MapperConfiguration _mapperConfig = new(cfg =>
  {
  });

  public void Disassemble(TObj obj)
  {
    foreach (PropertyInfo propInfo in typeof(TObj).GetProperties())
    {
      Add(new() { Name = propInfo.Name, Value = propInfo.GetValue(obj) });
    }
  }

  public TObj? Assemble()
  {
    ExpandoObject expObj = new();
    TObj? retObj = default;
    foreach (Property prop in this)
    {
      expObj.TryAdd(prop.Name, prop.Value);
    }
    try
    {
      retObj = _mapper.Map<TObj?>(expObj);
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex);
    }
    return retObj;
  }
}

public class Property : INotifyPropertyChanged
{
  private string _name;
  public string Name
  {
    get => _name;
    set
    {
      _name = value;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
    }
  }

  private object? _value;
  public object? Value
  {
    get => _value;
    set
    {
      _value = value;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;
}