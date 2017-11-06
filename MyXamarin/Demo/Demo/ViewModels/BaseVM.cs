using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Demo.ViewModels
{
    using Services;

    /// <summary>
    /// Base view model
    /// </summary>
    public class BaseVM : INotifyPropertyChanged
    {
        #region -- Implements --

        /// <summary>
        /// Event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public BaseVM() { }

        /// <summary>
        /// On property changed
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Set property
        /// </summary>
        /// <typeparam name="T">Entity class type</typeparam>
        /// <param name="backingStore">Backing store</param>
        /// <param name="value">Value</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="onChanged">On changed</param>
        /// <returns>Return the result</returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            return true;
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Account service
        /// </summary>
        public AccountService AccountService => DependencyService.Get<AccountService>() ?? new AccountService();

        /// <summary>
        /// Facebook service
        /// </summary>
        public FacebookService FacebookService => DependencyService.Get<FacebookService>() ?? new FacebookService();

        /// <summary>
        /// Object service
        /// </summary>
        public ObjectService ObjectService => DependencyService.Get<ObjectService>() ?? new ObjectService();

        /// <summary>
        /// Busy
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// Busy
        /// </summary>
        private bool _isBusy;

        /// <summary>
        /// Title
        /// </summary>
        private string _title;

        #endregion
    }
}