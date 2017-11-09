using System;

namespace Demo.Models
{
    /// <summary>
    /// Menu model
    /// </summary>
    public class MenuModel
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public MenuModel()
        {
            TargetType = typeof(MenuDetail);
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Identify
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Target type
        /// </summary>
        public Type TargetType { get; set; }

        #endregion
    }
}