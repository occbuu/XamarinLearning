using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo.ViewModels
{
    using Helpers;
    using Models;

    /// <summary>
    /// Register view model
    /// </summary>
    public class RegisterVM : BaseVM
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public RegisterVM()
        {
            Title = "Register";
            Model = new RegisterModel();
            RegisterCommand = new Command(async () => await ExecuteRegisterCommand());
        }

        /// <summary>
        /// Execute register command
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteRegisterCommand()
        {
            if (IsBusy) { return; }
            IsBusy = true;

            try
            {
                var ok = await AccountService.RegisterAsync(Model.Email,
                    Model.Password, Model.ConfirmPassword);

                Settings.User = Model.Email;
                Settings.Password = Model.Password;

                if (ok)
                {
                    Message = "Success :)";
                }
                else
                {
                    Message = "Please try again :(";
                }
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
        public RegisterModel Model { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Register command
        /// </summary>
        public Command RegisterCommand { get; set; }

        #endregion
    }
}