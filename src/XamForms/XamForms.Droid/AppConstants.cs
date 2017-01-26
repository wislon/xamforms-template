namespace XamForms.Droid
{
  public class AppConstants
  {
    public const string TAG = "LGIA";

    /// <summary>
    /// ID for setting normal kinds of notifications in the status bar
    /// </summary>
    public const int DEFAULT_NOTIFICATION_ID = 2123;

    public const int DEBUG_NOTIFICATION_ID = DEFAULT_NOTIFICATION_ID - 1;

    /// <summary>
    /// ID for setting a notification for login required
    /// </summary>
    public const int LOGIN_REQUIRED_NOTIFICATION_ID = DEFAULT_NOTIFICATION_ID + 1;

    /// <summary>
    /// ID for setting a notification for device not supported
    /// </summary>
    public const int DEVICE_NOT_SUPPORTED_NOTIFICATION_ID = DEFAULT_NOTIFICATION_ID + 3;

    /// <summary>
    /// Because we haven't implemented Marshmallow yet
    /// </summary>
    public const string DEVICE_NOT_SUPPORTED_BECAUSE_NOUGAT = "Sorry, devices newer than Nougat aren't supported yet";

    /// <summary>
    /// Because they don't support BLE.
    /// </summary>
    public const string DEVICE_NOT_SUPPORTED_BECAUSE_KITKAT = "Devices older than KitKat are not supported";
  }
}