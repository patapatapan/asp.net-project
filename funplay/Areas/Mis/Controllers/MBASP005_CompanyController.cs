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

namespace funplay.Areas.Mis.Controllers
{
    /// <summary>
    /// MBASP005_Company 公司基本資料維護
    /// </summary>
    public class MBASP005_CompanyController : BaseController
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

            using (z_repoCompanys repos = new z_repoCompanys())
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
                    model = new Companys();
                    // 設定新增預設值
                    using (AttributeService attr = new AttributeService())
                    {
                        model.IsDefault = true;
                        model.IsEnabled = true;
                        model.CodeNo = "Company";
                        //                        model.CompNo = (string)attr.GetDefaultValue<z_metaCompanys>("CompNo");
                        //                        model.CompName = (string)attr.GetDefaultValue<z_metaCompanys>("CompName");
                        //                        model.ShortName = (string)attr.GetDefaultValue<z_metaCompanys>("ShortName");
                        //                        model.EngName = (string)attr.GetDefaultValue<z_metaCompanys>("EngName");
                        //                        model.EngShortName = (string)attr.GetDefaultValue<z_metaCompanys>("EngShortName");
                        //                        model.RegisterDate = (DateTime)attr.GetDefaultValue<z_metaCompanys>("RegisterDate");
                        //                        model.BossName = (string)attr.GetDefaultValue<z_metaCompanys>("BossName");
                        //                        model.ContactName = (string)attr.GetDefaultValue<z_metaCompanys>("ContactName");
                        //                        model.CompTel = (string)attr.GetDefaultValue<z_metaCompanys>("CompTel");
                        //                        model.ContactTel = (string)attr.GetDefaultValue<z_metaCompanys>("ContactTel");
                        //                        model.CompFax = (string)attr.GetDefaultValue<z_metaCompanys>("CompFax");
                        //                        model.CompID = (string)attr.GetDefaultValue<z_metaCompanys>("CompID");
                        //                        model.ContactEmail = (string)attr.GetDefaultValue<z_metaCompanys>("ContactEmail");
                        //                        model.CompAddress = (string)attr.GetDefaultValue<z_metaCompanys>("CompAddress");
                        //                        model.CompUrl = (string)attr.GetDefaultValue<z_metaCompanys>("CompUrl");
                        //                        model.TwitterUrl = (string)attr.GetDefaultValue<z_metaCompanys>("TwitterUrl");
                        //                        model.FacebookUrl = (string)attr.GetDefaultValue<z_metaCompanys>("FacebookUrl");
                        //                        model.InstagramUrl = (string)attr.GetDefaultValue<z_metaCompanys>("InstagramUrl");
                        //                        model.SkypeUrl = (string)attr.GetDefaultValue<z_metaCompanys>("SkypeUrl");
                        //                        model.LinkedinUrl = (string)attr.GetDefaultValue<z_metaCompanys>("LinkedinUrl");
                        //                        model.Latitude = (decimal)attr.GetDefaultValue<z_metaCompanys>("Latitude");
                        //                        model.Longitude = (decimal)attr.GetDefaultValue<z_metaCompanys>("Longitude");
                        //                        model.AboutusText = (string)attr.GetDefaultValue<z_metaCompanys>("AboutusText");
                        //                        model.SupportText = (string)attr.GetDefaultValue<z_metaCompanys>("SupportText");
                        //                        model.ReturnText = (string)attr.GetDefaultValue<z_metaCompanys>("ReturnText");
                        //                        model.ShippingText = (string)attr.GetDefaultValue<z_metaCompanys>("ShippingText");
                        //                        model.PaymentText = (string)attr.GetDefaultValue<z_metaCompanys>("PaymentText");
                        //                        model.Remark = (string)attr.GetDefaultValue<z_metaCompanys>("Remark");
                        //                        model.CodeName = (string)attr.GetDefaultValue<z_metaCompanys>("CodeName");
                    }
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
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ActionService.SetErrorMessage<z_metaCompanys>(ModelState);
                return View(model);
            }
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
        [LoginAuthorize()]
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
        /// 選取
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [HttpGet]
        [LoginAuthorize()]
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
