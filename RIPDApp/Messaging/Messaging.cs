using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RIPDApp.Messaging;
public class PageReturnObjectMessage<T> : ValueChangedMessage<T>
{
  public PageReturnObjectMessage(T value) : base(value) { }
}

