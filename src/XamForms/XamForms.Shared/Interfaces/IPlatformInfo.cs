using XamForms.Shared.Enums;

namespace XamForms.Shared.Interfaces
{
  public interface IPlatformInfo
  {
    /// <summary>
    /// Platform-specific path to user data, logs, etc. 
    /// </summary>
    string UserDataPath { get; }
    string AppVersion { get; }

    int DatabaseVersion { get; set; }

    string DeviceUniqueId { get; }

    string DeviceName { get; }

    /// <summary>
    /// iOS/Android
    /// </summary>
    OSType OSType { get; }

    /// <summary>
    /// Version of the OS, e.g. 'iOS 9.8.2' or 'Android 6.0.1 (API 23)'
    /// </summary>
    string OSVersionString { get; }

    /// <summary>
    /// Whether we're running on real hardware, or on
    /// an Emulator/Simulator
    /// </summary>
    bool IsRunningOnDevice { get; }

  }
}