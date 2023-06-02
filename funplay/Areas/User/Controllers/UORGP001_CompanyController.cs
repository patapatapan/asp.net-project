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
    /// UORGP001_Company 公司基本資料維護
    /// </summary>
    public class UORGP001_CompanyController : BaseController
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
        public ActionResult Index()
        {
            //檢查瀏覽權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.Index))
                return RedirectToAction(ActionService.Index, ActionService.Home, new { area = ActionService.Area });

            using (z_repoCompanys repos = new z_repoCompanys())
            {
                PrgService.SubHeader = "";
                PrgService.SetAction(enAction.Index, enCardSize.Medium);
                PrgService.SetProgram();
                var model = repos.GetDapperDataList();
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
            using (z_repoCompanys repos = new z_repoCompanys())
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

            using (z_repoCompanys repos = new z_repoCompanys())
            {
                SessionService.KeyValue = id;
                enAction action = (id == 0) ? enAction.Create : enAction.Edit;
                PrgService.SetAction(action, enCardSize.Medium);
                var model = repos.repo.ReadSingle(m => m.Id == id);
                if (model == null)
                {
                    // 設定新增預設值
                    model = new Companys()
                    {
                        IsDefault = true,
                        IsEnabled = true,
                        RegisterDate = DateTime.Today,
                        Latitude = 0,
                        Longitude = 0,
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
        public ActionResult CreateEdit(Companys model)
        {
            if (!ModelState.IsValid) return View(model);
            using (z_repoCompanys repos = new z_repoCompanys())
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

            using (z_repoCompanys repos = new z_repoCompanys())
            {
                repos.Delete(id);
                dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = "資料已刪除!!" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 公司簡介
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <param name="columnName">Column Name</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HtmlEditor(int id = 0, string columnName = "")
        {
            //檢查修改權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.Edit))
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });

            using (z_repoCompanys repos = new z_repoCompanys())
            {
                var model = repos.repo.ReadSingle(m => m.Id == id);
                if (model == null) return RedirectToAction("Index");
                string str_value = "";
                if (columnName == "AboutusText") { PrgService.SubHeader = "公司簡介"; str_value = model.AboutusText; }
                if (columnName == "SupportText") { PrgService.SubHeader = "服務介紹"; str_value = model.SupportText; }
                if (columnName == "ReturnText") { PrgService.SubHeader = "退貨處理"; str_value = model.ReturnText; }
                if (columnName == "ShippingText") { PrgService.SubHeader = "送貨說明"; str_value = model.ShippingText; }
                if (columnName == "PaymentText") { PrgService.SubHeader = "付款說明"; str_value = model.PaymentText; }
                ActionService.SetPriorAction(ActionService.Area, ActionService.Controller, ActionService.Index, enPriorParmIdType.None, 0, "Company");
                ActionService.SetPriorUpdate("Companys", "Id", id, columnName, str_value);
                return RedirectToAction(ActionService.Index, ActionService.HtmlEditor, new { area = "" });
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
