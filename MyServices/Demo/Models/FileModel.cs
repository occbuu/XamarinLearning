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