using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    using Services;
    using ViewModels;

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
            Title = "Facebook orofile";

            var req = "https://www.facebook.com/v2.10/dialog/oauth?client_id="
                + FacebookService.ClientId + "&response_type=token&redirect_uri="
                + FacebookService.RedirectUri;
            var v = new WebView
            {
                Source = req,
                HeightRequest = 1
            };

            v.Navigated += V_Navigated;
            Content = v;
        }

        /// <summary>
        /// Navigated
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private async void V_Navigated(object sender, WebNavigatedEventArgs e)
        {
            var token = ExtractTokenFromUrl(e.Url);
            if (!string.IsNullOrEmpty(token))
            {
                var vm = BindingContext as FacebookVM;
                await vm.SetProfileAsync(token);
                Content = MainStackLayout;
            }
        }

        /// <summary>
        /// Extract token from URL
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Return the result</returns>
        private string ExtractTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                var res = at.Remove(at.IndexOf("&expires_in="));
                return res;
            }

            return string.Empty;
        }

        #endregion
    }
}