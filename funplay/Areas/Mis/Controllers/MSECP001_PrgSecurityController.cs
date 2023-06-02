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
using static Slapper.AutoMapper;

namespace funplay.Areas.Mis.Controllers
{
    /// <summary>
    /// MSECP001_PrgSecurity 程式使用者權限設定
    /// </summary>
    public class MSECP001_PrgSecurityController : BaseController
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

            using (z_repoPrograms repos = new z_repoPrograms())
            {
                PrgService.SearchText = searchText;
                PrgService.SetAction(enAction.Index, enCardSize.Max);
                PrgService.SetProgram();
                PrgService.SubHeader = "";
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
            using (z_repoPrograms repos = new z_repoPrograms())
            {
                var model = repos.repo.ReadSingle(m => m.Id == id);
                SessionService.TagNo1 = model.RoleNo;
                SessionService.TagNo2 = model.PrgNo;
                repos.SetSubHeader(model.PrgNo, "程式");
                return RedirectToAction(ActionService.Index, "MSECP001_Security", new { area = ActionService.Area });
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

            using (z_repoPrograms repos = new z_repoPrograms())
            {
                SessionService.KeyValue = id;
                enAction action = (id == 0) ? enAction.Create : enAction.Edit;
                PrgService.SetAction(action, enCardSize.Medium);
                var model = repos.repo.ReadSingle(m => m.Id == id);
                if (model == null)
                {
                    // 設定新增預設值
                    model = new Programs()
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
        public ActionResult CreateEdit(Programs model)
        {
            if (!ModelState.IsValid) return View(model);
            using (z_repoPrograms repos = new z_repoPrograms())
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

            using (z_repoPrograms repos = new z_repoPrograms())
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

        [HttpGet]
        [LoginAuthorize()]
        public PartialViewResult AddUserList(string id)
        {
            using (z_repoSecuritys sec = new z_repoSecuritys())
            {
                string str_role_no = id.Split('_')[0];
                string str_prg_no = id.Split('_')[1];
                var model = sec.GetDapperPrgAddUserList(str_role_no, str_prg_no);
                SessionService.TagNo1 = str_role_no;
                SessionService.TagNo2 = str_prg_no;
                return PartialView("AddUserList", model);
            }
        }

        [HttpPost]
        [LoginAuthorize()]
        public ActionResult AddUserList(FormCollection collection)
        {
            using (z_repoSecuritys sec = new z_repoSecuritys())
            {
                if (collection.AllKeys.Count() > 0)
                {
                    string str_role_no = SessionService.TagNo1;
                    string str_prg_no = SessionService.TagNo2;
                    foreach (string key in collection.AllKeys)
                    {
                        if (key.EndsWith("_1"))
                        {
                            string str_user_no = key.Substring(0, key.Length - 2);
                            sec.AddSecurityUser(str_role_no, str_prg_no, str_user_no);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [LoginAuthorize()]
        public PartialViewResult DelUserList(string id)
        {
            using (z_repoSecuritys sec = new z_repoSecuritys())
            {
                string str_role_no = id.Split('_')[0];
                string str_prg_no = id.Split('_')[1];
                var model = sec.GetDapperPrgDelUserList(str_role_no, str_prg_no);
                SessionService.TagNo1 = str_role_no;
                SessionService.TagNo2 = str_prg_no;
                return PartialView("DelUserList", model);
            }
        }

        [HttpPost]
        [LoginAuthorize()]
        public ActionResult DelUserList(FormCollection collection)
        {
            using (z_repoSecuritys sec = new z_repoSecuritys())
            {
                if (collection.AllKeys.Count() > 0)
                {
                    string str_role_no = SessionService.TagNo1;
                    string str_prg_no = SessionService.TagNo2;
                    foreach (string key in collection.AllKeys)
                    {
                        if (key.EndsWith("_1"))
                        {
                            string str_user_no = key.Substring(0, key.Length - 2);
                            sec.DelSecurityUser(str_role_no, str_prg_no, str_user_no);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
        }

        //[HttpGet]
        //[LoginAuthorize()]
        //public ActionResult AddUser(string id)
        //{
        //    using (z_repoSecuritys sec = new z_repoSecuritys())
        //    {
        //        sec.AddSecurityUser(ActionService.TargetNo, id);
        //        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        //    }
        //}

        //[HttpGet]
        //[LoginAuthorize()]
        //public ActionResult DelUser(string id)
        //{
        //    using (z_repoSecuritys sec = new z_repoSecuritys())
        //    {
        //        sec.DelSecurityUser(ActionService.TargetNo, id);
        //        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
        //    }
        //}
    }
}
