using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo.ViewModels
{
    using Models;

    /// <summary>
    /// Object view model
    /// </summary>
    public class ObjectVM : BaseVM
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public ObjectVM()
        {
            Title = "List objects";
            Models = new ObservableCollection<ObjectModel>();
            LoadCommand = new Command(async () => await ExecuteLoadCommand());
        }

        /// <summary>
        /// Execute LoadCommand
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteLoadCommand()
        {
            if (IsBusy) { return; }
            IsBusy = true;

            try
            {
                var e = await ObjectService.GetAllAsync();
                Models.Clear();

                foreach (var i in e)
                {
                    Models.Add(i);
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
        /// List models
        /// </summary>
        public ObservableCollection<ObjectModel> Models { get; set; }

        /// <summary>
        /// Load command
        /// </summary>
        public Command LoadCommand { get; set; }

        #endregion
    }
}