using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    /// <summary>
    /// Register
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public Register()
        {
            InitializeComponent();
            Title = "Register";
        }

        /// <summary>
        /// Button clicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        #endregion
    }
}