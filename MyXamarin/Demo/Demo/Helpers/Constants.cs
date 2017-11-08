namespace Demo.Helpers
{
    /// <summary>
    /// Constants
    /// </summary>
    public static class Constants
    {
        public static string AppName = "OAuthNativeFlow";

        // OAuth, for Google login, configure at https://console.developers.google.com/
        public static string iOSClientId = "788182042675-82f6dbdn1rccd2isuupc6jbbboaj5puc.apps.googleusercontent.com";
        public static string AndroidClientId = "788182042675-jvp07gj2lb2593mub7ip4mmviu42qibo.apps.googleusercontent.com";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "com.googleusercontent.apps.788182042675-82f6dbdn1rccd2isuupc6jbbboaj5puc:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.788182042675-jvp07gj2lb2593mub7ip4mmviu42qibo:/oauth2redirect";
    }
}