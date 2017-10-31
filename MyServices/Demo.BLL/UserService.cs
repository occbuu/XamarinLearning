using System.Linq;

namespace Demo.BLL
{
    using DAL;
    using DAL.DBContext;

    /// <summary>
    /// User service
    /// </summary>
    public class UserService : Repository<UserDao, User>
    {
        #region -- Methods --

        /// <summary>
        /// Check log in
        /// </summary>
        /// <param name="user">User account</param>
        /// <param name="pass">Password</param>
        /// <returns>Return the result</returns>
        public User LogIn(string user, string pass)
        {
            var m = _dao.SearchFor(p => p.UserID == user).FirstOrDefault();

            if (m != null)
            {
                //TODO
                /*/ Check password
                if (m.PWD != pass)
                {
                    m = null;
                }*/
            }

            return m;
        }

        #endregion
    }
}