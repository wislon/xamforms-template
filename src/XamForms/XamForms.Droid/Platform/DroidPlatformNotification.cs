using Android.App;
using Android.Content;
using Android.Media;
using Android.Net;
using Android.OS;
using Android.Support.V4.App;
using Splat;
using XamForms.Shared.Enums;
using XamForms.Shared.Interfaces;

namespace XamForms.Droid.Platform
{
  public class DroidPlatformNotification : IPlatformNotification
  {
    private bool _initialised;
    private static NotificationManager _notificationManager;

    public void Init()
    {
      if (!_initialised)
      {
        LogHost.Default.Debug("Initialising Android Platform Notification manager...");
        _initialised = true;
      }
    }

    public void SendNotification(string notificationMessage, AppNotificationType notificationType = AppNotificationType.Default)
    {
      int notificationId = AppConstants.DEFAULT_NOTIFICATION_ID;

      var intent = new Intent(Application.Context, typeof(MainActivity));

      switch (notificationType)
      {
        case AppNotificationType.LoginRequired:
          {
            LogHost.Default.Debug("User not logged in, sending notification.");
            notificationId = AppConstants.LOGIN_REQUIRED_NOTIFICATION_ID;
            break;
          }
        case AppNotificationType.DeviceNotSupported:
          {
            // this hasn't been instantiated yet - this.Log().Debug("Device not supported, sending notification.");
            notificationId = AppConstants.DEVICE_NOT_SUPPORTED_NOTIFICATION_ID;
            break;
          }
        default:
          break;
      }

      _notificationManager = _notificationManager ?? (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);

      var intentFlags = PendingIntentFlags.UpdateCurrent;
      // see https://stackoverflow.com/questions/21250364/notification-click-not-launch-the-given-activity-on-nexus-phones
      // tho it's not just nexus, it's most kitkat phones.
      if (Build.VERSION.SdkInt == BuildVersionCodes.Kitkat)
      {
        intentFlags |= PendingIntentFlags.OneShot;
      }

      var pendingIntent = PendingIntent.GetActivity(Application.Context, 0, intent, intentFlags);

      long[] pattern = new[] { 250L, 250L, 250L };

      var notificationSound = GetNotificationSound(notificationType);

      var notificationBuilder = new NotificationCompat.Builder(Application.Context)
        .SetContentTitle("XFTemplate")
        .SetContentText(notificationMessage)
        .SetAutoCancel(true)
        .SetPriority(5)
        .SetVibrate(pattern)
        .SetSound(notificationSound)
        .SetDefaults((int)NotificationDefaults.All);

      // if device isn't supported, we don't want the user to be able to go anywhere with it.
      // TODO redirect to something which will pop up a message saying WHY it's not supported.
      if (notificationType != AppNotificationType.DeviceNotSupported)
      {
        // oneshot works fine for notifications which dismiss automatically, but only
        // works the FIRST time when a persistent notification is used. However this
        // is enough to at least allow the user to log in the first time.
        notificationBuilder.SetContentIntent(pendingIntent);
      }

      //notificationBuilder.SetSmallIcon(AppGlobal.GetAppIconForSdkLevel(notificationBuilder, Resource.Drawable.notification_trans, Resource.Drawable.notification_trans));
      // TODO you'll need app-specific xyz-dpi icons for these notifications, this one below is just a placeholder
      notificationBuilder.SetSmallIcon(AppGlobal.GetAppIconForSdkLevel(notificationBuilder, Resource.Drawable.notification_template_icon_bg, Resource.Drawable.notification_template_icon_bg));
      var notification = notificationBuilder.Build();

      _notificationManager.Notify(notificationId, notification);
    }

    /// <summary>
    /// Used to close any notifications which may still be in the status bar when (for e.g.)
    /// the app is closed. This prevents possible unexpected app states if the notification 
    /// is tapped after the app has been destroyed.
    /// </summary>
    /// <param name="notificationIdType"></param>
    public static void CancelPendingNotificationsOfType(int notificationIdType)
    {
      _notificationManager = _notificationManager ?? (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
      _notificationManager.Cancel(notificationIdType);
    }

    private static Uri GetNotificationSound(AppNotificationType notificationType)
    {
      switch (notificationType)
      {
        default:
          {
            return RingtoneManager.GetDefaultUri(RingtoneType.Notification);
          }
      }
    }

  }
}