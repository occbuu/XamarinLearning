#region Information
/*
 * Author       : Zng Tfy
 * Email        : nvt87x@gmail.com
 * Phone        : +84 1645 515 010
 * ------------------------------- *
 * Create       : 26/10/2017 16:42
 * Update       : 26/10/2017 16:42
 * Checklist    : 1.0
 * Status       : OK
 */
#region License
/**************************************************************************************************************
 * Copyright © 2012-2017 SKG™ all rights reserved
 **************************************************************************************************************/
#endregion
#region Description
/**************************************************************************************************************
 * Basic information
 **************************************************************************************************************/
#endregion
#endregion

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Models
{
    /// <summary>
    /// Basic information
    /// </summary>
    [Serializable]
    public abstract class Zinfors
    {
        #region -- Methods --

        /// <summary>
        /// Default
        /// </summary>
        public Zinfors()
        {
            Order = 0; // not sort order
        }

        /// <summary>
        /// Copy this instance
        /// </summary>
        /// <typeparam name="T">The class</typeparam>
        /// <returns>Return the instance</returns>
        public T Copy<T>()
        {
            return (T)MemberwiseClone();
        }

        #endregion

        #region -- Properties --

        #region Key
        /// <summary>
        /// Primary key
        /// </summary>
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }

        /// <summary>
        /// Secondary key
        /// </summary>
        [Required, StringLength(96)]
        public string Code { set; get; }
        #endregion

        #region Data
        /// <summary>
        /// Content is shown
        /// </summary>
        public string Text { set; get; }

        /// <summary>
        /// Descriptive detailing
        /// </summary>
        public string Note { set; get; }

        /// <summary>
        /// More information
        /// </summary>
        public string More { set; get; }
        #endregion

        #region Write log for audit
        /// <summary>
        /// Inserter record
        /// </summary>
        public long? Inserter { set; get; }

        /// <summary>
        /// Insert date time
        /// </summary>
        public DateTime? InsertDate { set; get; }

        /// <summary>
        /// Updater record
        /// </summary>
        public long? Updater { set; get; }

        /// <summary>
        /// Update date time
        /// </summary>
        public DateTime? UpdateDate { set; get; }

        /// <summary>
        /// Deleter record
        /// </summary>
        public long? Deleter { set; get; }

        /// <summary>
        /// Delete date time
        /// </summary>
        public DateTime? DeleteDate { set; get; }
        #endregion

        #region Status of record
        /// <summary>
        /// Deleted record
        /// </summary>
        public bool Deleted { set; get; }

        /// <summary>
        /// Sort order to display
        /// </summary>
        public int Order { set; get; }
        #endregion

        #endregion
    }
}