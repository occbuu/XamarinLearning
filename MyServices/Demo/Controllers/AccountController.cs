using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Demo.Controllers
{
    using Models;
    using Providers;
    using Results;

    /// <summary>
    /// Account controller
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : BaseController
    {
        #region -- Overrides --

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public AccountController() { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="userManager">User manager</param>
        /// <param name="accessTokenFormat">Access token format</param>
        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        /// <summary>
        /// Get user info
        /// GET api/Account/UserInfo
        /// </summary>
        /// <returns>Return the result</returns>
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoVM GetUserInfo()
        {
            var login = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoVM
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = login == null,
                LoginProvider = login != null ? login.LoginProvider : null
            };
        }

        /// <summary>
        /// Logout
        /// POST api/Account/Logout
        /// </summary>
        /// <returns>Return the result</returns>
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        /// <summary>
        /// Get manage info
        /// GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <param name="generateState">Generate state</param>
        /// <returns>Return the result</returns>
        [Route("ManageInfo")]
        public async Task<ManageInfoVM> GetManageInfo(string returnUrl,
            bool generateState = false)
        {
            var user = await UserManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return null;
            }

            var logins = new List<UserLoginInfoVM>();
            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoVM
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoVM
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoVM
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        /// <summary>
        /// Change password
        /// POST api/Account/ChangePassword
        /// </summary>
        /// <param name="m">Model</param>
        /// <returns>Return the result</returns>
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBM m)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await UserManager.ChangePasswordAsync(UserId,
                m.OldPassword, m.NewPassword);

            if (!res.Succeeded)
            {
                return GetErrorResult(res);
            }

            return Ok();
        }

        /// <summary>
        /// Set password
        /// POST api/Account/SetPassword
        /// </summary>
        /// <param name="m">Model</param>
        /// <returns>Return the result</returns>
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBM m)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await UserManager.AddPasswordAsync(UserId, m.NewPassword);

            if (!res.Succeeded)
            {
                return GetErrorResult(res);
            }

            return Ok();
        }

        /// <summary>
        /// Add external login
        /// POST api/Account/AddExternalLogin
        /// </summary>
        /// <param name="m">Model</param>
        /// <returns>Return the result</returns>
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBM m)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            var ticket = AccessTokenFormat.Unprotect(m.ExternalAccessToken);
            if (ticket == null || ticket.Identity == null
                || (ticket.Properties != null
                    && ticket.Properties.ExpiresUtc.HasValue
                    && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            var login = ExternalLoginData.FromIdentity(ticket.Identity);

            if (login == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            var res = await UserManager.AddLoginAsync(UserId,
                new UserLoginInfo(login.LoginProvider, login.ProviderKey));

            if (!res.Succeeded)
            {
                return GetErrorResult(res);
            }

            return Ok();
        }

        /// <summary>
        /// Remove login
        /// POST api/Account/RemoveLogin
        /// </summary>
        /// <param name="m">Model</param>
        /// <returns>Return the result</returns>
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBM m)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult res;
            if (m.LoginProvider == LocalLoginProvider)
            {
                res = await UserManager.RemovePasswordAsync(UserId);
            }
            else
            {
                res = await UserManager.RemoveLoginAsync(UserId,
                    new UserLoginInfo(m.LoginProvider, m.ProviderKey));
            }

            if (!res.Succeeded)
            {
                return GetErrorResult(res);
            }

            return Ok();
        }

        /// <summary>
        /// Get external login
        /// GET api/Account/ExternalLogin
        /// </summary>
        /// <param name="provider">Provider</param>
        /// <param name="error">Error</param>
        /// <returns>Return the result</returns>
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            var login = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            if (login == null)
            {
                return InternalServerError();
            }

            if (login.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            var x = new UserLoginInfo(login.LoginProvider, login.ProviderKey);
            var user = await UserManager.FindAsync(x);
            var hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                var oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                var cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);
                var properties = ApplicationOAuthProvider.CreateProperties(user.UserName);

                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                var claims = login.GetClaims();
                var identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        /// <summary>
        /// Get external logins
        /// GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <param name="generateState">Generate state</param>
        /// <returns>Return the result</returns>
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginVM> GetExternalLogins(string returnUrl,
            bool generateState = false)
        {
            var descriptions = Authentication.GetExternalAuthenticationTypes();
            var logins = new List<ExternalLoginVM>();
            string state;

            if (generateState)
            {
                state = RandomOAuthStateGenerator.Generate(StrengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (var description in descriptions)
            {
                var vm = new ExternalLoginVM
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(vm);
            }

            return logins;
        }

        /*
            {
              "Email": "nvt87x@gmail.com",
              "Password": "P@ssword123",
              "ConfirmPassword": "P@ssword123"
            }
        */
        /// <summary>
        /// Register
        /// POST api/Account/Register
        /// </summary>
        /// <param name="m">Model</param>
        /// <returns>Return the result</returns>
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBM m)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = m.Email, Email = m.Email };
            var res = await UserManager.CreateAsync(user, m.Password);

            if (!res.Succeeded)
            {
                return GetErrorResult(res);
            }

            return Ok();
        }

        /// <summary>
        /// Register external
        /// POST api/Account/RegisterExternal
        /// </summary>
        /// <param name="m">Model</param>
        /// <returns>Return the result</returns>
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBM m)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = m.Email, Email = m.Email };

            var res = await UserManager.CreateAsync(user);
            if (!res.Succeeded)
            {
                return GetErrorResult(res);
            }

            res = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!res.Succeeded)
            {
                return GetErrorResult(res);
            }

            return Ok();
        }

        /// <summary>
        /// Get error result
        /// </summary>
        /// <param name="result">Result</param>
        /// <returns>Return the result</returns>
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send,
                    // so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// User manager
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager
                    ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary>
        /// Access token format
        /// </summary>
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        /// <summary>
        /// Authentication
        /// </summary>
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        /// <summary>
        /// User identity
        /// </summary>
        private string UserId { get { return User.Identity.GetUserId(); } }

        #endregion

        #region -- Fields --

        /// <summary>
        /// User manager
        /// </summary>
        private ApplicationUserManager _userManager;

        #endregion

        #region -- Constants --

        /// <summary>
        /// Local login provider
        /// </summary>
        private const string LocalLoginProvider = "Local";

        /// <summary>
        /// Strength in bits
        /// </summary>
        private const int StrengthInBits = 256;

        #endregion

        #region -- Helpers --

        /// <summary>
        /// External login data
        /// </summary>
        private class ExternalLoginData
        {
            #region -- Methods --

            /// <summary>
            /// Get claims
            /// </summary>
            /// <returns>Return the result</returns>
            public IList<Claim> GetClaims()
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            /// <summary>
            /// From identity
            /// </summary>
            /// <param name="identity">identity</param>
            /// <returns>Return the result</returns>
            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                var providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null
                    || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }

            #endregion

            #region -- Properties --

            /// <summary>
            /// Login provider
            /// </summary>
            public string LoginProvider { get; set; }

            /// <summary>
            /// Provider key
            /// </summary>
            public string ProviderKey { get; set; }

            /// <summary>
            /// User name
            /// </summary>
            public string UserName { get; set; }

            #endregion
        }

        /// <summary>
        /// Random OAuth state generator
        /// </summary>
        private static class RandomOAuthStateGenerator
        {
            #region -- Methods --

            /// <summary>
            /// Generate
            /// </summary>
            /// <param name="strengthInBits">Strength in bits</param>
            /// <returns>Return the result</returns>
            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    var msg = "strengthInBits must be evenly divisible by 8.";
                    throw new ArgumentException(msg, "strengthInBits");
                }

                var strengthInBytes = strengthInBits / bitsPerByte;

                var data = new byte[strengthInBytes];
                _random.GetBytes(data);

                return HttpServerUtility.UrlTokenEncode(data);
            }

            #endregion

            #region -- Fields --

            /// <summary>
            /// Random
            /// </summary>
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            #endregion
        }

        #endregion
    }
}