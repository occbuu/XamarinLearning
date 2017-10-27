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
 * User repository
 **************************************************************************************************************/
#endregion
#endregion

using System;
using System.Linq;

namespace Demo.DAL
{
    using Entities;
    using Generics;
    using UTL;

    /// <summary>
    /// User repository
    /// </summary>
    public class UserRep : GenericRep<ZContext, User>
    {
        #region -- Overrides --

        /// <summary>
        /// Get single
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the User object</returns>
        public override User GetSingle(long id)
        {
            var query = GetAll()
                .FirstOrDefault(x => x.Id == id);
            return query;
        }

        /// <summary>
        /// Get single
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the User object</returns>
        public override User GetSingle(string code)
        {
            var query = GetAll()
                .FirstOrDefault(x => x.Code == code || x.Acc == code);
            return query;
        }

        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns>Return query data</returns>
        public override IQueryable<User> GetAll()
        {
            var query = base.GetAll()
                .Where(x => x.Deleted == false);
            return query;
        }

        /// <summary>
        /// Get all history data
        /// </summary>
        /// <returns>Return query data</returns>
        public override IQueryable<User> GetHistory()
        {
            var query = base.GetAll()
                .Where(x => x.Deleted == true);
            return query;
        }

        /// <summary>
        /// Get history data by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return query data</returns>
        public override IQueryable<User> GetHistory(long id)
        {
            var query = GetHistory()
                .Where(p => p.Id == id);
            return query;
        }

        /// <summary>
        /// Get history data by secondary key
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return query data</returns>
        public override IQueryable<User> GetHistory(string code)
        {
            var query = GetHistory()
                .Where(p => p.Code == code);
            return query;
        }

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Return the object</returns>
        public override object Add(User entity)
        {
            var e = GetSingle(entity.Acc);
            if (e != null)
            {
                return null; // exists data
            }

            entity.InsertDate = DateTime.Now;
            var a = base.Add(entity);

            Save();
            return a;
        }

        /// <summary>
        /// Edit the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Return the object</returns>
        public override object Edit(User entity)
        {
            var f = entity.Id > 0 ? GetSingle(entity.Id) : GetSingle(entity.Acc);
            var ok = true;

            if (ok)
            {
                entity.Kopy(f);
                f.UpdateDate = DateTime.Now;
                var c = base.Edit(f);

                Save();
                return c;
            }

            return null;
        }

        /// <summary>
        /// Delete the entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override object Delete(long id)
        {
            var entity = GetSingle(id);
            if (entity == null)
            {
                return null; // error
            }

            // Update information deleted
            entity.Deleted = true;
            entity.DeleteDate = DateTime.Now;
            var a = base.Edit(entity);

            Save();
            return a;
        }

        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affective</returns>
        public override int Remove(long id)
        {
            var entity = GetSingle(id);
            if (entity == null)
            {
                return -1; // error
            }

            base.Delete(entity);
            return Save();
        }

        /// <summary>
        /// Restore the entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override object Restore(long id)
        {
            var entity = GetSingle(id);
            if (entity == null)
            {
                return null; // error
            }

            // Update information deleted
            entity.Deleted = false;
            entity.Deleter = null;
            entity.DeleteDate = null;
            var a = base.Edit(entity);

            Save();
            return a;
        }

        #endregion
    }
}