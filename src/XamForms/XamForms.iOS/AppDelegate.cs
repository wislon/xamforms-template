using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Splat;
using UIKit;
using XamForms.UI;

namespace XamForms.iOS
{
  // The UIApplicationDelegate for the application. This class is responsible for launching the 
  // User Interface of the application, as well as listening (and optionally responding) to 
  // application events from iOS.
  [Register("AppDelegate")]
  public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IEnableLogger
  {
    //
    // This method is invoked when the application has loaded and is ready to run. In this 
    // method you should instantiate the window, load the UI into it and then make the window
    // visible.
    //
    // You have 17 seconds to return from this method, or iOS will terminate your application.
    //
    public override bool FinishedLaunching(UIApplication app, NSDictionary launchOptions)
    {
      // check for us being started by a location notification. If we did, it means we've been kicked off
      // either by geofence or beacon transition
      // bool startedByLocationNotificaton = launchOptions != null && launchOptions.ContainsKey(UIApplication.LaunchOptionsLocationKey);
      // this.Log().Debug($"Started by location/region notification: {startedByLocationNotificaton}");

      AppGlobal.Init();
      LogDeviceSpecifics();

      global::Xamarin.Forms.Forms.Init();
      LoadApplication(new App());

      return base.FinishedLaunching(app, launchOptions);
    }

    private void LogDeviceSpecifics()
    {
      this.Log().Debug(AppGlobal.DeviceSpecifics);
    }
  }
}
