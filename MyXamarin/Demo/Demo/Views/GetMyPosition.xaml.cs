using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    using Demo.Helpers;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetMyPosition : ContentPage
    {
        public GetMyPosition()
        {
            InitializeComponent();
        }

        private async void GetPositionClicked(object sender, EventArgs e)
        {
            var hasPermission = await Utils.CheckPermissions(Permission.Location);
            if (!hasPermission)
            {
                return;
            }
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var postion = await locator.GetLastKnownPositionAsync();
            //var postion = await locator.GetPositionAsync(TimeSpan.FromMinutes(10));

            var address = await locator.GetAddressesForPositionAsync(postion);

            ////  MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(postion.Latitude, postion.Longitude), Distance.FromMeters(1)));

            var a = address;

            var longitude = postion.Longitude.ToString().Replace(",", ".");
            var latitude = postion.Latitude.ToString().Replace(",", ".");

            //LongitudeLabel.Text = longitude;
            //LatitudeLabel.Text = latitude;
            var lati = double.Parse(postion.Latitude.ToString());
            var longti = double.Parse(postion.Longitude.ToString());

            var map = new Map(MapSpan.FromCenterAndRadius(
              new Position(lati, longti),
              Distance.FromKilometers(1)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(lati, longti),
                Label = "This is your location",
                Address = lati + ", " + longti
            };
            var btnBack = new Button { Text = "Back" };
            var btnLati = new Button { Text = "La:" + latitude };
            var btnLongti = new Button { Text = "Lg:" + longitude };
            btnBack.Clicked += BtnBack_Clicked;

            var buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                    btnBack,
                    btnLati,
                    btnLongti
                }
            };

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    map,
                    buttons
                }
            };
            //Content = map;
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new GetMyPosition());
            //Chuyen qua trang moi la Navigation.PushAsync(new TrangMoi())
        }
    }
}