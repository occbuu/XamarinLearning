using System;

namespace Demo.Models
{
    /// <summary>
    /// Object model
    /// </summary>
    public class ObjectModel : BaseModel
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public ObjectModel() { }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Object ID
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// Full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// People ID
        /// </summary>
        public string PID { get; set; }

        /// <summary>
        /// People ID date
        /// </summary>
        public DateTime? PIDDate { get; set; }

        /// <summary>
        /// People ID issue
        /// </summary>
        public string PIDIssue { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        public DateTime? DoB { get; set; }

        /// <summary>
        /// Place of birth
        /// </summary>
        public string PoB { get; set; }

        /// <summary>
        /// PerAdd
        /// </summary>
        public string PerAdd { get; set; }

        /// <summary>
        /// TemAdd
        /// </summary>
        public string TemAdd { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// Telephone
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        #endregion
    }
}