using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Areas.Mis.Controllers
{
    /// <summary>
    /// MCODP001_Metadata Metadata 產生器
    /// </summary>
    public class MCODP001_MetadataController : BaseController
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index()
        {
            PrgService.SetAction(enAction.Index, enCardSize.Large);
            PrgService.SetProgram();
            var model = new vmMetadataModel();
            model.KeyColumn = "Id";
            return View(model);
        }

        [HttpPost]
        [LoginAuthorize()]
        public ActionResult Index(vmMetadataModel model)
        {
            if (!ModelState.IsValid) return View(model);
            using (CodeGenerator code = new CodeGenerator())
            {
                model.TextResult = code.GetMetadataClass(model);
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
                vmMetadataModel model = (vmMetadataModel)TempData["ResultModel"];
                model.FolderName = code.MetadataFolderName;
                model.FileName = code.GetMetadataFileName(model.ClassName);
                return View(model);
            }
        }

        [HttpPost]
        [LoginAuthorize()]
        [ValidateInput(false)]
        public ActionResult Result(vmMetadataModel model)
        {
            using (CodeGenerator code = new CodeGenerator())
            {
                bool bln_result = code.CreateMetadataFile(model);
                string str_file_name = code.GetMetadataClassName(model.ClassName);
                if (bln_result)
                    TempData["ErrorMessage"] = $"{str_file_name} 檔案建立成功!!";
                else
                    TempData["ErrorMessage"] = $"{str_file_name} 檔案已存在,無法建立!!";
                return RedirectToAction("Index");
            }
        }
    }
}