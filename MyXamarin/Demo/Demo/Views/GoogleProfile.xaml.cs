using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    using Models;
    using Services;
    using ViewModels;

    /// <summary>
    /// Google profile
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoogleProfile : ContentPage
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public GoogleProfile()
        {
            InitializeComponent();
            Title = "Google profile";

            var req = "https://accounts.google.com/o/oauth2/v2/auth"
                + "?response_type=code&scope=openid&redirect_uri="
                + GoogleService.RedirectUri + "&client_id="
                + GoogleService.ClientId;
            var v = new WebView
            {
                Source = req,
                HeightRequest = 1
            };

            v.Navigated += V_Navigated;
            Content = v;
        }

        /// <summary>
        /// Navigated
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private async void V_Navigated(object sender, WebNavigatedEventArgs e)
        {
            var code = ExtractCodeFromUrl(e.Url);
            if (!string.IsNullOrEmpty(code))
            {
                var vm = BindingContext as GoogleVM;
                var token = await vm.GetTokenAsync(code);
                await vm.SetProfileAsync(token);
                SetPageContent(vm.Model);
                //Content = MainStackLayout;
            }
        }

        /// <summary>
        /// Extract code from URL
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Return the result</returns>
        private string ExtractCodeFromUrl(string url)
        {
            if (url.Contains("code="))
            {
                var tmp = url.Split('&');
                var res = tmp.FirstOrDefault(s => s.Contains("code=")).Split('=')[1];
                return res;
            }

            return string.Empty;
        }

        /// <summary>
        /// Set page content
        /// </summary>
        /// <param name="m">Model</param>
        private void SetPageContent(GoogleModel m)
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(8, 30),
                Children =
                {
                    new Label
                    {
                        Text = m.DisplayName,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.Id,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.Verified.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.Gender,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.Tagline,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.CircledByCount.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = m.Occupation,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Xamarin.Forms.Image
                    {
                         Source = m.Image.Url,
                         HeightRequest = 100
                    },
                     new Xamarin.Forms.Image
                    {
                         Source = m.Cover.CoverPhoto.Url,
                         HeightRequest = 100
                    },
                }
            };
        }

        #endregion
    }
}