using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Toasts;

namespace Demo.ViewModels
{
    using Models;

    /// <summary>
    /// User view model
    /// </summary>
    public class UserVM : BaseVM
    {
        #region -- Methods --

        public UserVM()
        {
            Title = "Log in";
            Model = new UserModel();
            LogInCommand = new Command(async () => await ExecuteLogInCommand());
        }

        /// <summary>
        /// Execute LogInCommand
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteLogInCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                var m = await UserService.LogInAsync(Model.User, Model.Pass);
                if (m.Success)
                {
                    await App.Current.MainPage.DisplayAlert("Notification", "Login Success", "OK");
                    await App.Current.MainPage.Navigation.PushModalAsync(new Views.GetMyPosition());
                    //TODO - Set Pagiantion
                }
                else
                {
                    var notificator = DependencyService.Get<IToastNotificator>();
                    var x = m.ErrMsg;
                    var options = new NotificationOptions()
                    {
                        Title = "Error",
                        Description = x
                    };
                    var result = await notificator.Notify(options);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Model
        /// </summary>
        public UserModel Model { get; set; }

        /// <summary>
        /// Log in command
        /// </summary>
        public Command LogInCommand { get; set; }

        #endregion
    }
}