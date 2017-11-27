using Plugin.Vibrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VibrateDemo : ContentPage
    {
        public VibrateDemo()
        {
            InitializeComponent();
        }

        private void btnVibrate_Clicked(object sender, EventArgs e)
        {
            try
            {
                var v = CrossVibrate.Current;
                v.Vibration(TimeSpan.FromSeconds(20));

                lblResult.Text = "Who is calling....";
                lblResult.IsVisible = true;
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
                lblResult.IsVisible = true;
            }
        }
    }
}