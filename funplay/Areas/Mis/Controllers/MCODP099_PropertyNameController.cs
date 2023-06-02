using funplay.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace funplay.Areas.Mis.Controllers
{
    /// <summary>
    /// MCODP099_PropertyName 預設欄位名稱維護
    /// </summary>
    public class MCODP099_PropertyNameController : BaseController
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

            using (z_repoPropertyNames repos = new z_repoPropertyNames())
            {
                PrgService.SearchText = searchText;
                PrgService.SetAction(enAction.Index, enCardSize.Max);
                PrgService.SetProgram();
                var model = repos.GetDapperDataList(searchText).ToPagedList(page, pageSize);
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
            using (z_repoPropertyNames repos = new z_repoPropertyNames())
            {
                PrgService.SetAction(enAction.Detail, enCardSize.Medium);
                var model = repos.repo.ReadSingle(m => m.Id == id);
                return View(model);
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

            using (z_repoPropertyNames repos = new z_repoPropertyNames())
            {
                SessionService.KeyValue = id;
                enAction action = (id == 0) ? enAction.Create : enAction.Edit;
                PrgService.SetAction(action, enCardSize.Medium);
                var model = repos.repo.ReadSingle(m => m.Id == id);
                if (model == null)
                {
                    // 設定新增預設值
                    model = new PropertyNames()
                    {
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
        public ActionResult CreateEdit(PropertyNames model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ActionService.SetErrorMessage<z_metaPropertyNames>(ModelState);
                return View(model);
            }
            using (z_repoPropertyNames repos = new z_repoPropertyNames())
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

            using (z_repoPropertyNames repos = new z_repoPropertyNames())
            {
                repos.Delete(id);
                dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = "資料已刪除!!" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 匯入資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Import()
        {
            using (z_repoPropertyNames propName = new z_repoPropertyNames())
            {
                int int_count = propName.AddPropertyName();
                dmJsonMessage result = new dmJsonMessage();
                result.Mode = true;
                result.Message = $"已匯入 {int_count} 筆資料!!";
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
