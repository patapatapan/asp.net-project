using funplay.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace funplay.Controllers
{
    public class ForumBoardController : BaseController
    {
        /// <summary>
        /// 資料列表
        /// </summary>
        /// <param name="page">目前頁數</param>
        /// <param name="pageSize">每頁筆數</param>
        /// <param name="searchText">搜尋文字</param>
        /// <returns></returns>
        [HttpGet]
        [LoginAuthorize(RoleList = "User,Mis")]
        public ActionResult Index(int page = 1, int pageSize = 10, string searchText = "")
        {
            //檢查瀏覽權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.Index))
                return RedirectToAction(ActionService.Index, ActionService.Home, new { area = ActionService.Area });

            using (z_repoForums repos = new z_repoForums())
            {
                PrgService.SearchText = searchText;
                PrgService.SetAction(enAction.Index, enCardSize.Max);
                var model = repos.GetDapperDataList(SessionService.TagNo1, searchText).ToPagedList(page, pageSize);
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, model.PageNumber, model.PageCount);
                PrgService.SetProgramNoName("ForumBoard", SessionService.TagName1);
                PrgService.Action = $"{SessionService.TagName1} 討論區";
                PrgService.SubHeader = "討論區";
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
        [LoginAuthorize(RoleList = "User,Mis")]
        public ActionResult Detail(string id = "")
        {
            using (z_repoForums repos = new z_repoForums())
            {
                var forumsModel = repos.GetForums(id);
                PrgService.SetAction(forumsModel.SubjectName, enCardSize.Max);
                PrgService.SubHeader = "討論區 / " + $"{SessionService.TagName1} 討論區"; ;
                SessionService.TagNo2 = id;
                var model = repos.GetDapperDetailList(id);
                return View(model);
            }
        }

        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [HttpGet]
        [LoginAuthorize(RoleList = "User,Mis")]
        public ActionResult CreateEdit(int id = 0)
        {
            //檢查新增/修改權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.CreateEdit, id))
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });

            ActionService.SetPriorAction(ActionService.Area, ActionService.Controller, ActionService.Index, enPriorParmIdType.None, 0, "Forum");
            using (z_repoForums repos = new z_repoForums())
            {
                SessionService.KeyValue = id;
                enAction action = (id == 0) ? enAction.Create : enAction.Edit;
                PrgService.SetAction(action, enCardSize.Max);
                var model = repos.repo.ReadSingle(m => m.Id == id);
                if (model == null)
                {
                    // 設定新增預設值
                    model = new Forums()
                    {
                        BoardNo = SessionService.TagNo1,
                        IsEnabled = true,
                        IsClosed = false,
                        SubjectDate = DateTime.Now,
                        UserNo = UserService.UserNo,
                        GuidNo = Guid.NewGuid().ToString().Replace("-", ""),
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
        [LoginAuthorize(RoleList = "User,Mis")]
        [ValidateInput(false)]
        public ActionResult CreateEdit(Forums model)
        {
            if (!ModelState.IsValid) return View(model);
            if (string.IsNullOrEmpty(model.SubjectName))
            {
                ModelState.AddModelError("SubjectName", "不可空白!!");
                return View(model);
            }
            using (z_repoForums repos = new z_repoForums())
            {
                model.ParentGuid = "";
                model.ReplyGuid = "";
                model.SubjectDate = DateTime.Now;
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
        [LoginAuthorize(RoleList = "User,Mis")]
        public ActionResult Delete(int id = 0)
        {
            //檢查刪除權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.Delete))
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });

            using (z_repoForums repos = new z_repoForums())
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
        [LoginAuthorize(RoleList = "User,Mis")]
        public ActionResult Select(int id = 0)
        {
            PrgService.SelectedId = id;
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, page = PrgService.PageNumber, searchText = PrgService.SearchText });
        }

        /// <summary>
        /// 回覆新增/修改
        /// </summary>
        /// <param name="id">文章 GUID</param>
        /// <returns></returns>
        [HttpGet]
        [LoginAuthorize(RoleList = "User,Mis")]
        public ActionResult Reply(string id = "")
        {
            //檢查新增/修改權限
            if (!PrgService.IsProgramSecurity(enSecurtyMode.CreateEdit))
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });

            ActionService.SetPriorAction(ActionService.Area, ActionService.Controller, ActionService.Index, enPriorParmIdType.None, 0, "Forum");
            using (z_repoForums repos = new z_repoForums())
            {
                enAction action = (id == "") ? enAction.Create : enAction.Edit;
                PrgService.SetAction(action, enCardSize.Max);
                var model = repos.repo.ReadSingle(m => m.GuidNo == id);
                SessionService.TagNo3 = id;
                if (model == null)
                {
                    // 設定新增預設值
                    model = new Forums()
                    {
                        GuidNo = "",
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
        [LoginAuthorize(RoleList = "User,Mis")]
        [ValidateInput(false)]
        public ActionResult Reply(Forums model)
        {
            using (z_repoForums repos = new z_repoForums())
            {
                if (string.IsNullOrEmpty(model.GuidNo))
                {
                    model.BoardNo = SessionService.TagNo1;
                    model.ParentGuid = SessionService.TagNo2;
                    model.ReplyGuid = "";
                    model.IsEnabled = true;
                    model.IsClosed = false;
                    model.SubjectDate = DateTime.Now;
                    model.UserNo = UserService.UserNo;
                    model.GuidNo = Guid.NewGuid().ToString().Replace("-", "");
                    model.SubjectDate = DateTime.Now;
                    repos.CreateEdit(model);
                }
                else
                {
                    var data = repos.GetForums(SessionService.TagNo3);
                    data.SubjectContent = model.SubjectContent;
                    repos.CreateEdit(data);
                }
                return RedirectToAction(ActionService.Detail, ActionService.Controller, new { area = ActionService.Area, id = SessionService.TagNo2 });
            }
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [LoginAuthorize(RoleList = "User,Mis")]
        public ActionResult Search()
        {
            object obj_text = Request.Form[ActionService.SearchText];
            string str_text = (obj_text == null) ? string.Empty : obj_text.ToString();
            return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, searchText = str_text });
        }

        /// <summary>
        /// 圖片上傳
        /// </summary>
        /// <param name="upload">上傳檔案</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase upload)
        {
            string str_folder = "~/Images/Forum";
            string str_path = Server.MapPath(str_folder);

            //如果目錄不存在則建立此目錄
            if (!Directory.Exists(str_path)) Directory.CreateDirectory(str_path);

            //獲取圖片檔案名稱及延伸檔名
            string str_file_name = Path.GetFileName(upload.FileName);
            string str_file_ext = Path.GetExtension(str_file_name).ToLower();

            //用時間來生成新圖片名稱並存檔
            string str_save_name = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + str_file_ext;
            string str_upload_url = str_path + "/" + str_save_name;
            upload.SaveAs(str_upload_url);

            str_upload_url = "../../Images/Forum/" + str_save_name;
            //上傳成功後，返回Json格式的響應
            return Json(new
            {
                uploaded = 1,
                fileName = str_save_name,
                url = str_upload_url
            });
        }
    }
}
