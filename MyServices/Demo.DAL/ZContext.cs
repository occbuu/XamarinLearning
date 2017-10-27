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
 * Database context
 **************************************************************************************************************/
#endregion
#endregion

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Demo.DAL
{
    using Entities;
    using UTL;

    /// <summary>
    /// Database context
    /// </summary>
    public class ZContext : DbContext
    {
        #region -- Overrides --

        /// <summary>
        /// Model creating
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Default constructor
        /// </summary>
        public ZContext() : base(DefaultValue.ConfigDB) { }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        #endregion
    }
}