using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Areas.Mis.Controllers
{
    /// <summary>
    /// MCODP002_Repository Repository 產生器
    /// </summary>
    public class MCODP002_RepositoryController : BaseController
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index()
        {
            PrgService.SetAction(enAction.Index, enCardSize.Large);
            PrgService.SetProgram();
            var model = new vmRepositoryModel();
            model.KeyColumn = "Id";
            model.SortColumns = "Id";
            return View(model);
        }

        [HttpPost]
        [LoginAuthorize()]
        public ActionResult Index(vmRepositoryModel model)
        {
            if (!ModelState.IsValid) return View(model);
            using (CodeGenerator code = new CodeGenerator())
            {
                model.TextResult = code.GetRepositoryClass(model);
                TempData["ResultModel"] = model;
                return RedirectToAction("Result");
            }
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Result()
        {
            using (CodeGenerator code = new CodeGenerator())
            {
                vmRepositoryModel model = (vmRepositoryModel)TempData["ResultModel"];
                model.FolderName = code.RepositoryFolderName;
                model.FileName = code.GetRepositoryFileName(model.ClassName);
                return View(model);
            }
        }

        [HttpPost]
        [LoginAuthorize()]
        [ValidateInput(false)]
        public ActionResult Result(vmRepositoryModel model)
        {
            using (CodeGenerator code = new CodeGenerator())
            {
                bool bln_result = code.CreateRepositoryFile(model);
                string str_file_name = code.GetRepositoryClassName(model.ClassName);
                if (bln_result)
                    TempData["ErrorMessage"] = $"{str_file_name} 檔案建立成功!!";
                else
                    TempData["ErrorMessage"] = $"{str_file_name} 檔案已存在,無法建立!!";
                return RedirectToAction("Index");
            }
        }
    }
}