using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Areas.User.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index()
        {
            ActionService.SetCalendarAction(UserService.RoleNo, UserService.UserNo, "個人行事曆");
            ViewBag.Today = DateTime.Today.ToString("yyyy-MM-dd");
            PrgService.SetAction(enAction.Home, enCardSize.Max);
            PrgService.SetProgram("", "首頁");
            return View();
        }
    }
}