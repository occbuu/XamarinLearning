using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    using Helpers;

    /// <summary>
    /// Login
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public Login()
        {
            InitializeComponent();
            Title = "Login";

            Settings.AccessToken = null;
            Settings.User = null;
            Settings.Password = null;

#if DEBUG
            txtUserName.Text = "nvt87x@gmail.com";
            txtPassword.Text = "P@ssword123";
#endif
        }

        #endregion

        /// <summary>
        /// Go register clicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }

        /// <summary>
        /// Go forget password clicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private async void Forget_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgetPassword());
        }

        /// <summary>
        /// Go Facebook clicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private async void GoFacebook_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacebookProfile());
        }

        /// <summary>
        /// Go Google clicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private async void GoGoogle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OAuthNativeFlow());
        }
    }
}