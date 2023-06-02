using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class WebController : Controller
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index()
        {
            UserService.Logout();
            return RedirectToAction("Login", "Web", new { area = "" });
        }

        public ActionResult Login()
        {
            vmLogin model = new vmLogin();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(vmLogin model)
        {
            if (!ModelState.IsValid) return View(model);
            using (z_repoUsers repos = new z_repoUsers())
            {
                bool bln_value = repos.Login(model.UserNo, model.Password);
                if (!bln_value)
                {
                    ModelState.AddModelError("UserNo", "帳號或密碼輸入錯誤!!");
                    return View(model);
                }
                if (!AppService.IsConfig) AppService.Init();
                return RedirectToAction("Home", "Web", new { area = "" });
            }
        }

        public ActionResult Home()
        {
            string str_area = "";
            string str_controller = "Web";
            string str_action = "Login";
            if (UserService.IsLogin)
            {
                str_area = UserService.RoleNo;
                str_controller = "Home";
                str_action = "Index";
            }
            return RedirectToAction(str_action, str_controller, new { area = str_area });
        }

        public ActionResult Logout()
        {
            UserService.Logout();
            return RedirectToAction("Login", "Web", new { area = "" });
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult ChangePassword()
        {
            vmChangePassword model = new vmChangePassword();
            return View(model);
        }

        [HttpPost]
        [LoginAuthorize()]
        public ActionResult ChangePassword(vmChangePassword model)
        {
            if (!ModelState.IsValid) return View(model);
            using (z_repoUsers repos = new z_repoUsers())
            {
                bool bln_value = repos.ChangePassword(model.OldPassword, model.NewPassword);
                if (!bln_value)
                {
                    ModelState.AddModelError("OldPassword", "密碼輸入錯誤!!");
                    return View(model);
                }
                TempData["ErrorMessage"] = "密嗎已成功變更!!";
                return RedirectToAction("Index", "Home", new { area = UserService.RoleNo });
            }
        }

        /// <summary>
        /// 使用者註冊電子信箱驗證
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]

        public ActionResult ValidateEmail(string id)
        {
            using (z_repoUsers users = new z_repoUsers())
            {
                var userData = users.repo.ReadSingle(m => m.ValidateCode == id);

                string str_message = "";
                if (!users.ValidateEmail(id, ref str_message))
                    TempData["Message"] = str_message;
                else
                {
                    //記錄會員註冊驗證成功時間
                    using (z_repoLogs logs = new z_repoLogs())
                    {
                        logs.EventLogCount(enLogType.EmailSend, userData.UserNo, id);
                    }
                    TempData["Message"] = "員工電子郵件已驗證成功，您可以進入登入頁登入系統!!";
                }
                //顯示訊息畫面
                return RedirectToAction("Login", "Web", new { area = "" });
            }
        }
    }
}