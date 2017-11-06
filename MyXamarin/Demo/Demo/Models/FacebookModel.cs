using Newtonsoft.Json;

namespace Demo.Models
{
    /// <summary>
    /// Facebook model
    /// </summary>
    public class FacebookModel
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public FacebookModel() { }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Identify
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Picture
        /// </summary>
        public Picture Picture { get; set; }

        /// <summary>
        /// Locale
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Link
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Cover
        /// </summary>
        public CoverFacebook Cover { get; set; }

        /// <summary>
        /// Age range
        /// </summary>
        [JsonProperty("age_range")]
        public AgeRange AgeRange { get; set; }

        /// <summary>
        /// Devices
        /// </summary>
        public Device[] Devices { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Is verified
        /// </summary>
        public bool IsVerified { get; set; }

        #endregion
    }

    /// <summary>
    /// Picture
    /// </summary>
    public class Picture
    {
        #region -- Properties --

        /// <summary>
        /// Data
        /// </summary>
        public Data Data { get; set; }

        #endregion
    }

    /// <summary>
    /// Data
    /// </summary>
    public class Data
    {
        #region -- Properties --

        /// <summary>
        /// Is silhouette
        /// </summary>
        public bool IsSilhouette { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        #endregion
    }

    /// <summary>
    /// Cover Facebook
    /// </summary>
    public class CoverFacebook
    {
        #region -- Properties --

        /// <summary>
        /// Identify
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Offset Y
        /// </summary>
        public int OffsetY { get; set; }

        /// <summary>
        /// Source
        /// </summary>
        public string Source { get; set; }

        #endregion
    }

    /// <summary>
    /// Age range
    /// </summary>
    public class AgeRange
    {
        #region -- Properties --

        /// <summary>
        /// Min
        /// </summary>
        public int Min { get; set; }

        #endregion
    }

    /// <summary>
    /// Device
    /// </summary>
    public class Device
    {
        #region -- Properties --

        /// <summary>
        /// OS
        /// </summary>
        public string Os { get; set; }

        #endregion
    }
}