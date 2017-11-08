using Newtonsoft.Json;

namespace Demo.Models
{
    /// <summary>
    /// User model
    /// </summary>
    [JsonObject]
    public class UserModel
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public UserModel() { }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Identify
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Verified email
        /// </summary>
        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Given name
        /// </summary>
        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        /// <summary>
        /// Family name
        /// </summary>
        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        /// <summary>
        /// Link
        /// </summary>
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// Picture
        /// </summary>
        [JsonProperty("picture")]
        public string Picture { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        #endregion
    }
}