using System;
using Foundation;
using Splat;
using UIKit;
using Xamarin.Forms;
using XamForms.Shared.Enums;
using XamForms.Shared.Interfaces;

namespace XamForms.iOS.Platform
{
  public class iOSPlatformNotification : IPlatformNotification, IEnableLogger
  {
    private static bool _initialised;

    public void Init()
    {
      if (!_initialised)
      {
        UIApplication.SharedApplication.RegisterUserNotificationSettings(
          UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Sound, new NSSet()));
        _initialised = true;
      }
    }

    public void SendNotification(string message, AppNotificationType type = AppNotificationType.Default)
    {
      bool allowDisplay = true;
      switch (type)
      {
        case AppNotificationType.Default:
        {
            // TODO - put in system settings for this in settings.bundle/root.plist
          if (!NSUserDefaults.StandardUserDefaults.BoolForKey("XFTemplateNotification") || 
               NSUserDefaults.StandardUserDefaults.ValueForKey(new NSString("XFTemplateNotification")) == null)
          {
            allowDisplay = false;
          }
          break;
        }
        default:
        {
          break;
        }
      }

      if (allowDisplay)
      {

        // need to run this on the main ui thread or we never get the notification
        // (not even in the notification bar/list)
        this.Log().Debug($"Sending notification of '{message}' (Notification Type: {type})");
        Device.BeginInvokeOnMainThread(() =>
        {
          var notification = new UILocalNotification
          {
            AlertBody = message,
            SoundName = UILocalNotification.DefaultSoundName
          };
          UIApplication.SharedApplication.PresentLocalNotificationNow(notification);
#if DEBUG
          //UserDialogs.Instance.Toast(message, TimeSpan.FromMilliseconds(2000));
#endif
        });
      }
    }

  }
}