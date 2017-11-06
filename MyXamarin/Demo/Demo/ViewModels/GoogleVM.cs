using System.Threading.Tasks;

namespace Demo.ViewModels
{
    using Helpers;
    using Models;
    using Services;

    /// <summary>
    /// Google view model
    /// </summary>
    public class GoogleVM : BaseVM
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public GoogleVM()
        {
            Title = "Google";
        }

        /// <summary>
        /// Set profile
        /// </summary>
        /// <param name="token">accessToken</param>
        /// <returns>Return the result</returns>
        public async Task SetProfileAsync(string token)
        {
            Settings.AccessToken = token;
            Model = await GoogleService.GetProfileAsync();
        }

        /// <summary>
        /// Get access token
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>Return the result</returns>
        public async Task<string> GetTokenAsync(string code)
        {
            var res = await GoogleService.GetTokenAsync(code);
            return res;
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Model
        /// </summary>
        public GoogleModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// Model
        /// </summary>
        private GoogleModel _model;

        #endregion
    }
}