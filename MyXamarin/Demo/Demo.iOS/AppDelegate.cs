﻿using Foundation;
using Google.Maps;
using Plugin.Toasts;
using System;
using UIKit;
using Xamarin.Forms;

namespace Demo.iOS
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
        const string Mapkey = "AIzaSyBPOzAeh9n3-PwBNy0dtW7-ZIBkgS0XB48";
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            MapServices.ProvideAPIKey(Mapkey);
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.FormsMaps.Init();

            DependencyService.Register<ToastNotification>(); // Register your dependency
            // If you are using Android you must pass through the activity
            ToastNotification.Init();

            global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();
            //init for Scanner barcode
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            // Convert NSUrl to Uri
            var uri = new Uri(url.AbsoluteString);

            // Load redirectUrl page
            Helpers.Settings._authenticator.OnPageLoading(uri);

            return true;
        }
    }
}
