using System;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.DAL
{
    /// <summary>
    /// Interface repository Data Access Layer (DAL)
    /// </summary>
    /// <typeparam name="T">Class entity type</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Return the result</returns>
        T GetById(object id);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Insert(T entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Update(T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Delete(T entity);

        /// <summary>
        /// Search for
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>Return the result</returns>
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
    }
}