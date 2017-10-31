using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Demo.Services
{
    /// <summary>
    /// Base service
    /// </summary>
    /// <typeparam name="T">Model class type</typeparam>
    public class BaseService<T> : IService<T> where T : class
    {
        #region -- Implements --

        public Task<bool> AddAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Create data
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Return the result</returns>
        protected StringContent CreateData(object data)
        {
            var jo = JsonConvert.SerializeObject(data);
            var res = new StringContent(jo);
            res.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return res;
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// Host
        /// </summary>
        protected const string Host = "http://occapp.ddns.net:9696/api/";

        #endregion
    }
}