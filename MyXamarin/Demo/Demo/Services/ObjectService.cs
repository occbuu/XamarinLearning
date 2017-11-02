using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Services
{
    using Models;

    /// <summary>
    /// Object service
    /// </summary>
    public class ObjectService : BaseService<ObjectModel>
    {
        #region -- Overrides --

        /// <summary>
        /// Get all data
        /// </summary>
        /// <param name="refresh">Force refresh</param>
        /// <returns>Return the result</returns>
        public override async Task<IEnumerable<ObjectModel>> GetAllAsync(bool refresh = false)
        {
            var client = CreateClient();
            var json = await client.GetStringAsync(Host + "api/Object/GetAll");
            var res = JsonConvert.DeserializeObject<IEnumerable<ObjectModel>>(json);
            return res;
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public ObjectService() { }

        #endregion
    }
}