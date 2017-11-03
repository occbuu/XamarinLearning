using Xamarin.Forms;

namespace Demo
{
    using Demo.Services;
    using Demo.ViewModels;
    using Demo.Views;

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
            MainPage = new Menu();
        }

        public App(IProductsService productsRepository)
        {
            InitializeComponent();

            var productsPage = new ProductsPage()
            {
                BindingContext = new ProductsVM(productsRepository)
            };

            MainPage = new NavigationPage(productsPage);
        }
        #endregion
    }
}