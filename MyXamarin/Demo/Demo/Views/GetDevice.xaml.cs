using Plugin.DeviceInfo;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetDevice : ContentPage
    {
        public GetDevice()
        {
            InitializeComponent();
        }

        private void OpenUriClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://vnexpress.net"));
        }

        private void GetInformationClicked(object sender, EventArgs e)
        {

            var heading = "";

            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    heading += " Phone ";
                    break;
                case TargetIdiom.Tablet:
                    heading += " Tablet ";
                    break;
                case TargetIdiom.Desktop:
                    heading += " Desktop ";
                    break;
                default:
                    heading += " unknown ";
                    break;
            }

            // Device.RuntimePlatform
            if (Device.RuntimePlatform == Device.iOS)
            {
                heading += "iOS";
            }
            else
            { // could be Android or WinPhone
                heading += Device.RuntimePlatform;
            }
            LblTagertPlatform.Text = heading;
            LblFontSize.Text = Device.GetNamedSize(NamedSize.Large, typeof(String)).ToString();

            //Dung plugin :Xam.Plugin.DeviceInfo
            var devInfor = CrossDeviceInfo.Current;
            LblTargetIdiom.Text = " ID: " + devInfor.Id + "\n Model number: " + devInfor.Model
                + " \n Platform: " + devInfor.Platform
                + " \n Version: " + devInfor.Version;
        }
    }
}