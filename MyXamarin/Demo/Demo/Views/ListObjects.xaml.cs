using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    using ViewModels;

    /// <summary>
    /// List objects
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListObjects : ContentPage
    {
        #region -- Overrides --

        /// <summary>
        /// On appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_vm.Models.Count == 0)
            {
                _vm.LoadCommand.Execute(null);
            }
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public ListObjects()
        {
            InitializeComponent();
            BindingContext = _vm = new ObjectVM();
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// View model
        /// </summary>
        private ObjectVM _vm;

        #endregion
    }
}