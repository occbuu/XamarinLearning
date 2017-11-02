using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo
{
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
    }
}