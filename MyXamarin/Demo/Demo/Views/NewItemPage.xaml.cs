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
 * New item page
 **************************************************************************************************************/
#endregion
#endregion

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    using Models;

    /// <summary>
    /// New item page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        #region -- Methods --

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Item
        /// </summary>
        public Item Item { get; set; }

        #endregion
    }
}