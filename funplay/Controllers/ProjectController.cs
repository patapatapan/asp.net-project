using DocumentFormat.OpenXml.Drawing.Charts;
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
    public class ProjectController : BaseController
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index()
        {
            //ViewBag.Css1 = Url.Content("~/Content/css/navbar.css");
            //ViewBag.Css2 = Url.Content("~/Content/css/index.css");            
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Product()
        {
            return View();
        }
    }


}