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
 * Interface generic repository
 **************************************************************************************************************/
#endregion
#endregion

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.DAL.Generics
{
    /// <summary>
    /// Interface generic repository
    /// </summary>
    /// <typeparam name="T">Entity class type</typeparam>
    public interface IGenericRep<T> where T : class
    {
        #region -- Methods --

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Return the object</returns>
        object Add(T entity);

        /// <summary>
        /// Delete the entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        object Delete(long id);

        /// <summary>
        /// Edit the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Return the object</returns>
        object Edit(T entity);

        /// <summary>
        /// Find by
        /// </summary>
        /// <param name="pre">Predicate</param>
        /// <returns>Return query data</returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> pre);

        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns>Return query data</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        T GetSingle(long id);

        /// <summary>
        /// Get single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>
        T GetSingle(string code);

        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <param name="table">Table name</param>
        /// <returns>Number of affect</returns>
        int Remove(long id, string table);

        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        int Remove(long id);

        /// <summary>
        /// Restore the entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        object Restore(long id);

        /// <summary>
        /// Save data
        /// </summary>
        /// <returns>Number of affective</returns>
        int Save();

        #endregion
    }
}