using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadImages : ContentPage
    {

        private MediaFile _mediaFile;
        private string url = "http://occapp.ddns.net:9696/api/File/SaveImage";
        public UploadImages()
        {
            InitializeComponent();
            //takePhoto.Clicked += async (sender, args) =>
            //{

            //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //    {
            //        DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
            //        return;
            //    }

            //    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //    {
            //        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            //        Directory = "Sample",
            //        Name = "test.jpg"
            //    });

            //    if (file == null)
            //        return;

            //    DisplayAlert("File Location", file.Path, "OK");

            //    //Add
            //    image.Source = ImageSource.FromStream(() =>
            //    {
            //        var stream = file.GetStream();
            //        file.Dispose();
            //        return stream;
            //    });
            //};

            //pickPhoto.Clicked += async (sender, args) =>
            //{
            //    if (!CrossMedia.Current.IsPickPhotoSupported)
            //    {
            //        DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
            //        return;
            //    }
            //    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            //    {
            //        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            //    });


            //    if (file == null)
            //        return;
            //    //Add
            //    image.Source = ImageSource.FromStream(() =>
            //    {
            //        var stream = file.GetStream();
            //        file.Dispose(); //De xoa file di
            //        return stream;
            //    });
            //};

            //takeVideo.Clicked += async (sender, args) =>
            //{
            //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
            //    {
            //        DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
            //        return;
            //    }

            //    var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
            //    {
            //        Name = "video.mp4",
            //        Directory = "DefaultVideos",
            //    });

            //    if (file == null)
            //        return;

            //    DisplayAlert("Video Recorded", "Location: " + file.Path, "OK");

            //    file.Dispose();
            //};

            //pickVideo.Clicked += async (sender, args) =>
            //{
            //    if (!CrossMedia.Current.IsPickVideoSupported)
            //    {
            //        DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
            //        return;
            //    }
            //    var file = await CrossMedia.Current.PickVideoAsync();

            //    if (file == null)
            //        return;

            //    DisplayAlert("Video Selected", "Location: " + file.Path, "OK");
            //    file.Dispose();
            //};
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "myImage.jpg"
            });

            if (_mediaFile == null)
                return;

            LblPath.Text = _mediaFile.Path;

            image.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });
        }

        private async void PickPhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No PickPhoto", ":( No PickPhoto available.", "OK");
                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (_mediaFile == null)
                return;

            LblPath.Text = _mediaFile.Path;

            image.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });
        }

        private async void TakeVideo_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
            {
                DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            _mediaFile = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
            {
                Name = "video.mp4",
                Directory = "DefaultVideos",
            });

            if (_mediaFile == null)
            { return; }
            DisplayAlert("Video Recorded", "Location: " + _mediaFile.Path, "OK");

            //file.Dispose();
        }

        private async void PickVideo_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
                return;
            }
            _mediaFile = await CrossMedia.Current.PickVideoAsync();

            if (_mediaFile == null)
                return;

            DisplayAlert("Video Selected", "Location: " + _mediaFile.Path, "OK");
            //file.Dispose();
        }

        private async void Upload_Clicked(object sender, EventArgs e)
        {
            BinaryReader br = new BinaryReader(_mediaFile.GetStream());
            Byte[] bytes = br.ReadBytes((Int32)_mediaFile.GetStream().Length);
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

            try
            {
                var client = new HttpClient();
                var m = new
                {
                    ObjectID = "99151121000001",
                    Image64 = base64String
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
                    LblRemotePath.Text = "Upload success!!!";
                }
                else
                {
                    LblRemotePath.Text = "Upload fail............";
                }
            }
            catch (Exception ex)
            {

                LblRemotePath.Text = ex.Message;
            }
        }

        protected StringContent CreateData(object data)
        {
            var jo = JsonConvert.SerializeObject(data);
            var res = new StringContent(jo);
            res.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return res;
        }


        ////support Upload file
        //private async void Upload_Clicked1(object sender, System.EventArgs e)
        //{
        //    var content = new MultipartFormDataContent();

        //    content.Add(new StreamContent(_mediaFile.GetStream()),
        //        "\"file\"",
        //        $"\"{_mediaFile.Path}\"");
        //    try
        //    {
        //        var httpClient = new HttpClient();

        //        // DisplayAlert("Content file", "Content: " + content, "OK");

        //        var uploadServiceBaseAddress = "http://localhost:54861/api/Files/Upload";
        //        ////"http://localhost:12214/api/Files/Upload";

        //        var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

        //        LblRemotePath.Text = await httpResponseMessage.Content.ReadAsStringAsync();

        //    }
        //    catch (Exception ex)
        //    {

        //        LblRemotePath.Text = ex.Message;
        //    }
        //}

    }
}