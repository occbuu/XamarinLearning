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

        private async void Register_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }

        private async void Forget_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ForgetPassword());
        }

        private async void GoFacebook_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new FacebookProfile());
        }

        private async void GoGoogle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new GoogleProfile());
        }
    }
}