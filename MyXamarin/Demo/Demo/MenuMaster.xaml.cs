using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo
{
    using Demo.Helpers;
    using Demo.Views;
    using ViewModels;

    /// <summary>
    /// Menu master
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuMaster : ContentPage
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public MenuMaster()
        {
            InitializeComponent();
            BindingContext = new MenuVM();
            ListView = MenuItemsListView;
        }

        #endregion

        #region -- Properties --

        public ListView ListView { get; set; }

        #endregion

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Settings.AccessToken = "";
            Settings.User = "";
            Settings.Password = "";

            Application.Current.MainPage = new NavigationPage(new Login());
        }
    }
}