using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Demo.Controllers
{
    using DAL.DBContext;
    using Models;

    /// <summary>
    /// TimeOutIn controller
    /// </summary>
    [Authorize]
    [RoutePrefix("api/TimeOutIn")]
    public class TimeOutInController : BaseController
    {
        #region -- Methods --

        /*
            {
              "ObjectID": "99151121000001",
              "StampTime": "2015-07-20 19:20:00",
              "Type": "IN",
              "DeviceID": "ASUS_T00G|Android|7d118c35648768f2",
              "Latitude": "10.7622418",
              "Longtitude": "106.701331",
              "Altitude": "0",
              "Address": "109/26 Bến Vân Đồn, phường 8, Quận 4, Hồ Chí Minh, Việt Nam",
            }
        */
        /// <summary>
        /// Check out or in
        /// POST api/TimeOutIn/CheckOutIn
        /// </summary>
        /// <returns>Return the result</returns>
        [Route("CheckOutIn")]
        [HttpPost]
        public HttpResponseMessage CheckOutIn(JObject jsonData)
        {
            var msg = new TimeOutInModel();

            try
            {
                dynamic json = jsonData;
                msg.ObjectID = json.ObjectID;
                msg.StampTime = json.StampTime;
                msg.Type = json.Type;
                msg.DeviceID = json.DeviceID;
                msg.Latitude = json.Latitude;
                msg.Longtitude = json.Longtitude;
                msg.Altitude = json.Altitude;
                msg.Address = json.Address;

                var m = new TimeOutIn
                {
                    ObjectID = json.ObjectID,
                    StampTime = json.StampTime,
                    Type = json.Type,
                    DeviceID = json.DeviceID,
                    Latitude = json.Latitude,
                    Longtitude = json.Longtitude,
                    Altitude = json.Altitude,
                    Address = json.Address
                };

                TimeOutInService.Add(m);
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