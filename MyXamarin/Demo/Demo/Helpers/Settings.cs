using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using Xamarin.Auth;

namespace Demo.Helpers
{
    /// <summary>
    /// Settings
    /// </summary>
    public static class Settings
    {
        #region -- Properties --

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        /// <summary>
        /// Access token
        /// </summary>
        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        /// <summary>
        /// User
        /// </summary>
        public static string User
        {
            get
            {
                return AppSettings.GetValueOrDefault("User", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("User", value);
            }
        }

        /// <summary>
        /// Password
        /// </summary>
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }

        public static DateTime AccessTokenExpirationDate
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessTokenExpirationDate", DateTime.UtcNow);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessTokenExpirationDate", value);
            }
        }

        /// <summary>
        /// Authenticator
        /// </summary>
        public static OAuth2Authenticator Authenticator;

        #endregion
    }
}