using System;

namespace Demo.Models
{
    /// <summary>
    /// File model
    /// </summary>
    public class FileModel : BaseModel
    {
        #region -- Properties --

        /// <summary>
        /// Object ID
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// Image base 64
        /// </summary>
        public string Image64 { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Date upload
        /// </summary>
        public DateTime? DateUpload { get; set; }

        /// <summary>
        /// Date download
        /// </summary>
        public DateTime? DateDownload { get; set; }

        #endregion
    }
}