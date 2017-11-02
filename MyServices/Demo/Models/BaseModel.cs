namespace Demo.Models
{
    /// <summary>
    /// Base model
    /// </summary>
    public class BaseModel
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public BaseModel() { }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Message status
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrMsg { get; set; }

        #endregion
    }
}