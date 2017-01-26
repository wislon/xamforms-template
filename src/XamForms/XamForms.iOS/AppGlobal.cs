using System;
using System.Text;
using Splat;
using XamForms.iOS.Platform;
using XamForms.Shared;
using XamForms.Shared.Interfaces;

namespace XamForms.iOS
{
  public static class AppGlobal
  {
    private static bool _initialised;

    public static IPlatformInfo PlatformInfo { get; private set; }

    public static IPlatformNotification iOSPlatformNotification { get; private set; }

    public static IPlatformInfo PlatformInfo { get; private set; }
    public static IPlatformFile iOSPlatformFile { get; private set; }

    public static IPlatformDirectory iOSPlatformDirectory { get; private set; }

    public static string DeviceSpecifics { get; private set; }

    public static void Init()
    {
      if (_initialised)
      {
        return;
      }

      // this must be registered as early as possible because it contains
      // things like time zone -> api url calculations
      RegisterPlatformInfoImplementation();

      SharedNetwork.Init();

      // must be initialised before SharedConfig.
      SharedCache.Init();

      RegisterPlatformFileImplementation();
      RegisterPlatformDirectoryImplementation();

      RegisterPlatformNotificationImplementation();
    }

    private static void RegisterPlatformInfoImplementation()
    {
      PlatformInfo = new iOSPlatformInfo();
      Locator.CurrentMutable.RegisterConstant(PlatformInfo, typeof(IPlatformInfo));
    }

    private static void RegisterPlatformFileImplementation()
    {
      iOSPlatformFile = new PlatformFile();
      Locator.CurrentMutable.RegisterConstant(iOSPlatformFile, typeof(IPlatformFile));
    }

    private static void RegisterPlatformDirectoryImplementation()
    {
      iOSPlatformDirectory = new PlatformDirectory();
      Locator.CurrentMutable.RegisterConstant(iOSPlatformDirectory, typeof(IPlatformDirectory));
    }

    private static void RegisterPlatformNotificationImplementation()
    {
      if (iOSPlatformNotification != null) return;
      iOSPlatformNotification = new iOSPlatformNotification();
      iOSPlatformNotification.Init();
      Locator.CurrentMutable.RegisterConstant(iOSPlatformNotification, typeof(IPlatformNotification));
    }

    private static string GetDeviceSpecifics()
    {
      var version = new Version(ObjCRuntime.Constants.Version);

      var platformInfo = Locator.Current.GetService<IPlatformInfo>();

      var sb = new StringBuilder();
      sb.AppendLine("======= Device Details =========================================");
      sb.AppendFormat("======= Hardware:         \t{0}", platformInfo.DeviceName).AppendLine();
      sb.AppendFormat("======= OS Version:       \t{0}", version).AppendLine();
      sb.AppendFormat("======= App Version:      \t{0}", platformInfo.AppVersion).AppendLine();
      sb.AppendFormat("======= Database Version: \t{0}", platformInfo.DatabaseVersion).AppendLine();
      sb.AppendLine();

      /*
      ======= Hardware:         	iPhone 6 Plus
      ======= OS Version:       	9.6.2
      ======= App Version:      	1.28
      ======= Database Version: 	27
     */

      return sb.ToString();
    }

  }
}