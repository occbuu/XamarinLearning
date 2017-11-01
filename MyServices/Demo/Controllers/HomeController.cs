using System.Web.Mvc;

namespace Demo.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {
        #region -- Methods --

        /// <summary>
        /// Index
        /// </summary>
        /// <returns>Return the result</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        #endregion
    }
}