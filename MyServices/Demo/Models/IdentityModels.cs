using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo.Models
{
    /// <summary>
    /// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        #region -- Methods --

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager,
            string authenticationType)
        {
            // Note the authenticationType must match
            // the one defined in CookieAuthenticationOptions.AuthenticationType
            var res = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return res;
        }

        #endregion
    }

    /// <summary>
    /// Application DbContext
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <returns>Return the result</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        #endregion
    }
}