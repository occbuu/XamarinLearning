using System.Linq;

namespace Demo.BLL
{
    using DAL;
    using DAL.DBContext;

    /// <summary>
    /// Object service
    /// </summary>
    public class ObjectService : Repository<ObjectDao, Object>
    {
        #region -- Methods --

        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns>Return the result</returns>
        public IQueryable<Object> GetAll()
        {
            var res = _dao.SearchFor(p => true);
            return res;
        }

        #endregion
    }
}