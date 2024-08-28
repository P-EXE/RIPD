using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RIPDApp.Messaging;
public class PageReturnObjectMessage<T> : ValueChangedMessage<T>
{
  public PageReturnObjectMessage(T value) : base(value) { }
}

public class FoodCreatePageBarcodeResponse<T> : ValueChangedMessage<T>
{
  public FoodCreatePageBarcodeResponse(T value) : base(value) { }
}

