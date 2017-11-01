using System;

namespace Demo.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Plugin.Toasts;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
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