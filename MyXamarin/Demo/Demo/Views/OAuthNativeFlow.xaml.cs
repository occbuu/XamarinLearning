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

    /// <summary>
    /// OAuth native flow
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OAuthNativeFlow : ContentPage
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public OAuthNativeFlow()
        {
            InitializeComponent();
            Title = "OAuth native flow";

            _store = AccountStore.Create();
            _account = _store.FindAccountsForService(Constants.AppName).FirstOrDefault();
        }

        /// <summary>
        /// On login clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void OnLoginClicked(object sender, EventArgs e)
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

            var authenticator = new OAuth2Authenticator(clientId, null, Constants.Scope,
                new Uri(Constants.AuthorizeUrl), new Uri(redirectUri),
                new Uri(Constants.AccessTokenUrl), null, true);
            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            Settings.Authenticator = authenticator;
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        /// <summary>
        /// OnAuth completed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
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
        /// OnAuth error
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
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