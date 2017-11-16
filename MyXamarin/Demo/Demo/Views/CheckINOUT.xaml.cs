using Newtonsoft.Json;
using Plugin.DeviceInfo;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    using Helpers;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckINOUT : ContentPage
    {
        private string url = "http://occapp.ddns.net:9696/api/TimeOutIn/CheckOutIn";
        public CheckINOUT()
        {
            InitializeComponent();
        }

        private async void CheckIN_Clicked(object sender, EventArgs e)
        {
            var hasPermission = await Utils.CheckPermissions(Permission.Location);
            if (!hasPermission)
            {
                return;
            }

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(TimeSpan.FromMinutes(10));
                //var position = await locator.GetLastKnownPositionAsync();
                var longitude = position.Longitude.ToString().Replace(",", ".");
                var latitude = position.Latitude.ToString().Replace(",", ".");



                var address = await locator.GetAddressesForPositionAsync(position);
                var add = address.FirstOrDefault();
                //var add = address;
                var addressOb = string.Format("{0}, {1}, {2}, {3}, {4}", add.Thoroughfare, add.SubLocality, add.SubAdminArea, add.AdminArea, add.CountryName);

                var devInfor = CrossDeviceInfo.Current;
                var deviceIDOb = string.Format("{0}|{1}|{2}", devInfor.Model, devInfor.Platform, devInfor.Id);


                var client = new HttpClient();
                var m = new
                {
                    ObjectID = "99151121000001",
                    StampTime = DateTime.Now,
                    Type = "IN",
                    DeviceID = deviceIDOb,
                    Latitude = latitude,
                    Longtitude = longitude,
                    Altitude = "0",
                    Address = addressOb
                };

                var content = CreateData(m);
                var rsp = await client.PostAsync(url, content);
                var json = await rsp.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Models.BaseModel>(json);

                if (!rsp.IsSuccessStatusCode)
                {
                    res.ErrMsg = "Cannot call to WebAPI service";
                }
                if (res.Success)
                {
                    LblMssg.Text = "Check In success!!!";
                }
                else
                {
                    LblMssg.Text = "Check In fail............";
                }
            }
            catch (Exception ex)
            {
                LblMssg.Text = "Check In fail............" + ex.Message;
            }
        }

        private async void CheckOUT_Clicked(object sender, EventArgs e)
        {
            var hasPermission = await Utils.CheckPermissions(Permission.Location);
            if (!hasPermission)
            {
                return;
            }

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(TimeSpan.FromMinutes(10));
                //var position = await locator.GetLastKnownPositionAsync();
                var longitude = position.Longitude.ToString().Replace(",", ".");
                var latitude = position.Latitude.ToString().Replace(",", ".");

                var address = await locator.GetAddressesForPositionAsync(position);
                var add = address.FirstOrDefault();
                //var add = address;
                var addressOb = string.Format("{0}, {1}, {2}, {3}, {4}", add.Thoroughfare, add.SubLocality, add.SubAdminArea, add.AdminArea, add.CountryName);

                var devInfor = CrossDeviceInfo.Current;
                var deviceIDOb = string.Format("{0}|{1}|{2}", devInfor.Model, devInfor.Platform, devInfor.Id);


                var client = new HttpClient();
                var m = new
                {
                    ObjectID = "99151121000001",
                    StampTime = DateTime.Now,
                    Type = "OUT",
                    DeviceID = deviceIDOb,
                    Latitude = latitude,
                    Longtitude = longitude,
                    Altitude = "0",
                    Address = addressOb
                };

                var content = CreateData(m);
                var rsp = await client.PostAsync(url, content);
                var json = await rsp.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Models.BaseModel>(json);

                if (!rsp.IsSuccessStatusCode)
                {
                    res.ErrMsg = "Cannot call to WebAPI service";
                }
                if (res.Success)
                {
                    LblMssg.Text = "Check Out success!!!";
                }
                else
                {
                    LblMssg.Text = "Check Out fail............";
                }
            }
            catch (Exception ex)
            {
                LblMssg.Text = "Check Out fail............" + ex.Message;
            }
        }
        protected StringContent CreateData(object data)
        {
            var jo = JsonConvert.SerializeObject(data);
            var res = new StringContent(jo);
            res.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return res;
        }
    }
}