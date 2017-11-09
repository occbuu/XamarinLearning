using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    /// <summary>
    /// Forget password
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgetPassword : ContentPage
    {
        #region -- Methods --

        public ForgetPassword()
        {
            InitializeComponent();
            Title = "Forget password";
        }

        #endregion
    }
}