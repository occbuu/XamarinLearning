using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo.Controllers
{
    using BLL;
    using Utility;

    /// <summary>
    /// Base controller
    /// </summary>
    public class BaseController : ApiController
    {
        #region -- Methods --

        /// <summary>
        /// Response
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Return the message</returns>
        protected HttpResponseMessage Response(object data)
        {
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        /// <summary>
        /// Get path image with folder
        /// </summary>
        /// <param name="key">Image type</param>
        /// <returns>Return the result</returns>
        protected string GetPathImage(AppSettings key)
        {
            var res = GetAppSetting(AppSettings.ImagePath);
            res += GetAppSetting(key);
            return res;
        }

        /// <summary>
        /// Get AppSetting from Web.config
        /// </summary>
        /// <param name="key">Enum key</param>
        /// <returns>Return the result</returns>
        protected string GetAppSetting(Enum key)
        {
            var res = ConfigurationManager.AppSettings[key.ToString()];
            return res;
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// User service
        /// </summary>
        public static UserService UserService { get { return ZConverts.Xingleton<UserService>(); } }

        /// <summary>
        /// TimeOutIn service
        /// </summary>
        public static TimeOutInService TimeOutInService { get { return ZConverts.Xingleton<TimeOutInService>(); } }

        /// <summary>
        /// Object service
        /// </summary>
        public static ObjectService ObjectService { get { return ZConverts.Xingleton<ObjectService>(); } }

        #endregion
    }
}