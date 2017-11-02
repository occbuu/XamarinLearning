using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Demo.Controllers
{
    using Models;

    /// <summary>
    /// Object controller
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Object")]
    public class ObjectController : BaseController
    {
        #region -- Methods --

        /// <summary>
        /// Get all data
        /// GET api/Object/GetAll
        /// </summary>
        /// <returns>Return the result</returns>
        [Route("GetAll")]
        [HttpGet]
        public List<ObjectModel> GetAll()
        {
            var q = ObjectService.GetAll();
            var res = q
                .Select(p => new ObjectModel
                {
                    ObjectID = p.ObjectID,
                    FullName = p.FullName,
                    PID = p.PID,
                    PIDDate = p.PIDDate,
                    PIDIssue = p.PIDIssue,
                    DoB = p.DoB,
                    PoB = p.PoB,
                    PerAdd = p.PerAdd,
                    TemAdd = p.TemAdd,
                    Gender = p.Gender,
                    Tel = p.Tel,
                    Email = p.Email
                }).ToList();

            return res;
        }

        #endregion
    }
}