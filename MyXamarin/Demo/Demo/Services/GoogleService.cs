using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.Services
{
    using Helpers;
    using Models;

    /// <summary>
    /// Google service
    /// https://developers.google.com/identity/protocols/OAuth2InstalledApp
    /// </summary>
    public class GoogleService : BaseService<BaseModel>
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public GoogleService() { }

        /// <summary>
        /// Get profile
        /// </summary>
        /// <returns>Return the result</returns>
        public async Task<GoogleModel> GetProfileAsync()
        {
            var url = "https://www.googleapis.com/plus/v1/people/me" + "?access_token=" + Settings.AccessToken;
            var client = new HttpClient();
            var json = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<GoogleModel>(json);
            return res;
        }

        /// <summary>
        /// Get access token
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>Return the result</returns>
        public async Task<string> GetTokenAsync(string code)
        {
            var url = "https://www.googleapis.com/oauth2/v4/token" + "?code=" + code + "&client_id=" + ClientId + "&client_secret=" + ClientSecret + "&redirect_uri=" + RedirectUri + "&grant_type=authorization_code";
            var client = new HttpClient();
            var rsp = await client.PostAsync(url, null);
            var json = await rsp.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<JObject>(json).Value<string>("access_token");
            Settings.AccessToken = res;
            return res;
        }

        #endregion

        #region -- Constants --

        /// <summary>
        /// Redirect URI
        /// </summary>
        public const string RedirectUri = "https://www.youtube.com/user/zngtfy";

        /// <summary>
        /// Client identify
        /// </summary>
        public const string ClientId = "788182042675-8oscd7mlum1v9tord28ov2bap5qr4o9m.apps.googleusercontent.com";

        /// <summary>
        /// Client secret
        /// </summary>
        public const string ClientSecret = "mHluAlD8I3lCW9kYRnhRuuRR";

        #endregion
    }
}