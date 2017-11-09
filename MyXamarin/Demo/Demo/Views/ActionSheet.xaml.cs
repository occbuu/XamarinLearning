using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionSheet : ContentPage
    {
        public ActionSheet()
        {
            InitializeComponent();
        }

        private async void ActionSheet_Clicked(object sender, EventArgs e)
        {
            var color = await DisplayActionSheet("ActionSheet: Choose background color!", "Cancel", null, "Red", "Blue", "Green");
            if (color == "Red")
            {
                BackgroundColor = Color.Red;

            }
            else if (color == "Blue")
            {
                BackgroundColor = Color.Blue;
            }
            else
            {
                BackgroundColor = Color.Green;
            }
        }
    }
}