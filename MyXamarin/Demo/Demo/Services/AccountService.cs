using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.Services
{
    using Helpers;
    using Models;

    /// <summary>
    /// Account service
    /// </summary>
    public class AccountService : BaseService<BaseModel>
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public AccountService() { }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <param name="confirmPassword">Confirm password</param>
        /// <returns>Return the result</returns>
        public async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
            var m = new RegisterModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };
            var data = CreateData(m);

            var client = new HttpClient();
            var rsp = await client.PostAsync(Host + "api/Account/Register", data);
            if (rsp.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">password</param>
        /// <returns>Return the token</returns>
        public async Task<string> LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var req = new HttpRequestMessage(HttpMethod.Post, Host + "Token")
            {
                Content = new FormUrlEncodedContent(keyValues)
            };

            var client = new HttpClient();
            var rsp = await client.SendAsync(req);
            var content = await rsp.Content.ReadAsStringAsync();
            var jo = JsonConvert.DeserializeObject<dynamic>(content);
            Settings.TokenExpiration = jo.Value<DateTime>(".expires");
            var res = jo.Value<string>("access_token");

            return res;
        }

        #endregion
    }
}