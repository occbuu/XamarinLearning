using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.Services
{
    using Helpers;
    using Models;

    /// <summary>
    /// Facebook service
    /// </summary>
    public class FacebookService : BaseService<BaseModel>
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public FacebookService() { }

        /// <summary>
        /// Get profile
        /// </summary>
        /// <returns>Return the result</returns>
        public async Task<FacebookModel> GetProfileAsync()
        {
            var url = "https://graph.facebook.com/v2.10/me/?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages&access_token=" + Settings.AccessToken;
            var client = new HttpClient();
            var json = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<FacebookModel>(json);
            return res;
        }

        #endregion

        #region -- Constants --

        /// <summary>
        /// Redirect URI
        /// </summary>
        public const string RedirectUri = "https://www.facebook.com/connect/login_success.html";

        /// <summary>
        /// Client identify
        /// </summary>
        public const string ClientId = "128230904509295";

        #endregion
    }
}