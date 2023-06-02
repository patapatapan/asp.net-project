using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index()
        {
            using (z_repoUsers repos = new z_repoUsers())
            {
                PrgService.SetAction(enAction.Detail, enCardSize.Medium);
                PrgService.SetProgram("個人帳號明細");
                var model = repos.GetDapperLoginData();
                if (model == null) return RedirectToAction("Index", "Home", new { area = "" });
                return View(model);
            }
        }

        /// <summary>
        /// 新增/修改
        /// </summary>

        /// <returns></returns>
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Edit()
        {
            using (z_repoUsers repos = new z_repoUsers())
            {
                PrgService.SetAction(enAction.Edit, enCardSize.Medium);
                var model = repos.GetDapperLoginData();
                SessionService.KeyValue = model.Id;
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
        public ActionResult Edit(Users model)
        {
            if (!ModelState.IsValid) return View(model);
            using (z_repoUsers repos = new z_repoUsers())
            {
                repos.Edit(model);
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
            }
        }

        /// <summary>
        /// 上傳圖像
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize()]
        public ActionResult UploadImage()
        {
            ImageService.FileConfig("~/Images/User", UserService.UserNo, "jpg");
            ActionService.SetPriorAction("", "Profile", "Index", enPriorParmIdType.None, 0, "Company");
            return RedirectToAction(ActionService.Upload, ActionService.Image, new { area = "" });
        }
    }
}