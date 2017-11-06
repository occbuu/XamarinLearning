namespace Demo.Models
{
    /// <summary>
    /// Google model
    /// </summary>
    public class GoogleModel
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public GoogleModel() { }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Identify
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Kind
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// Etag
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// Occupation
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Object type
        /// </summary>
        public string ObjectType { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public Name Name { get; set; }

        /// <summary>
        /// Tag line
        /// </summary>
        public string Tagline { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Image
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// Organizations
        /// </summary>
        public Organization[] Organizations { get; set; }

        /// <summary>
        /// PlacesLived
        /// </summary>
        public Placeslived[] PlacesLived { get; set; }

        /// <summary>
        /// Is plus user
        /// </summary>
        public bool IsPlusUser { get; set; }

        /// <summary>
        /// Circled by count
        /// </summary>
        public int CircledByCount { get; set; }

        /// <summary>
        /// Verified
        /// </summary>
        public bool Verified { get; set; }

        /// <summary>
        /// Cover
        /// </summary>
        public CoverGoogle Cover { get; set; }

        #endregion
    }

    /// <summary>
    /// Name
    /// </summary>
    public class Name
    {
        #region -- Properties --

        /// <summary>
        /// Family name
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Given name
        /// </summary>
        public string GivenName { get; set; }

        #endregion
    }

    public class Image
    {
        #region -- Properties --

        public string Url { get; set; }
        public bool IsDefault { get; set; }

        #endregion
    }

    /// <summary>
    /// Cover Google
    /// </summary>
    public class CoverGoogle
    {
        #region -- Properties --

        /// <summary>
        /// Layout
        /// </summary>
        public string Layout { get; set; }

        /// <summary>
        /// Cover photo
        /// </summary>
        public CoverPhoto CoverPhoto { get; set; }

        /// <summary>
        /// Cover information
        /// </summary>
        public CoverInfo CoverInfo { get; set; }

        #endregion
    }

    /// <summary>
    /// Cover photo
    /// </summary>
    public class CoverPhoto
    {
        #region -- Properties --

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; set; }

        #endregion
    }

    /// <summary>
    /// Cover information
    /// </summary>
    public class CoverInfo
    {
        #region -- Properties --

        /// <summary>
        /// Top image offset
        /// </summary>
        public int TopImageOffset { get; set; }

        /// <summary>
        /// Left image offset
        /// </summary>
        public int LeftImageOffset { get; set; }

        #endregion
    }

    /// <summary>
    /// Organization
    /// </summary>
    public class Organization
    {
        #region -- Properties --

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Start date
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// Primary
        /// </summary>
        public bool Primary { get; set; }

        #endregion
    }

    /// <summary>
    /// Places lived
    /// </summary>
    public class Placeslived
    {
        #region -- Properties --

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Primary
        /// </summary>
        public bool Primary { get; set; }

        #endregion
    }
}