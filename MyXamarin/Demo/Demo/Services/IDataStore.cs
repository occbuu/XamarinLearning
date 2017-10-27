#region Information
/*
 * Author       : Zng Tfy
 * Email        : nvt87x@gmail.com
 * Phone        : +84 1645 515 010
 * ------------------------------- *
 * Create       : 27/10/2017 08:58
 * Update       : 27/10/2017 08:58
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
 * Interface data store
 **************************************************************************************************************/
#endregion
#endregion

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Services
{
    /// <summary>
    /// Interface data store
    /// </summary>
    /// <typeparam name="T">Entity class type</typeparam>
    public interface IDataStore<T>
    {
        #region -- Methods --

        Task<bool> AddItemAsync(T item);

        Task<bool> UpdateItemAsync(T item);

        Task<bool> DeleteItemAsync(string id);

        Task<T> GetItemAsync(string id);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        #endregion
    }
}