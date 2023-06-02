using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class FileManagerController : BaseController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index()
        {
            using (FileService file = new FileService())
            {
                PrgService.SetAction(enAction.Index, enCardSize.Max);
                ActionService.PathName = "~/Images/User";
                vmFileManager model = new vmFileManager();
                model.PathName = ActionService.PathName;
                model.FileInfoList = file.GetFileInfoList(ActionService.PathName);
                return View(model);
            }
        }


    }
}