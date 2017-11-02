using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Services
{
    /// <summary>
    /// Interface service
    /// </summary>
    /// <typeparam name="T">Model class type</typeparam>
    public interface IService<T>
    {
        #region -- Methods --

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="m">Model</param>
        /// <returns>Return the result</returns>
        Task<bool> AddAsync(T m);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="m">Model</param>
        /// <returns>Return the result</returns>
        Task<bool> UpdateAsync(T m);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Identity</param>
        /// <returns>Return the result</returns>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="id">Identity</param>
        /// <returns>Return the result</returns>
        Task<T> GetAsync(string id);

        /// <summary>
        /// Get all data
        /// </summary>
        /// <param name="refresh">Force refresh</param>
        /// <returns>Return the result</returns>
        Task<IEnumerable<T>> GetAllAsync(bool refresh = false);

        #endregion
    }
}