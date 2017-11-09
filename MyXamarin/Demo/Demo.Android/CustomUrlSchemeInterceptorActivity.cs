using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using System;

namespace Demo.Droid
{
    using Helpers;

    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataSchemes = new[] { "com.googleusercontent.apps.788182042675-jvp07gj2lb2593mub7ip4mmviu42qibo" }, DataPath = "/oauth2redirect")]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Convert Android.Net.Url to Uri
            var uri = new Uri(Intent.Data.ToString());

            // Load redirectUrl page
            Settings._authenticator.OnPageLoading(uri);

            Finish();
        }
    }
}