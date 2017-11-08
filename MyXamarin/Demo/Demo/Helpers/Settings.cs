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
        public static string AccessToken { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public static string User { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// Authenticator
        /// </summary>
        public static OAuth2Authenticator Authenticator;

        #endregion
    }
}