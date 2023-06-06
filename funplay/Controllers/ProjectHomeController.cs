using DocumentFormat.OpenXml.Drawing.Charts;
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
        public ActionResult ProjectLogin()
        {
            vmProjectLogin model = new vmProjectLogin();
            return View(model);
        }

        /*登入驗證*/
        [HttpPost]
        public ActionResult ProjectLogin(vmProjectLogin model)
        {
            if (!ModelState.IsValid) return View(model);
            using (z_repoUsers repos = new z_repoUsers())
            {
                bool bln_value = repos.Login(model.Account, model.Password);
                if (!bln_value)
                {
                    ModelState.AddModelError("DisplayName", "帳號或密碼輸入錯誤!!");
                    return View(model);
                }
                if (!AppService.IsConfig) AppService.Init();
                return RedirectToAction("ProjectIndex", "ProjectController", new { area = "" });
            }
        }

        [HttpGet]
        public ActionResult ProjectRegister()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProjectProduct()
        {
            return View();
        }
    }


}