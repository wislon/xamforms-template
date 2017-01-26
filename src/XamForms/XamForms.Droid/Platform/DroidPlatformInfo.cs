using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using XamForms.Shared.Enums;
using XamForms.Shared.Interfaces;

namespace XamForms.Droid.Platform
{
  public class DroidPlatformInfo : IPlatformInfo
  {
    private static readonly Context _context = Application.Context;

    private string _appDataDirectory;

    public string UserDataPath => _appDataDirectory;

    public string AppVersion { get; }

    public int DatabaseVersion { get; set; }

    public string DeviceUniqueId { get; private set; }

    public string DeviceName { get; }

    public OSType OSType { get; private set; }
    public string OSVersionString { get; private set; }

    public bool IsRunningOnDevice { get; }

    public DroidPlatformInfo()
    {
      SetAppDataDirectory();
      AppVersion = _context.ApplicationContext.PackageManager.GetPackageInfo(_context.PackageName, 0).VersionName;
      DeviceUniqueId = Settings.Secure.GetString(_context.ContentResolver, "android_id");

      // e.g. 'google Nexus 5' (case sensitive, watch out)
      DeviceName = $"{Build.Brand} {Build.Model}";

      OSVersionString = $"Android {Build.VERSION.Release} (API {Build.VERSION.Sdk})";

      OSType = OSType.Android;

      string[] emulators = {"vbox", "generic", "vsemu"};

      IsRunningOnDevice = emulators.Contains(Build.Fingerprint);
    }

    private void SetAppDataDirectory()
    {
      _appDataDirectory = Application.Context.GetExternalFilesDir(null).AbsolutePath;
#if (!DEBUG)
      _appDataDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
#endif
    }

  }

}