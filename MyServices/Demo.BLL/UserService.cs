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