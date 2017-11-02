namespace Demo.Models
{
    /// <summary>
    /// Register model
    /// </summary>
    public class RegisterModel : BaseModel
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public RegisterModel() { }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        public string ConfirmPassword { get; set; }

        #endregion
    }
}