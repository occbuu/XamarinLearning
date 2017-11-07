using System.Collections.ObjectModel;

namespace Demo.ViewModels
{
    using Models;
    using Views;

    /// <summary>
    /// Menu view model
    /// </summary>
    public class MenuVM : BaseVM
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public MenuVM()
        {
            Title = "Main menu";
            Models = new ObservableCollection<MenuModel>();

            var m = new MenuModel { Id = 0, Title = "Log in", TargetType = typeof(Login) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Profile" };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Main page", TargetType = typeof(MainPage) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "List objects", TargetType = typeof(ListObjects) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Test SQLite", TargetType = typeof(ProductPage2) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Get Device", TargetType = typeof(GetDevice) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Get Position", TargetType = typeof(GetMyPosition) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Get Position", TargetType = typeof(Slide) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Log out" };
            Models.Add(m);
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// List models
        /// </summary>
        public ObservableCollection<MenuModel> Models { get; set; }

        #endregion
    }
}