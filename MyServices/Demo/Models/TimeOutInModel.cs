using System;

namespace Demo.Models
{
    /// <summary>
    /// TimeOutIn model
    /// </summary>
    public class TimeOutInModel : BaseModel
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public TimeOutInModel() { }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Object ID
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// Stamp time
        /// </summary>
        public DateTime? StampTime { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Device ID
        /// </summary>
        public string DeviceID { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Longtitude
        /// </summary>
        public string Longtitude { get; set; }

        /// <summary>
        /// Altitude
        /// </summary>
        public string Altitude { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        #endregion
    }
}