using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    #region -- Binding --

    /// <summary>
    /// Add external login binding model
    /// </summary>
    public class AddExternalLoginBM
    {
        #region -- Properties --

        /// <summary>
        /// External access token
        /// </summary>
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }

        #endregion
    }

    /// <summary>
    /// Change password binding model
    /// </summary>
    public class ChangePasswordBM
    {
        #region -- Properties --

        /// <summary>
        /// Old password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// New password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        #endregion
    }

    /// <summary>
    /// Register binding model
    /// </summary>
    public class RegisterBM
    {
        #region -- Properties --

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        #endregion
    }

    /// <summary>
    /// Register external binding model
    /// </summary>
    public class RegisterExternalBM
    {
        #region -- Properties --

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        #endregion
    }

    /// <summary>
    /// Remove login binding model
    /// </summary>
    public class RemoveLoginBM
    {
        #region -- Properties --

        /// <summary>
        /// Login provider
        /// </summary>
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        /// <summary>
        /// Provider key
        /// </summary>
        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }

        #endregion
    }

    /// <summary>
    /// Set password binding model
    /// </summary>
    public class SetPasswordBM
    {
        #region -- Properties --

        /// <summary>
        /// New password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        #endregion
    }

    #endregion

    #region -- View --

    /// <summary>
    /// External login view model
    /// </summary>
    public class ExternalLoginVM
    {
        #region -- Properties --

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }

        #endregion
    }

    /// <summary>
    /// Manage info view model
    /// </summary>
    public class ManageInfoVM
    {
        #region -- Properties --

        /// <summary>
        /// Local login provider
        /// </summary>
        public string LocalLoginProvider { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Logins
        /// </summary>
        public IEnumerable<UserLoginInfoVM> Logins { get; set; }

        /// <summary>
        /// External login providers
        /// </summary>
        public IEnumerable<ExternalLoginVM> ExternalLoginProviders { get; set; }

        #endregion
    }

    /// <summary>
    /// User info view model
    /// </summary>
    public class UserInfoVM
    {
        #region -- Properties --

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Has registered
        /// </summary>
        public bool HasRegistered { get; set; }

        /// <summary>
        /// Login provider
        /// </summary>
        public string LoginProvider { get; set; }

        #endregion
    }

    /// <summary>
    /// User login info view model
    /// </summary>
    public class UserLoginInfoVM
    {
        #region -- Properties --

        /// <summary>
        /// Login provider
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Provider key
        /// </summary>
        public string ProviderKey { get; set; }

        #endregion
    }

    #endregion
}