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
 * Generic repository
 **************************************************************************************************************/
#endregion
#endregion

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.DAL.Generics
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="C">DbContext class type</typeparam>
    /// <typeparam name="T">Entity class type</typeparam>
    public class GenericRep<C, T> : IGenericRep<T> where T : class where C : DbContext, new()
    {
        #region -- Implements --

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Return the object</returns>
        public virtual object Add(T entity)
        {
            var a = _entities.Set<T>().Add(entity);
            return a;
        }

        /// <summary>
        /// Delete the entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public virtual object Delete(long id)
        {
            return null;
        }

        /// <summary>
        /// Edit the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Return the object</returns>
        public virtual object Edit(T entity)
        {
            var a = _entities.Entry(entity).State = EntityState.Modified;
            return a;
        }

        /// <summary>
        /// Find by
        /// </summary>
        /// <param name="pre">Predicate</param>
        /// <returns>Return query data</returns>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> pre)
        {
            var a = _entities.Set<T>().Where(pre);
            return a;
        }

        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns>Return query data</returns>
        public virtual IQueryable<T> GetAll()
        {
            var a = _entities.Set<T>();
            return a;
        }

        /// <summary>
        /// Get single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public virtual T GetSingle(long id)
        {
            return null;
        }

        /// <summary>
        /// Get single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>
        public virtual T GetSingle(string code)
        {
            return null;
        }

        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <param name="table">Table name</param>
        /// <returns>Number of affect</returns>
        public virtual int Remove(long id, string table)
        {
            var s = string.Format(S_DeleteId, table, id);
            var a = ExecuteSqlCommand(s);
            return a;
        }

        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public virtual int Remove(long id)
        {
            return -1;
        }

        /// <summary>
        /// Restore the entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public virtual object Restore(long id)
        {
            return null;
        }

        /// <summary>
        /// Save data
        /// </summary>
        /// <returns>Number of affective</returns>
        public virtual int Save()
        {
            var a = _entities.SaveChanges();
            return a;
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Restore the entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <param name="table">Table name</param>
        /// <param name="schema">Schema name</param>
        /// <returns>Return the affect</returns>
        public virtual int Restore(long id, string table, string schema)
        {
            var s = string.Format(S_Restore, id, table, schema);
            var a = ExecuteSqlCommand(s);
            return a;
        }

        /// <summary>
        /// Get all history data
        /// </summary>
        /// <returns>Return query data</returns>
        public virtual IQueryable<T> GetHistory()
        {
            return null;
        }

        /// <summary>
        /// Get history data by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return query data</returns>
        public virtual IQueryable<T> GetHistory(long id)
        {
            return null;
        }

        /// <summary>
        /// Get history data by secondary key
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return query data</returns>
        public virtual IQueryable<T> GetHistory(string code)
        {
            return null;
        }

        /// <summary>
        /// Edit the entity
        /// </summary>
        /// <param name="old">The old entity</param>
        /// <param name="new">The new entity</param>
        protected object Edit(T old, T @new)
        {
            _entities.Entry(old).State = EntityState.Modified;
            var a = _entities.Set<T>().Add(@new);
            return a;
        }

        /// <summary>
        /// Delete the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        protected void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Delete all data of table
        /// </summary>
        /// <param name="table">Table name</param>
        public int Delete(string table)
        {
            var a = string.Format(S_Delete, table);
            var b = ExecuteSqlCommand(a);
            return b;
        }

        /// <summary>
        /// Execute SQL command
        /// </summary>
        /// <param name="sql">The SQL command</param>
        /// <param name="params">The parameters</param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, params object[] @params)
        {
            var a = Context.Database.ExecuteSqlCommand(sql, @params);
            return a;
        }

        /// <summary>
        /// Execute SQL query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL query</param>
        /// <param name="params">The parameters</param>
        /// <returns></returns>
        public List<TT> ExecuteSqlQuery<TT>(string sql, params object[] @params)
        {
            var a = Context.Database.SqlQuery<TT>(sql, @params);
            var b = a.ToList();
            return b;
        }

        /// <summary>
        /// Get current date time from SQL Server
        /// </summary>
        /// <returns>Return the date time</returns>
        public DateTime GetDate()
        {
            try
            {
                var a = Context.Database.SqlQuery<DateTime>(S_GetDate).First();
                return a;
            }
            catch { return DateTime.Now; }
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// The database context
        /// </summary>
        protected C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        /// <summary>
        /// Is connection
        /// </summary>
        public bool IsConnection
        {
            get
            {
                try
                {
                    var a = Context.Database.Connection.Database;
                    var b = Context.Database.Connection.ConnectionString.Replace(a, S_Master);
                    var c = new SqlConnection(b);
                    c.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// The entities
        /// </summary>
        private C _entities = new C();

        #endregion

        #region -- Constants --

        /// <summary>
        /// Master database name
        /// </summary>
        private const string S_Master = "master";

        /// <summary>
        /// Procedure restore data deleted
        /// </summary>
        private const string S_Restore = "sp_Restore {0}, '{1}', '{2}'";

        /// <summary>
        /// Delete command
        /// </summary>
        private const string S_Delete = "delete {0}";

        /// <summary>
        /// Delete command by id
        /// </summary>
        private const string S_DeleteId = "delete {0} where Id = {1}";

        /// <summary>
        /// Get date time
        /// </summary>
        private const string S_GetDate = "select getdate()";

        #endregion
    }
}