﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    /// <summary>
    /// Login
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public Login()
        {
            InitializeComponent();
            Title = "Login";
        }

        #endregion
    }
}