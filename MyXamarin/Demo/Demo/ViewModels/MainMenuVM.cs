using System.Collections.ObjectModel;

namespace Demo.ViewModels
{
    using Models;
    using Views;

    public class MainMenuVM : BaseVM
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public MainMenuVM()
        {
            Title = "Main menu";
            Models = new ObservableCollection<MainMenuModel>();

            var m = new MainMenuModel { Id = 0, Title = "Login", TargetType = typeof(Login) };
            Models.Add(m);

            m = new MainMenuModel { Id = 1, Title = "Profile", TargetType = typeof(Profile) };
            Models.Add(m);

            m = new MainMenuModel { Id = 2, Title = "Brigthness", TargetType = typeof(Brightness) };
            Models.Add(m);

            m = new MainMenuModel { Id = 3, Title = "Get My Position", TargetType = typeof(GetMyPosition) };
            Models.Add(m);

            m = new MainMenuModel { Id = 4, Title = "Get Device", TargetType = typeof(GetDevice) };
            Models.Add(m);
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// List models
        /// </summary>
        public ObservableCollection<MainMenuModel> Models { get; set; }

        #endregion
    }
}
