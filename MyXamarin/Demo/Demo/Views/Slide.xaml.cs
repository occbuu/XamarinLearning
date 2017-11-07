using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Slide : ContentPage
    {
        public Slide()
        {
            InitializeComponent();

            var images = new List<string>
            {
                "mobile1.jpg",
                "mobile2.jpg",
                "mobile3.jpg",
                "mobile4.jpg"
            };

            MainCarouselView.ItemsSource = images;
        }
    }
}