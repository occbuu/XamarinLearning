using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.DAL
{
    using DBContext;

    /// <summary>
    /// Repository Data Access Layer (DAL)
    /// </summary>
    /// <typeparam name="T">Class entity type</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        #region -- Implement --

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Return the result</returns>
        public T GetById(object id)
        {
            var res = Entities.Find(id);
            return res;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                SetUserID();
                Entities.Add(entity);
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var item in dbEx.EntityValidationErrors)
                {
                    var entry = item.Entry;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;

                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;

                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                var msg = dbEx.EntityValidationErrors
                    .SelectMany(validationErrors => validationErrors.ValidationErrors)
                    .Aggregate(string.Empty, (current, validationError)
                        => current + (string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage)
                        + Environment.NewLine));
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                SetUserID();
                Entities.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var item in dbEx.EntityValidationErrors)
                {
                    var entry = item.Entry;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;

                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;

                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                var msg = dbEx.EntityValidationErrors
                    .SelectMany(validationErrors => validationErrors.ValidationErrors)
                    .Aggregate(string.Empty, (current, validationError)
                        => current + (string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage)
                        + Environment.NewLine));
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                SetUserID();
                Entities.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var item in dbEx.EntityValidationErrors)
                {
                    var entry = item.Entry;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;

                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;

                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                var msg = dbEx.EntityValidationErrors
                    .SelectMany(validationErrors => validationErrors.ValidationErrors)
                    .Aggregate(string.Empty, (current, validationError)
                        => current + (string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage)
                        + Environment.NewLine));
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        /// <summary>
        /// Search for
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>Return the result</returns>
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                {
                    throw new ArgumentNullException("entity");
                }

                var res = Entities.Where(predicate);
                return res;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors
                  .SelectMany(validationErrors => validationErrors.ValidationErrors)
                  .Aggregate(string.Empty, (current, validationError)
                      => current + (string.Format("Property: {0} Error: {1}",
                      validationError.PropertyName, validationError.ErrorMessage)
                      + Environment.NewLine));
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Return the result</returns>
        public string ExecuteScalar(string query, object[] parameters)
        {
            var tmp = DbContext.Database.SqlQuery<string>(query, parameters).ToList();
            var res = tmp.Count > 0 ? tmp[0] : string.Empty;
            return res;
        }

        /// <summary>
        /// Execute stored procedure
        /// </summary>
        /// <param name="sp">Stored procedure name</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Return the result</returns>
        public List<T> ExecuteStoredProcedure(string sp, object[] parameters)
        {
            var res = DbContext.Database.SqlQuery<T>(sp, parameters).ToList<T>();
            return res;
        }

        /// <summary>
        /// Execute SQL query
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Return the result</returns>
        public List<T> ExecuteSQLQuery(string query, object[] parameters)
        {
            var res = DbContext.Database.SqlQuery<T>(query, parameters).ToList<T>();
            return res;
        }

        /// <summary>
        /// Set UserID
        /// </summary>
        /// <param name="context">DB context</param>
        private void SetUserID(DbContext context = null)
        {
            try
            {
                DbConnection connection;

                if (context == null)
                {
                    connection = DbContext.Database.Connection;
                }
                else
                {
                    connection = context.Database.Connection;
                }

                if (connection.State == ConnectionState.Closed
                    || connection.State == ConnectionState.Broken)
                {
                    connection.Open();
                }
            }
            catch
            {
                //TODO
            }
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Get all
        /// </summary>
        public virtual List<T> GetAll
        {
            get
            {
                var res = Entities.ToList<T>();
                return res;
            }
        }

        /// <summary>
        /// DB context
        /// </summary>
        protected PSNSPEntities DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = new PSNSPEntities();
                }

                return _dbContext;
            }

            private set
            {
                _dbContext = value;
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        private IDbSet<T> Entities
        {
            get
            {
                var res = _entities ?? (_entities = DbContext.Set<T>());
                return res;
            }
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// Entities
        /// </summary>
        private IDbSet<T> _entities;

        /// <summary>
        /// DB context
        /// </summary>
        private PSNSPEntities _dbContext;

        #endregion
    }
}