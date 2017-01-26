using XamForms.Shared.Enums;

namespace XamForms.Shared.Interfaces
{
  public interface IPlatformNotification
  {
    void Init();

    /// <summary>
    /// For best results, on most platforms you need to invoke/marshall this
    /// call into the MAIN APP/UI thread. iOS spits the dummy if you don't, 
    /// Android doesn't, but then doesn't always show the notifications.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="type"></param>
    void SendNotification(string message, AppNotificationType type = AppNotificationType.Default);
  }
}


