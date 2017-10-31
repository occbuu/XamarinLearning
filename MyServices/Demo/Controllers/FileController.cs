using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Demo.Controllers
{
    using Models;

    /// <summary>
    /// File controller
    /// </summary>
    public class FileController : BaseController
    {
        #region -- Methods --

        /* api/File/Upload
        */
        [HttpPost]
        public HttpResponseMessage Upload()
        {
            var msg = new FileModel();

            try
            {
                var req = HttpContext.Current.Request;

                if (req.Files.Count > 0)
                {
                    foreach (string f in req.Files)
                    {
                        var file = req.Files[f];
                        var fileName = file.FileName;
                        fileName = fileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                        var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);
                        file.SaveAs(filePath);

                        msg.URL = "/Uploads/" + fileName;
                        msg.DateUpload = DateTime.Now;
                        msg.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                msg.ErrMsg = ex.Message;
            }

            return Response(msg);
        }

        #endregion
    }
}