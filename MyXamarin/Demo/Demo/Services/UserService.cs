using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.Services
{
    using Models;

    /// <summary>
    /// User service
    /// </summary>
    public class UserService : BaseService<UserModel>
    {
        /// <summary>
        /// Check LogIn
        /// </summary>
        /// <param name="user">User account</param>
        /// <param name="pass">Password</param>
        /// <returns>Return the result</returns>
        public async Task<UserModel> LogInAsync(string user, string pass)
        {
            var client = new HttpClient();
            var m = new UserModel
            {
                User = user,
                Pass = pass
            };

            var content = CreateData(m);
            var rsp = await client.PostAsync(Host + "User/LogIn", content);
            var json = await rsp.Content.ReadAsStringAsync();
            m = JsonConvert.DeserializeObject<UserModel>(json);

            if (!rsp.IsSuccessStatusCode)
            {
                m.ErrMsg = "Cannot call to WebAPI service";
            }

            return m;
        }
    }
}