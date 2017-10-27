#region Information
/*
 * Author       : Zng Tfy
 * Email        : nvt87x@gmail.com
 * Phone        : +84 1645 515 010
 * ------------------------------- *
 * Create       : 26/10/2017 16:42
 * Update       : 26/10/2017 16:42
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
 * Base controller
 **************************************************************************************************************/
#endregion
#endregion

using System.Web.Http;

namespace Demo.WebApi.Controllers
{
    using DAL;
    using UTL;

    /// <summary>
    /// Base controller
    /// </summary>
    public class BaseController : ApiController
    {
        #region -- Properties --

        /// <summary>
        /// User repository
        /// </summary>
        public static UserRep UserRep { get { return ZConverts.Xingleton<UserRep>(); } }

        #endregion
    }
}