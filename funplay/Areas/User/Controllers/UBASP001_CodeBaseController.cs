using funplay.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace funplay.Areas.User.Controllers
{
    /// <summary>
    /// UBASP001_CodeBase  基本主檔資料維護
    /// </summary>
    public class UBASP001_CodeBaseController : BaseController
    {
        /// <summary>
        /// 資料列表
        /// </summary>
        /// <param name="page">目前頁數</param>
        /// <param name="pageSize">每頁筆數</param>
        /// <param name="searchText">搜尋文字</param>
        /// <returns></returns>
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index(int page = 1, int pageSize = 10, string searchText = "")
        {
            //檢查瀏覽權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.Index))
                return RedirectToAction(ActionService.Index, ActionService.Home, new { area = ActionService.Area });

            using (z_repoCodeBases repos = new z_repoCodeBases())
            {
                PrgService.SearchText = searchText;
                PrgService.SetAction(enAction.Index, enCardSize.Max);
                PrgService.SetProgram();
                var model = repos.GetDapperDataList(false, searchText).ToPagedList(page, pageSize);
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, model.PageNumber, model.PageCount);
                ViewBag.SearchText = searchText;
                ViewBag.PageInfo = $"第{model.PageNumber}頁,共{model.PageCount}頁";
                return View(model);
            }
        }

        /// <summary>
        /// 明細
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Detail(int id = 0)
        {
            using (z_repoCodeBases repos = new z_repoCodeBases())
            {
                var data = repos.repo.ReadSingle(m => m.Id == id);
                if (data == null) return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
                SessionService.TagNo1 = data.BaseNo;
                PrgService.SubHeader = $"{data.BaseNo} {data.BaseName}";
                return RedirectToAction(ActionService.Index, "UBASP001_CodeData", new { area = ActionService.Area });
            }
        }

        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult CreateEdit(int id = 0)
        {
            //檢查新增/修改權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.CreateEdit, id))
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });


            using (z_repoCodeBases repos = new z_repoCodeBases())
            {
                SessionService.KeyValue = id;
                enAction action = (id == 0) ? enAction.Create : enAction.Edit;
                PrgService.SetAction(action, enCardSize.Medium);
                var model = repos.repo.ReadSingle(m => m.Id == id);
                if (model == null)
                {
                    model = new CodeBases()
                    {
                        IsAdmin = false,
                        BaseNo = "",
                        BaseName = "",
                        DefaultValue = "",
                        Remark = ""
                    };
                }
                return View(model);
            }
        }

        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="model">資料</param>
        /// <returns></returns>
        [HttpPost]
        [LoginAuthorize()]
        public ActionResult CreateEdit(CodeBases model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ActionService.SetErrorMessage<z_metaCodeBases>(ModelState);
                return View(model);
            }
            using (z_repoCodeBases repos = new z_repoCodeBases())
            {
                repos.CreateEdit(model);
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            //檢查刪除權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.Delete))
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });

            using (z_repoCodeBases repos = new z_repoCodeBases())
            {
                repos.Delete(id);
                dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = "資料已刪除!!" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 選取
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Select(int id = 0)
        {
            PrgService.SelectedId = id;
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, page = PrgService.PageNumber, searchText = PrgService.SearchText });
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [LoginAuthorize()]
        public ActionResult Search()
        {
            object obj_text = Request.Form[ActionService.SearchText];
            string str_text = (obj_text == null) ? string.Empty : obj_text.ToString();
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, searchText = str_text });
        }
    }
}