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
 * User
 **************************************************************************************************************/
#endregion
#endregion

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entities
{
    using Models;
    using UTL;

    /// <summary>
    /// User
    /// </summary>
    [Table(nameof(User), Schema = DefaultValue.SchemaSkg)]
    [Serializable]
    public class User : Zinfors
    {
        #region -- Methods --

        /// <summary>
        /// Default
        /// </summary>
        public User()
        {
            Never = true; // password never expires
            Locked = true; // user is locked
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Account
        /// </summary>
        [StringLength(32)]
        public string Acc { set; get; }

        /// <summary>
        /// Password
        /// </summary>
        [StringLength(128)]
        public string Pass { set; get; }

        /// <summary>
        /// Date time login
        /// </summary>
        public DateTime? LogIn { set; get; }

        /// <summary>
        /// Date time logout
        /// </summary>
        public DateTime? LogOut { set; get; }

        /// <summary>
        /// Cannot change password
        /// </summary>
        public bool Cannot { set; get; }

        /// <summary>
        /// Never expires
        /// </summary>
        public bool Never { set; get; }

        /// <summary>
        /// Account is locked out
        /// </summary>
        public bool Locked { set; get; }

        #endregion
    }
}