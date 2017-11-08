using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
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
        }

        #endregion

        private async void Register_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }

        private void Forget_Clicked(object sender, System.EventArgs e)
        {
            //await Navigation.PushAsync(new Forget());
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