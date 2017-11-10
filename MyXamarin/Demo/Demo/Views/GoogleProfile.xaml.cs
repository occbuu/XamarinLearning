using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    using Helpers;
    using Models;
    using ViewModels;

    /// <summary>
    /// Google profile
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoogleProfile : ContentPage
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public GoogleProfile()
        {
            InitializeComponent();
            Title = "Google profile";

            /*var req = "https://accounts.google.com/o/oauth2/v2/auth"
                + "?response_type=code&scope=openid&redirect_uri="
                + GoogleService.RedirectUri + "&client_id="
                + GoogleService.ClientId;
            var v = new WebView
            {
                Source = req,
                HeightRequest = 1
            };

            v.Navigated += V_Navigated;
            Content = v;*/

            _store = AccountStore.Create();
            _account = _store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            Login();
        }

        /// <summary>
        /// Navigated
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private async void V_Navigated(object sender, WebNavigatedEventArgs e)
        {
            var code = ExtractCodeFromUrl(e.Url);
            if (!string.IsNullOrEmpty(code))
            {
                var vm = BindingContext as GoogleVM;
                var token = await vm.GetTokenAsync(code);
                await vm.SetProfileAsync(token);
                SetPageContent(vm.Model);
                //Content = MainStackLayout;
            }
        }

        /// <summary>
        /// Extract code from URL
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Return the result</returns>
        private string ExtractCodeFromUrl(string url)
        {
            if (url.Contains("code="))
            {
                var tmp = url.Split('&');
                var res = tmp.FirstOrDefault(s => s.Contains("code=")).Split('=')[1];
                return res;
            }

            return string.Empty;
        }

        /// <summary>
        /// Set page content
        /// </summary>
        /// <param name="m">Model</param>
        private void SetPageContent(GoogleModel m)
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(8, 30),
                Children =
                {
                    new Label
                    {
                        Text = m.DisplayName,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.Id,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.Verified.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.Gender,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.Tagline,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.CircledByCount.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.Occupation,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Xamarin.Forms.Image
                    {
                         Source = m.Image.Url,
                         HeightRequest = 100
                    },
                     new Xamarin.Forms.Image
                    {
                         Source = m.Cover.CoverPhoto.Url,
                         HeightRequest = 100
                    },
                }
            };
        }

        /// <summary>
        /// Login
        /// </summary>
        private void Login()
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.iOSClientId;
                    redirectUri = Constants.iOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.AndroidClientId;
                    redirectUri = Constants.AndroidRedirectUrl;
                    break;
            }

            var auth = new OAuth2Authenticator(clientId, null, Constants.Scope,
                new Uri(Constants.AuthorizeUrl), new Uri(redirectUri),
                new Uri(Constants.AccessTokenUrl), null, true);

            auth.Completed += Auth_Completed;
            auth.Error += Auth_Error; ;

            Settings._authenticator = auth;
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(auth);
        }

        /// <summary>
        /// Authenticator completed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            var auth = sender as OAuth2Authenticator;
            if (auth != null)
            {
                auth.Completed -= Auth_Completed;
                auth.Error -= Auth_Error;
            }

            UserModel user = null;
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<UserModel>(userJson);
                }

                if (_account != null)
                {
                    _store.Delete(_account, Constants.AppName);
                }

                await _store.SaveAsync(_account = e.Account, Constants.AppName);
                await DisplayAlert("Email address", user.Email, "OK");
            }
        }

        /// <summary>
        /// Authenticator error
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private void Auth_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            var auth = sender as OAuth2Authenticator;
            if (auth != null)
            {
                auth.Completed -= Auth_Completed;
                auth.Error -= Auth_Error;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// Account
        /// </summary>
        private Account _account;

        /// <summary>
        /// Account store
        /// </summary>
        private AccountStore _store;

        #endregion
    }
}