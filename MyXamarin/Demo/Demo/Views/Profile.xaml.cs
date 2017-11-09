using Plugin.Toasts;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }

        private async void Button_Toast(object sender, EventArgs e)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            var options = new NotificationOptions()
            {
                Title = "Hello",
                Description = "I'm Van !!!",
            };
            var result = await notificator.Notify(options);
        }
    }
}