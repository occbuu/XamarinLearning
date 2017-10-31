using System;

namespace Demo.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class UserModel : BaseModel
    {
        #region -- Properties --

        /// <summary>
        /// User account
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Pass { get; set; }

        /// <summary>
        /// Object ID
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// Last log in
        /// </summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Log in name
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// Role ID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// Manage group
        /// </summary>
        public string ManageGroup { get; set; }

        #endregion
    }
}