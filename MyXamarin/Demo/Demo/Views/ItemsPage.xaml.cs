#region Information
/*
 * Author       : Zng Tfy
 * Email        : nvt87x@gmail.com
 * Phone        : +84 1645 515 010
 * ------------------------------- *
 * Create       : 27/10/2017 09:30
 * Update       : 27/10/2017 09:30
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
 * Items page
 **************************************************************************************************************/
#endregion
#endregion

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    using ViewModels;

    /// <summary>
    /// Items page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _vm = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_vm.Items.Count == 0)
            {
                _vm.LoadItemsCommand.Execute(null);
            }
        }



        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        #region -- Fields --

        /// <summary>
        /// Item
        /// </summary>
        ItemsViewModel _vm;

        #endregion
    }
}