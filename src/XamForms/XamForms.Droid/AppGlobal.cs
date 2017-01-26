using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Splat;
using XamForms.Droid.Platform;
using XamForms.Shared;
using XamForms.Shared.Interfaces;

namespace XamForms.Droid
{
  public static class AppGlobal
  {
    private static Context _context = Application.Context;
    private static bool _initialised;
    public static Context MainActivityContext { get; private set; }

    public static IPlatformInfo PlatformInfo { get; private set; }
    public static IPlatformFile DroidPlatformFile { get; private set; }
    public static IPlatformDirectory DroidPlatformDirectory { get; private set; }
    public static IPlatformNotification DroidPlatformNotification { get; private set; }

    public static string DeviceSpecifics { get; private set; }

    public static void Init(Context context)
    {
      if (_initialised)
      {
        return;
      }

      _context = context;

      // Android only, for Android N. Other platforms shouldn't need this.
      SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

      // this must be registered as early as possible because it contains
      // things like time zone -> api url calculations
      RegisterPlatformInfoImplementation();

      SharedNetwork.Init();

      // must be initialised before SharedConfig.
      SharedCache.Init();

      RegisterPlatformFileImplementation();
      RegisterPlatformDirectoryImplementation();
    }

    private static void RegisterPlatformInfoImplementation()
    {
      PlatformInfo = new DroidPlatformInfo();
      Locator.CurrentMutable.RegisterConstant(PlatformInfo, typeof(IPlatformInfo));
    }

    private static void RegisterPlatformFileImplementation()
    {
      DroidPlatformFile = new PlatformFile();
      Locator.CurrentMutable.RegisterConstant(DroidPlatformFile, typeof(IPlatformFile));
    }

    private static void RegisterPlatformDirectoryImplementation()
    {
      DroidPlatformDirectory = new PlatformDirectory();
      Locator.CurrentMutable.RegisterConstant(DroidPlatformDirectory, typeof(IPlatformDirectory));
    }


    private static string GetDeviceSpecifics()
    {
      var sb = new StringBuilder();

      var platformInfo = Locator.Current.GetService<IPlatformInfo>();

      sb.AppendLine("======= Device Details =========================================");
      sb.AppendFormat("======= OS Version:         \t{0}", Build.VERSION.Release).AppendLine();
      sb.AppendFormat("======= API Level:          \t{0}", Build.VERSION.Sdk).AppendLine();
      sb.AppendFormat("======= Device:             \t{0}", Build.Brand).AppendLine();
      sb.AppendFormat("======= Model:              \t{0}", Build.Model).AppendLine();
      sb.AppendFormat("======= Product:            \t{0}", Build.Display).AppendLine();
      sb.AppendFormat("======= App Version:        \t{0}", platformInfo.AppVersion).AppendLine();
      sb.AppendFormat("======= Database Version:   \t{0}", platformInfo.DatabaseVersion).AppendLine();
      sb.AppendLine();

      /*
      ======= OS Version:         	6.0.1
      ======= API Level:          	23
      ======= Device:             	google
      ======= Model:              	Nexus 5
      ======= Product:            	MOB30M
      ======= App Version:        	1.28
      ======= Database Version:   	29
      */
      return sb.ToString();
    }

    /// <summary>
    /// Apps >= Lollipop need an image with a transparent background, or you just get a square white block in the
    /// notification bar (it forces all colours which aren't transparent to be pure white):
    /// http://stackoverflow.com/questions/28387602/notification-bar-icon-turns-white-in-android-5-lollipop and
    /// http://stackoverflow.com/questions/30795431/icon-not-displaying-in-notification-white-square-shown-instead
    /// </summary>
    /// <param name="notificationBuilder"></param>
    /// <param name="preLollipopIconResourceId">Normal icon resource as you'd expect</param>
    /// <param name="postLollipopIconResourceId">Transparent icon resource (usually silhouette), or same version of icon but with transparent background</param>
    /// <returns></returns>
    public static int GetAppIconForSdkLevel(NotificationCompat.Builder notificationBuilder, int preLollipopIconResourceId, int postLollipopIconResourceId)
    {
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
      {
        //int color = 0x000088; // dark blue popup backgroundfor now
        //int color = 0xFFFFFF; // pure white popup backgroundfor now
        int color = 0x000022; // navy blue popup backgroundfor now
        notificationBuilder.SetColor(color);
        return postLollipopIconResourceId;

      }
      return preLollipopIconResourceId;
    }
  }
}