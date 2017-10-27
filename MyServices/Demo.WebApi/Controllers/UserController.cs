#region Information
/*
 * Author       : Zng Tfy
 * Email        : nvt87x@gmail.com
 * Phone        : +84 1645 515 010
 * ------------------------------- *
 * Create       : 26/10/2017 16:42
 * Update       : 27/10/2017 08:18
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
 * User controller
 **************************************************************************************************************/
#endregion
#endregion

using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Demo.WebApi.Controllers
{
    using DAL;
    using Models;

    /// <summary>
    /// User controller
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetAll()
        {
            try
            {
                var tmp = UserRep.GetAll();
                var data = tmp
                    .Select(x => new Item
                    {
                        Id = x.Id + string.Empty,
                        Text = x.Acc,
                        Description = x.Note,
                        LogIn = x.LogIn,
                        Order = x.Order
                    })
                    .ToList();

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}