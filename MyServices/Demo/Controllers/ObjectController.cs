using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Demo.Controllers
{
    using Models;

    /// <summary>
    /// Object controller
    /// </summary>
    public class ObjectController : BaseController
    {
        #region -- Methods --

        /* api/Object/GetAll
        */
        [HttpPost]
        public HttpResponseMessage GetAll()
        {
            var msg = new ObjectModel();

            try
            {
                var q = ObjectService.GetAll();
                var t = q
                    .Select(p => new ObjectData
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
                        Gender = p.Gender
                    });

                msg.Data = new ObservableCollection<ObjectData>(t);
                msg.Success = true;
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