using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    using Demo.ViewModels;

    /// <summary>
    /// Facebook profile
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacebookProfile : ContentPage
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public FacebookProfile()
        {
            InitializeComponent();

            var apiRequest = "https://www.facebook.com/v2.10/dialog/oauth?client_id=" + _clientId + "&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";
            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebView_Navigated;
            Content = webView;
        }

        /// <summary>
        /// Web view navigated
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            var token = ExtractAccessTokenFromUrl(e.Url);
            if (!string.IsNullOrEmpty(token))
            {
                var vm = BindingContext as FacebookVM;
                await vm.SetFacebookProfileAsync(token);
                Content = MainStackLayout;
            }
        }

        /// <summary>
        /// Extract access token from URL
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Return the result</returns>
        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var res = at.Remove(at.IndexOf("&expires_in="));
                return res;
            }

            return string.Empty;
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// Client identify
        /// </summary>
        private string _clientId = "128230904509295";

        #endregion
    }
}