using Xamarin.Forms;

namespace Demo
{
    using Helpers;
    using Services;
    using ViewModels;
    using Views;

    /// <summary>
    /// App
    /// </summary>
    public partial class App : Application
    {
        #region -- Overrides --

        /// <summary>
        /// On start
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// On sleep
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// On resume
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public App()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Settings.AccessToken))
            {
                MainPage = new NavigationPage(new Login());
            }
            else
            {
                if (Settings.TokenExpiration != null)
                {
                    if (Settings.TokenExpiration < System.DateTime.Now)
                    {
                        MainPage = new NavigationPage(new Login());
                    }
                    else
                    {
                        MainPage = new Menu();
                    }
                }
                else
                {
                    MainPage = new NavigationPage(new Login());
                }
            }
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="rep">productsRepository</param>
        public App(IProductsService rep) : this()
        {
            var page = new ProductsPage()
            {
                BindingContext = new ProductsVM(rep)
            };

            MainPage = new NavigationPage(page);
        }

        #endregion
    }
}