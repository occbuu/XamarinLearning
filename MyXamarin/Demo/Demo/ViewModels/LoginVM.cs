using Plugin.Toasts;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo.ViewModels
{
    using Helpers;
    using Views;

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
            RegisterCommand = new Command(async () => await ExecuteRegisterCommand());
            ForgetCommand = new Command(async () => await ExecuteForgetCommand());
            GoFacebookCommand = new Command(async () => await ExecuteGoFacebookCommand());
            GoGoogleCommand = new Command(async () => await ExecuteGoGoogleCommand());
        }

        /// <summary>
        /// Execute login command
        /// </summary>
        /// <returns>Return the result</returns>
        private async Task ExecuteLoginCommand()
        {
            if (IsBusy) { return; }
            IsBusy = true;

            try
            {
                var t = await AccountService.LoginAsync(UserName, Password);
                Settings.AccessToken = t;

                if (string.IsNullOrEmpty(t))
                {
                    var a = DependencyService.Get<IToastNotificator>();
                    var b = new NotificationOptions()
                    {
                        Title = "Error",
                        Description = "Login failed..."
                    };
                    await a.Notify(b);
                }
                else
                {
                    await _nav.PushModalAsync(new Menu());
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally { IsBusy = false; }
        }

        /// <summary>
        /// Execute register command
        /// </summary>
        /// <returns>Return the result</returns>
        private async Task ExecuteRegisterCommand()
        {
            if (IsBusy) { return; }
            IsBusy = true;

            try
            {
                await _nav.PushModalAsync(new Register());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally { IsBusy = false; }
        }

        /// <summary>
        /// Execute forget password command
        /// </summary>
        /// <returns>Return the result</returns>
        private async Task ExecuteForgetCommand()
        {
            if (IsBusy) { return; }
            IsBusy = true;

            try
            {
                var e = await ObjectService.GetAllAsync();
                //TODO
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally { IsBusy = false; }
        }

        /// <summary>
        /// Execute go Facebook command
        /// </summary>
        /// <returns>Return the result</returns>
        private async Task ExecuteGoFacebookCommand()
        {
            if (IsBusy) { return; }
            IsBusy = true;

            try
            {
                await _nav.PushModalAsync(new FacebookProfile());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally { IsBusy = false; }
        }

        /// <summary>
        /// Execute go Google command
        /// </summary>
        /// <returns>Return the result</returns>
        private async Task ExecuteGoGoogleCommand()
        {
            if (IsBusy) { return; }
            IsBusy = true;

            try
            {
                await _nav.PushModalAsync(new GoogleProfile());
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

        /// <summary>
        /// Register command
        /// </summary>
        public Command RegisterCommand { get; set; }

        /// <summary>
        /// Forget password command
        /// </summary>
        public Command ForgetCommand { get; set; }

        /// <summary>
        /// Go facebook command
        /// </summary>
        public Command GoFacebookCommand { get; set; }

        /// <summary>
        /// Go Google command
        /// </summary>
        public Command GoGoogleCommand { get; set; }

        #endregion
    }
}