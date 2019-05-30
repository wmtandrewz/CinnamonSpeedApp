using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Lottie.Forms.iOS.Renderers;
using UIKit;

namespace SpeedTest.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            AnimationViewRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();

            LoadApplication(new App());

            NSUserDefaults.StandardUserDefaults.RegisterDefaults(new NSDictionary("UserAgent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A"));

            return base.FinishedLaunching(app, options);
        }
    }
}
