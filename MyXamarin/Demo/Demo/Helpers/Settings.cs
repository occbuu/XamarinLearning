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

        /// <summary>
        /// Access token expiration date
        /// </summary>
        public static DateTime TokenExpiration
        {
            get
            {
                return AppSettings.GetValueOrDefault("TokenExpiration", DateTime.UtcNow);
            }
            set
            {
                AppSettings.AddOrUpdateValue("TokenExpiration", value);
            }
        }

        /// <summary>
        /// App settings
        /// </summary>
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// Authenticator
        /// </summary>
        public static OAuth2Authenticator _authenticator;

        #endregion
    }
}