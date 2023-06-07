using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    /// <summary>
    /// ProjectController
    /// </summary>
    public class ProjectHomeController : BaseController
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult ProjectIndex()
        {         
            return View();
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult ProjectLogin()
        {
            vmProjectLogin model = new vmProjectLogin();
            return View(model);
        }

        /*登入驗證*/
        [HttpPost]
        [LoginAuthorize()]
        public ActionResult ProjectLogin(vmProjectLogin model)
        {
            if (!ModelState.IsValid) return View(model);
            using (z_repoUsers repos = new z_repoUsers())
            {
                bool bln_value = repos.ProjectLogin(model.Account, model.Password);
                if (!bln_value)
                {
                    ModelState.AddModelError("DisplayName", "帳號或密碼輸入錯誤!!");
                    return View(model);
                }
                if (!AppService.IsConfig) AppService.Init();
                return RedirectToAction("ProjectIndex", "ProjectHome", new { area = "" });
            }
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult ProjectRegister()
        {
            vmProjectRegister model = new vmProjectRegister();
            return View(model);
        }

        [HttpPost]
        [LoginAuthorize()]
        public ActionResult ProjectRegister(FormCollection collection)
        {
            vmProjectRegister model = new vmProjectRegister();
            model.UserName = collection["UserName"].ToString();
            model.Birthday = collection["Birthday"].ToString();
            model.Email = collection["Email"].ToString();
            model.Account = collection["Account"].ToString();
            model.Password = collection["Password"].ToString();

            //1. 沒有通過驗證，返回登入頁繼續輸入
            if (!ModelState.IsValid) return View(model);
            //2. 判斷登入資訊是否正確，不正確時手動引發一個錯誤
            string str_message = UserService.ProjectRegister(model);
            if (!string.IsNullOrEmpty(str_message))
            {
                if (str_message == "登入信箱重複註冊!!")
                {
                    ModelState.AddModelError("UserEmail", str_message);
                    return View(model);
                }

                ModelState.AddModelError("UserNo", str_message);
                return View(model);
            }
            //3.新增一筆未審核會員資訊
            string str_code = UserService.ProjectRegisterCreate(model);
            //4.寄出一封註冊驗證信件
            using (SendMailService sendEmail = new SendMailService())
            {
                sendEmail.UserRegister(str_code);
            }
            //5.顯示註冊訊息
            TempData["MessageText"] = $"您的註冊資訊已提交，請至您的電子信箱{model.Email}中驗證電子信箱功能,謝謝!!";
            return RedirectToAction("ProjectIndex", "ProjectHome", new { area = "" });
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult ProjectProduct()
        {
            return View();
        }
    }


}