using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo.ViewModels
{
    using Helpers;

    /// <summary>
    /// Login view model
    /// </summary>
    public class LoginVM : BaseVM
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public LoginVM()
        {
            Title = "Login";

            if (Settings.User != null)
            {
                UserName = Settings.User;
                Password = Settings.Password;
            }
            else
            {
#if DEBUG
                UserName = "nvt87x@gmail.com";
                Password = "P@ssword123";
#endif
            }

            LoginCommand = new Command(async () => await ExecuteLoginCommand());
        }

        /// <summary>
        /// Execute login command
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteLoginCommand()
        {
            if (IsBusy) { return; }
            IsBusy = true;

            try
            {
                Settings.AccessToken = await AccountService.LoginAsync(UserName, Password);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally { IsBusy = false; }
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Login command
        /// </summary>
        public Command LoginCommand { get; set; }

        #endregion
    }
}