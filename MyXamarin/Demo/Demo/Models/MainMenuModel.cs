using System;

namespace Demo.Models
{
    using Views;

    /// <summary>
    /// Menu model
    /// </summary>
    public class MainMenuModel
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public MainMenuModel()
        {
            TargetType = typeof(MainMenuDetail);
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Target type
        /// </summary>
        public Type TargetType { get; set; }

        #endregion
    }
}