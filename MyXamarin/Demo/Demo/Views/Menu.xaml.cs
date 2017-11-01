using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : MasterDetailPage
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            Detail = new Login();
        }

        private void Profile_Clicked(object sender, EventArgs e)
        {
            Detail = new Profile();
        }

        private void Brightness_Clicked(object sender, EventArgs e)
        {
            Detail = new Brightness();
        }

        private void GetDevice_Clicked(object sender, EventArgs e)
        {
            Detail = new GetDevice();
        }

        private void GetMyPosition_Clicked(object sender, EventArgs e)
        {
            Detail = new GetMyPosition();
        }

        private void UploadImage_Clicked(object sender, EventArgs e)
        {
            Detail = new UploadImages();
        }
    }
}