using System;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.BLL
{
    using Utility;

    /// <summary>
    /// Repository Business Logic Layer (BLL)
    /// </summary>
    /// <typeparam name="D">Class DAL type</typeparam>
    /// <typeparam name="T">Class entity type</typeparam>
    public class Repository<D, T> : IRepository<T> where T : class where D : DAL.Repository<T>, new()
    {
        #region -- Implement --

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Return the result</returns>
        public T GetById(int id)
        {
            var res = _dao.GetById(id);
            return res;
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Add(T entity)
        {
            var tmp = entity.GetPropertyValue("CreatedBy");
            if (tmp != null)
            {
                var now = DateTime.Now;
                entity.SetPropertyValue("CreatedDate", now);
                entity.SetPropertyValue("ModifiedBy", tmp);
                entity.SetPropertyValue("ModifiedDate", now);
            }

            tmp = entity.GetPropertyValue("IsDelete");
            if (tmp != null)
            {
                entity.SetPropertyValue("IsDelete", false);
            }

            _dao.Insert(entity);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Delete(T entity)
        {
            _dao.Delete(entity);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Update(T entity)
        {
            var tmp = entity.GetPropertyValue("ModifiedBy");
            if (tmp != null)
            {
                entity.SetPropertyValue("ModifiedDate", DateTime.Now);
            }

            _dao.Update(entity);
        }

        /// <summary>
        /// Search for
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>Return the result</returns>
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            var res = _dao.SearchFor(predicate);
            return res;
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// Main DAL
        /// </summary>
        protected D _dao = new D();

        #endregion
    }
}