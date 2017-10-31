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

        #endregion
    }
}