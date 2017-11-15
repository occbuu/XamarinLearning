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

            var m = new MenuModel { Id = 0, Title = "Profile", Icon = "Profile.png" };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Register_RedDot", TargetType = typeof(RedDot_Register) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Landing_RedDot", TargetType = typeof(RedDot_Landing) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "List objects", Icon = "ListObject.png", TargetType = typeof(ListObjects) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Check In/Out", Icon = "CheckInOut.png", TargetType = typeof(CheckINOUT) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Test SQLite", TargetType = typeof(ProductPage2) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Get Device", Icon = "GetDevice.png", TargetType = typeof(GetDevice) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Get Position", Icon = "Position.png", TargetType = typeof(GetMyPosition) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Slide", Icon = "Slides.png", TargetType = typeof(Slide) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Component", TargetType = typeof(ActionSheet) };
            Models.Add(m);

            m = new MenuModel { Id = 0, Title = "Tabs", Icon = "Tabs.png", TargetType = typeof(Tabs) };
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