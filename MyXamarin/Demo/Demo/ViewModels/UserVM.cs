using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

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
                    //TODO - Log in successful
                }
                else
                {
                    //TODO - Log in failed
                    var x = m.ErrMsg;
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