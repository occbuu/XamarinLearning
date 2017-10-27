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
 * User data store
 **************************************************************************************************************/
#endregion
#endregion

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.Services
{
    using Models;

    /// <summary>
    /// User data store
    /// </summary>
    public class UserDataStore : IDataStore<Item>
    {
        #region -- Implements --

        public async Task<bool> AddItemAsync(Item item)
        {
            //TODO
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            //TODO
            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            //TODO
            return await Task.FromResult(new Item());
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync(Host + "User/GetAll");
            var res = JsonConvert.DeserializeObject<IEnumerable<Item>>(json);
            return res;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            //TODO
            return await Task.FromResult(true);
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// Host
        /// </summary>
        private string Host = "http://192.168.1.179/ms/api/";

        #endregion
    }
}