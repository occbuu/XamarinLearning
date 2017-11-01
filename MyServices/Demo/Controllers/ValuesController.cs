using System.Collections.Generic;
using System.Web.Http;

namespace Demo.Controllers
{
    /// <summary>
    /// Values controller
    /// </summary>
    [Authorize]
    public class ValuesController : BaseController
    {
        #region -- Methods --

        /// <summary>
        /// Get
        /// GET api/values
        /// </summary>
        /// <returns>Return the result</returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get
        /// GET api/values/5
        /// </summary>
        /// <param name="id">Identity</param>
        /// <returns>Return the result</returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Post
        /// POST api/values
        /// </summary>
        /// <param name="value">Value</param>
        public void Post([FromBody]string value) { }

        /// <summary>
        /// Put
        /// PUT api/values/5
        /// </summary>
        /// <param name="id">Identity</param>
        /// <param name="value">Value</param>
        public void Put(int id, [FromBody]string value) { }

        /// <summary>
        /// Delete
        /// DELETE api/values/5
        /// </summary>
        /// <param name="id">Identity</param>
        public void Delete(int id) { }

        #endregion
    }
}