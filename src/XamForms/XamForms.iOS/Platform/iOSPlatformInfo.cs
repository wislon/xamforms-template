using Foundation;
using ObjCRuntime;
using UIKit;
using XamForms.iOS.Helpers;
using XamForms.Shared.Enums;
using XamForms.Shared.Interfaces;

namespace XamForms.iOS.Platform
{
  public class iOSPlatformInfo : IPlatformInfo
  {
    private string _pathToUserData;
    private readonly string _appVersion;

    public string UserDataPath => _pathToUserData;
    public string AppVersion => _appVersion;

    public int DatabaseVersion { get; set; }

    public string DeviceUniqueId { get; }

    /// <summary>
    /// e.g. 'iPhone 6S Plus'
    /// Used when looking for device-specific type thresholds 
    /// during lane detection (among other things)
    /// </summary>
    public string DeviceName { get; }

    public OSType OSType { get; private set; }

    public bool IsRunningOnDevice { get; }

    public string OSVersionString { get; private set; }

    public iOSPlatformInfo()
    {
      SetAppDataDirectory();
      _appVersion = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();

      DeviceUniqueId = UIDevice.CurrentDevice.IdentifierForVendor.AsString();

      // https://github.com/dannycabrera/Get-iOS-Model, e.g. 'iPhone 6S Plus'
      DeviceName = new iOSHardware().GetModel(DeviceHardware.Version);

      var iosVersion = NSProcessInfo.ProcessInfo.OperatingSystemVersionString; //new Version(ObjCRuntime.Constants.Version);
      OSVersionString = $"iOS {iosVersion}";

      OSType = OSType.iOS;

      IsRunningOnDevice = Runtime.Arch != Arch.SIMULATOR;
    }


    private void SetAppDataDirectory()
    {
      _pathToUserData = System.IO.Path.Combine(NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.CachesDirectory, NSSearchPathDomain.User)[0].Path);
    }


  }
}