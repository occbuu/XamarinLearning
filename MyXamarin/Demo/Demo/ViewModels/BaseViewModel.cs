#region Information
/*
 * Author       : Zng Tfy
 * Email        : nvt87x@gmail.com
 * Phone        : +84 1645 515 010
 * ------------------------------- *
 * Create       : 27/10/2017 08:58
 * Update       : 27/10/2017 08:58
 * Checklist    : 1.0
 * Status       : OK
 */
#region License
/**************************************************************************************************************
 * Copyright © 2012-2017 SKG™ all rights reserved
 **************************************************************************************************************/
#endregion
#region Description
/**************************************************************************************************************
 * Base view model
 **************************************************************************************************************/
#endregion
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Demo.ViewModels
{
    using Models;
    using Services;

    /// <summary>
    /// Base view model
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region -- Implements --

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region -- Methods --

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
        /// Data store
        /// </summary>
        public IDataStore<Item> DataStore =>
            DependencyService.Get<IDataStore<Item>>()
            ?? new UserDataStore();

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
        private bool _isBusy = false;

        /// <summary>
        /// Title
        /// </summary>
        private string _title = string.Empty;

        #endregion
    }
}