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

        Task<bool> AddAsync(T item);

        Task<bool> UpdateAsync(T item);

        Task<bool> DeleteAsync(string id);

        Task<T> GetAsync(string id);

        Task<IEnumerable<T>> GetAllAsync(bool forceRefresh = false);

        #endregion
    }
}