using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Demo.Controllers
{
    using Models;

    /// <summary>
    /// User controller
    /// </summary>
    public class UserController : BaseController
    {
        #region -- Methods --

        /* api/User/LogIn
            {
              "User": "vantoan",
              "Pass": "123456"
            }
        */
        [HttpPost]
        public HttpResponseMessage LogIn(JObject jsonData)
        {
            var msg = new UserModel();

            try
            {
                dynamic json = jsonData;
                string user = json.User;
                string pass = json.Pass;

                var m = UserService.LogIn(user, pass);
                if (m != null)
                {
                    msg.User = m.UserID;
                    msg.ObjectID = m.ObjectID;
                    msg.LoginName = m.LoginName;
                    msg.LoginName = m.LoginName;
                    msg.RoleID = m.RoleID;
                    msg.ManageGroup = m.ManageGroup;
                    msg.Success = true;
                }
                else
                {
                    msg.ErrMsg = "Invalid user or password";
                }
            }
            catch (Exception ex)
            {
                msg.ErrMsg = ex.Message;
            }

            return Response(msg);
        }

        #endregion
    }
}