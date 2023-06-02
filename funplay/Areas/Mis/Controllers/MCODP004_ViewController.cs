using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Areas.Mis.Controllers
{
    /// <summary>
    /// MCODP004_View View 產生器
    /// </summary>
    public class MCODP004_ViewController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            PrgService.SetAction(enAction.Index, enCardSize.Large);
            PrgService.SetProgram("Mis", "MCODP004", "View 產生器");
            var model = new vmViewModel();
            model.Id = 1;
            model.AreaName = "User";
            model.ControllerName = "";
            model.ViewName = "Index";
            model.ClassName = "";
            model.DeleteConfirmColumns = "";
            model.LayoutName = "_LayoutAdmin";
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(vmViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            using (CodeGenerator code = new CodeGenerator())
            {
                if (model.TemplateName == "Index") model.TextResult = code.GetViewIndexClass(model);
                if (model.TemplateName == "CreateEdit1") model.TextResult = code.GetViewCreateEdit1Class(model);
                if (model.TemplateName == "CreateEdit2") model.TextResult = code.GetViewCreateEditNClass(model, 2);
                if (model.TemplateName == "CreateEdit3") model.TextResult = code.GetViewCreateEditNClass(model, 3);
                TempData["ResultModel"] = model;
                return RedirectToAction("Result");
            }
        }

        [HttpGet]
        public ActionResult Result()
        {
            using (CodeGenerator code = new CodeGenerator())
            {
                vmViewModel model = (vmViewModel)TempData["ResultModel"];
                model.FolderName = code.GetViewFolderName(model.AreaName, model.ControllerName);
                model.FileName = code.GetViewFileName(model.AreaName, model.ControllerName, model.ViewName);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Result(vmViewModel model)
        {
            using (CodeGenerator code = new CodeGenerator())
            {
                bool bln_result = code.CreateViewFile(model);
                string str_file_name = code.GetViewClassName(model.ViewName);
                if (bln_result)
                    TempData["ErrorMessage"] = $"{str_file_name} 檔案建立成功!!";
                else
                    TempData["ErrorMessage"] = $"{str_file_name} 檔案已存在,無法建立!!";
                return RedirectToAction("Index");
            }
        }
    }
}