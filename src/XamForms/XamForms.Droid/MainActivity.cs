using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Splat;
using XamForms.Shared.Enums;
using XamForms.Shared.Interfaces;
using XamForms.UI;
using XamForms.UI.Interfaces;
using XamForms.UI.Navigation;

namespace XamForms.Droid
{
  [Activity(Label = "XamForms", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IEnableLogger
  {
    private bool _notSupported;
    private IPlatformNotification _notifier;

    protected override void OnCreate(Bundle bundle)
    {
      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;

      base.OnCreate(bundle);

      global::Xamarin.Forms.Forms.Init(this, bundle);
    }

    protected override void OnPostCreate(Bundle savedInstanceState)
    {
      base.OnPostCreate(savedInstanceState);

      AppGlobal.Init(this);
      this.Log().Debug(AppGlobal.DeviceSpecifics);

      _notifier = Locator.Current.GetService<IPlatformNotification>();

      string message = CheckIfDeviceIsSupported();

      if (!string.IsNullOrWhiteSpace(message))
      {
        this.Log().Warn($"This device isn't supported because: {message}");
        _notSupported = true;
      }


      if (_notSupported)
      {
        _notifier.SendNotification("Sorry, your device isn't supported (yet).", AppNotificationType.DeviceNotSupported);
        this.MoveTaskToBack(true);
        this.Finish();
        return;
      }

      var navService = new NavigationService();
      navService.RegisterViewModels(typeof(MasterDetailRootPage).Assembly);

      Locator.CurrentMutable.RegisterConstant(navService, typeof(INavigationService));

      LoadApplication(new App());
    }

    /// <summary>
    /// Check if device supports anything else you need e.g. BLE) or it may not work either.<br/>
    /// This is not for checking permissions, it's for checking if the hardware 
    /// supports what you need it to do.<br/>
    /// If not supported, returns a string saying why.<br/>
    /// If supported, result is empty.<br/>
    /// </summary>
    /// <returns></returns>
    private string CheckIfDeviceIsSupported()
    {
      if (Build.VERSION.SdkInt < BuildVersionCodes.Kitkat)
      {
        return AppConstants.DEVICE_NOT_SUPPORTED_BECAUSE_KITKAT;
      }

      // jerry-rig build version check for Nougat 7.1.1 (Nougat version code 25 not fully released yet, 
      // when it is, change it to 'N' or whatever they're using). .M+2 = 25 (Nougat 7.1.1)
      if ((int)Build.VERSION.SdkInt > (int)BuildVersionCodes.M + 2)
      {
        return AppConstants.DEVICE_NOT_SUPPORTED_BECAUSE_NOUGAT;
      }

      return string.Empty;
    }

  }

}

