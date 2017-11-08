using System;

namespace Demo.Views
{
    using Plugin.Toasts;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

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