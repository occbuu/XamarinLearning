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
    public partial class ScannerBarcode : ContentPage
    {
        public ScannerBarcode()
        {
            InitializeComponent();
        }

        private async void btnScanner_Clicked(object sender, EventArgs e)
        {
            try
            {
                #if __ANDROID__
                     MobileBarCodeScanner.Initialize(Application)
                #endif
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                var result = await scanner.Scan();
                if (result != null)
                {

                    lblResult.Text = result.Text;
                    lblResult.IsVisible = true;

                }
            }
            catch (Exception ex) {
                lblResult.Text = ex.Message;
                lblResult.IsVisible = true;
            }

        }
    }
}