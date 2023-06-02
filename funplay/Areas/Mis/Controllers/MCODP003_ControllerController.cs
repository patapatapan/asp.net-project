using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Areas.Mis.Controllers
{
    /// <summary>
    /// MCODP003_Controller Controller 產生器
    /// </summary>
    public class MCODP003_ControllerController : BaseController
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index()
        {
            PrgService.SetAction(enAction.Index, enCardSize.Large);
            PrgService.SetProgram();
            var model = new vmControllerModel();
            model.Id = 1;
            model.AreaName = "User";
            model.ControllerName = "";
            model.ClassName = "";
            model.PrgNo = "";
            model.PrgName = "";
            return View(model);
        }

        [HttpPost]
        [LoginAuthorize()]
        public ActionResult Index(vmControllerModel model)
        {
            if (!ModelState.IsValid) return View(model);
            using (CodeGenerator code = new CodeGenerator())
            {
                using (z_repoPrograms prg = new z_repoPrograms())
                {
                    string str_area_name = "";
                    string str_prg_name = "";
                    string str_controller_name = "";
                    var prgData = prg.repo.ReadSingle(m => m.PrgNo == model.PrgNo);
                    if (prgData != null)
                    {
                        str_prg_name = prgData.PrgName;
                        str_area_name = prgData.AreaName;
                        str_controller_name = prgData.ControllerName;
                    }
                    if (string.IsNullOrEmpty(str_area_name))
                    {
                        TempData["ErrorMessage"] = "Programs 資料表未輸入區域名稱 !!";
                        return RedirectToAction("Index");
                    }
                    if (string.IsNullOrEmpty(str_controller_name))
                    {
                        TempData["ErrorMessage"] = "Programs 資料表未輸入控制器名稱 !!";
                        return RedirectToAction("Index");
                    }
                    model.PrgName = str_prg_name;
                    model.AreaName = str_area_name;
                    model.ControllerName = str_controller_name;
                    model.TextResult = code.GetControllerClass(model);
                    TempData["ResultModel"] = model;
                    return RedirectToAction("Result");
                }
            }
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Result()
        {
            using (CodeGenerator code = new CodeGenerator())
            {
                vmControllerModel model = (vmControllerModel)TempData["ResultModel"];
                model.FolderName = code.GetControllerFolderName(model.AreaName);
                model.FileName = code.GetControllerFileName(model.AreaName, model.ControllerName);
                return View(model);
            }
        }

        [HttpPost]
        [LoginAuthorize()]
        [ValidateInput(false)]
        public ActionResult Result(vmControllerModel model)
        {
            using (CodeGenerator code = new CodeGenerator())
            {
                bool bln_result = code.CreateControllerFile(model);
                string str_file_name = code.GetControllerClassName(model.ControllerName);
                if (bln_result)
                    TempData["ErrorMessage"] = $"{str_file_name} 檔案建立成功!!";
                else
                    TempData["ErrorMessage"] = $"{str_file_name} 檔案已存在,無法建立!!";
                return RedirectToAction("Index");
            }
        }
    }
}